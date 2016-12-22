using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Films.Controllers
{
    public interface IFilmsView
    {
        event EventHandler<String[]> DeleteStarted;
        event EventHandler LoadStarted;
        event EventHandler<FormLocationWithFilmInfo> EditRequested;
        event EventHandler<FormLocationInfo> SearchRequested;

        void InformDataLoaded(DataTable data);
        void InformSearchFinished(DataTable data);
        void InformDeleteFinished(DataTable data);
        void InformUpdateFinished(DataTable data);
        void EnableMenu();
        void DisableMenu();
    }
}
