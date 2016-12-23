using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using Films.Helpers;

namespace Films.Models
{
    internal sealed class Films
    {
        private DataTable displayTable = null;
        private IFilmsInformer filmsInformer;
        private const String serverName = @"(localdb)\ProjectsV12";
        private const String databaseName = @"FilmsDatabase";
        private readonly static  String connectionString = String.Format(@"Data Source={0};Initial Catalog={1};Integrated Security=True", serverName, databaseName);

        public Films(IFilmsInformer filmsInformer)
        {
            this.filmsInformer = filmsInformer;
        }

        ~Films()
        {
            displayTable.Dispose();
        }

        public void LoadData()
        {
            if (displayTable != null)
            {
                displayTable.Dispose();
                displayTable = null;
            }
            displayTable = GetData(String.Format(@"select f.Id as '{0}', f.Image as '{1}', f.Name as '{2}', f.Country as '{3}', f.Year as '{4}', d.Name as '{5}' from
                Films as f join Directors as d on d.Id = f.DirectorId", 
                ColumnsNameHelper.Id, ColumnsNameHelper.ImagePath, ColumnsNameHelper.Name, ColumnsNameHelper.Country, ColumnsNameHelper.Year, ColumnsNameHelper.Director));
            GetImages();
            
            filmsInformer.InformDataLoaded(GetColumnsToDisplay(displayTable));
        }

        public void DeleteFilms(String[] filmNamesToDelete)
        {
            String filmNamesList = String.Format("(N'{0}')", String.Join("',N'", filmNamesToDelete));
            SqlDataAdapter deleteAdapter = new SqlDataAdapter(String.Format("select * from Films where Name in {0}", filmNamesList), connectionString);

            DataTable filmsToDeleteTable = new DataTable();
            deleteAdapter.Fill(filmsToDeleteTable);

            foreach (DataRow row in filmsToDeleteTable.Rows)
            {
                row.Delete();
            }

            deleteAdapter.DeleteCommand = new SqlCommand("delete from Films where Id = @Id", new SqlConnection(connectionString));
            deleteAdapter.DeleteCommand.Parameters.Add("@Id", SqlDbType.Int, 4, "Id");
            deleteAdapter.Update(filmsToDeleteTable);
            filmsToDeleteTable.Dispose();
            deleteAdapter.Dispose();

            String selectFilmNamesList = String.Format("('{0}')", String.Join("','", filmNamesToDelete));
            DataRow[] rows = displayTable.Select(String.Format("Name in {0}", selectFilmNamesList));
            foreach (DataRow row in rows)
            {
                row.Delete();
            }

            filmsInformer.InformDeleteFinished(GetColumnsToDisplay(displayTable));
        }

        public void Search(Dictionary<String, String> searchData)
        {
            const String and = " and ";
            StringBuilder stringBuilder = new StringBuilder();
            foreach (KeyValuePair<String, String> pair in searchData)
            {
                String data = pair.Value.Replace('*', '%').Replace("?", "*");
                if (pair.Key.Equals(ColumnsNameHelper.Actor))
                {
                    DataTable table = GetData(String.Format("select f.Id from Films as f join ActorFilm as af on f.Id = af.FilmId join Actors as a on af.ActorId = a.Id where a.Name like N'{0}'",
                    data));

                    List<string> idList = new List<string>();
                    foreach (DataRow row in table.Rows)
                    {
                        idList.Add(row[0].ToString());
                    }
                    table.Dispose();
                   
                    if (idList.Count == 0)
                    {
                        filmsInformer.InformSearchFinished(GetColumnsToDisplay(displayTable.Clone()));
                        return;
                    }
                    String actorSearchFilms = String.Format("({0})", String.Join(",", idList));
                    stringBuilder.AppendFormat("Id in {0}", actorSearchFilms);
                } else
                {
                    stringBuilder.AppendFormat("{0} like '{1}'", pair.Key, data);
                }
                stringBuilder.Append(and);
            }
            stringBuilder.Remove(stringBuilder.Length - and.Length, and.Length);
            DataRow[] rows = displayTable.Select(stringBuilder.ToString());

            DataTable resultTable = displayTable.Clone();
            foreach (DataRow row in rows)
            {
                resultTable.ImportRow(row);
            }

            filmsInformer.InformSearchFinished(GetColumnsToDisplay(resultTable));
        }

        public void UpdateFilm(Dictionary<String, String> updateInfo)
        {
            StringBuilder setStringBuilder = new StringBuilder();
            foreach (KeyValuePair<String, String> pair in updateInfo)
            {
                if (!pair.Key.Equals(ColumnsNameHelper.SourceName))
                {
                    setStringBuilder.AppendFormat("{0} = '{1}',", pair.Key, pair.Value);
                }
            }
            setStringBuilder.Remove(setStringBuilder.Length - 1, 1);

            SqlDataAdapter updateAdapter = new SqlDataAdapter(String.Format("select * from Films where Name = N'{0}'", updateInfo[ColumnsNameHelper.SourceName]), connectionString);

            DataTable filmsToUpdateTable = new DataTable();
            updateAdapter.Fill(filmsToUpdateTable);

            foreach (DataRow row in filmsToUpdateTable.Rows)
            {
                row["Name"] = updateInfo[ColumnsNameHelper.Name];
                row["Country"] = updateInfo[ColumnsNameHelper.Country];
                row["Year"] = updateInfo[ColumnsNameHelper.Year];
            }

            updateAdapter.UpdateCommand = new SqlCommand("update Films set Name = @Name, Country = @Country, Year = @Year where Id = @Id", new SqlConnection(connectionString));
            updateAdapter.UpdateCommand.Parameters.Add("@Id", SqlDbType.Int, 4, "Id");
            updateAdapter.UpdateCommand.Parameters.Add("@Name", SqlDbType.NVarChar, 50, "Name");
            updateAdapter.UpdateCommand.Parameters.Add("@Country", SqlDbType.NVarChar, 50, "Country");
            updateAdapter.UpdateCommand.Parameters.Add("@Year", SqlDbType.NVarChar, 50, "Year");
            updateAdapter.Update(filmsToUpdateTable);
            filmsToUpdateTable.Dispose();
            updateAdapter.Dispose();

            DataRow[] rows = displayTable.Select(String.Format("Name = '{0}'", updateInfo["SourceName"]));
            foreach (DataRow row in rows)
            {
                row["Name"] = updateInfo[ColumnsNameHelper.Name];
                row["Country"] = updateInfo[ColumnsNameHelper.Country];
                row["Year"] = updateInfo[ColumnsNameHelper.Year];
            }

            filmsInformer.InformUpdateFinished(GetColumnsToDisplay(displayTable));
        }


        private DataTable GetColumnsToDisplay(DataTable sourceTable)
        {
            String[] columnsToSelect = new[] { ColumnsNameHelper.Image, ColumnsNameHelper.Name, ColumnsNameHelper.Country, ColumnsNameHelper.Year, ColumnsNameHelper.Director };
            return new DataView(sourceTable).ToTable(false, columnsToSelect);
        }

        private DataTable GetData(string selectCommand)
        {
            SqlDataAdapter dataAdapter = new SqlDataAdapter(selectCommand, connectionString);

            DataTable table = new DataTable();
            dataAdapter.Fill(table);
            dataAdapter.Dispose();

            return table;
        }

        private void GetImages()
        {
            displayTable.Columns.Add(ColumnsNameHelper.Image, typeof(byte[]));
            foreach (DataRow dr in displayTable.Rows)
            {
                String fileName = (String)dr[ColumnsNameHelper.ImagePath];
                Bitmap bitmap = (Bitmap)Properties.Resources.ResourceManager.GetObject(fileName);
                if (bitmap == null)
                {
                    bitmap = Properties.Resources.DefaultImage;
                }
                dr[ColumnsNameHelper.Image] = BitmapToByteArray(bitmap);
            }
        }

        public byte[] BitmapToByteArray(Bitmap bitmap)
        {
            MemoryStream ms = new MemoryStream();
            bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return ms.ToArray();
        }
    }
}
