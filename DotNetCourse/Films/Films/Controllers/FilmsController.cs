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
            filmsView.DeleteStarted += OnFilmsViewInformerDeleteStarted;
            filmsView.LoadStarted += OnFilmsViewInformerLoadStarted;
            filmsView.SearchRequested += OnFilmsViewInformerSearchRequested;
            filmsView.EditRequested += OnFilmsViewEditRequested;
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

        private void OnFilmsViewInformerLoadStarted(object sender, EventArgs e)
        {
            filmsView.DisableMenu();
            Task.Run(() => films.LoadData());
        }

        private void OnFilmsViewInformerDeleteStarted(object sender, String[] filmsNamesToDelete)
        {
            filmsView.DisableMenu();
            Task.Run(() => films.DeleteFilms(filmsNamesToDelete));
        }

        private void OnFilmsViewInformerSearchRequested(object sender, FormLocationInfo info)
        {
            SearchForm form = new SearchForm();
            if (ShowForm(form, info))
            {
                searchView = form;
                searchView.SearchStarted += OnSearchViewSearchStarted;
            }
        }

        private void OnSearchViewSearchStarted(object sender, Dictionary<string, string> searchData)
        {
            filmsView.DisableMenu();
            Task.Run(() => films.Search(searchData));
        }

        private void OnFilmsViewEditRequested(object sender, FormLocationWithFilmInfo info)
        {
            EditForm form = new EditForm();
            if (ShowForm(form, info.Location))
            {
                editView = form;
                editView.SetFilmInfo(info.FilmName, info.FilmCountry, info.FilmYear);
                editView.EditStarted += OnEditViewEditStarted;
            }
        }

        private void OnEditViewEditStarted(object sender, Dictionary<string, string> editInfo)
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
