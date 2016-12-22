using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Films.Controllers
{
    public class FormLocationWithFilmInfo : EventArgs
    {
        public FormLocationWithFilmInfo(FormLocationInfo location, String filmName, String filmCountry, String filmYear)
        {
            Location = location;
            FilmName = filmName;
            FilmCountry = filmCountry;
            FilmYear = filmYear;
        }
        public FormLocationInfo Location { get; }
        public String FilmName { get; }

        public String FilmCountry { get; }

        public String FilmYear { get; }
    }
}
