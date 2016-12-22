using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Films.Views
{
    interface IEditView
    {
        event EventHandler<Dictionary<String, String>> EditStarted;
        void SetFilmInfo(String name, String country, String year);
        void Close();
    }
}
