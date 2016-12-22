using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Films.Models
{
    internal interface IFilmsInformer
    {
        void InformSearchFinished(DataTable data);
        void InformDataLoaded(DataTable data);
        void InformDeleteFinished(DataTable data);
        void InformUpdateFinished(DataTable data);
    }
}
