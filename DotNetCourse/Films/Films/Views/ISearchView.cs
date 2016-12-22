using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Films.Controllers
{
    interface ISearchView
    {
        event EventHandler<Dictionary<String, String>> SearchStarted;
    }
}
