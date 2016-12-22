using Films.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;
using System.Drawing;
using Films.Views;

namespace Films.Controllers
{
    internal sealed class FilmsController : IFilmsInformer
    {
        private IFilmsView filmsView;
        private ISearchView searchView;
        private IEditView editView;
        private Models.Films films;

        public FilmsController(IFilmsView filmsView)
        {
            this.filmsView = filmsView;
            films = new Models.Films(this);
            filmsView.DeleteStarted += FilmsViewInformer_DeleteStarted;
            filmsView.LoadStarted += FilmsViewInformer_LoadStarted;
            filmsView.SearchRequested += FilmsViewInformer_SearchRequested;
            filmsView.EditRequested += FilmsView_EditRequested;
            filmsView.DisableMenu();
            Task.Run(() => films.LoadData());
        }

        public void InformDataLoaded(DataTable data)
        {
            filmsView.InformDataLoaded(data);
            filmsView.EnableMenu();
        }

        public void InformSearchFinished(DataTable data)
        {
            filmsView.InformSearchFinished(data);
            filmsView.EnableMenu();
        }

        public void InformDeleteFinished(DataTable data)
        {
            filmsView.InformDeleteFinished(data);
            filmsView.EnableMenu();
        }

        public void InformUpdateFinished(DataTable data)
        {
            filmsView.InformUpdateFinished(data);
            editView.Close();
            filmsView.EnableMenu();
        }

        private void FilmsViewInformer_LoadStarted(object sender, EventArgs e)
        {
            filmsView.DisableMenu();
            Task.Run(() => films.LoadData());
        }

        private void FilmsViewInformer_DeleteStarted(object sender, String[] filmsNamesToDelete)
        {
            filmsView.DisableMenu();
            Task.Run(() => films.DeleteFilms(filmsNamesToDelete));
        }

        private void FilmsViewInformer_SearchRequested(object sender, FormLocationInfo info)
        {
            SearchForm form = new SearchForm();
            if (ShowForm(form, info))
            {
                searchView = form;
                searchView.SearchStarted += SearchView_SearchStarted;
            }
        }

        private void SearchView_SearchStarted(object sender, Dictionary<string, string> searchData)
        {
            filmsView.DisableMenu();
            Task.Run(() => films.Search(searchData));
        }

        private void FilmsView_EditRequested(object sender, FormLocationWithFilmInfo info)
        {
            EditForm form = new EditForm();
            if (ShowForm(form, info.Location))
            {
                editView = form;
                editView.SetFilmInfo(info.FilmName, info.FilmCountry, info.FilmYear);
                editView.EditStarted += EditView_EditStarted;
            }
        }

        private void EditView_EditStarted(object sender, Dictionary<string, string> editInfo)
        {
            filmsView.DisableMenu();
            Task.Run(() => films.UpdateFilm(editInfo));
        }

        private bool ShowForm(Form formToShow, FormLocationInfo info)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == formToShow.GetType())
                {
                    form.Activate();
                    return false;
                }
            }

            formToShow.StartPosition = FormStartPosition.Manual;
            formToShow.Location = new Point(info.Location.X + (info.Width - formToShow.Width) / 2, info.Location.Y + (info.Height - formToShow.Height) / 2);
            formToShow.Show();

            return true;
        }
    }
}
