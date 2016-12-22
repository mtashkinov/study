using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Films.Controllers
{
    public class FormLocationInfo : EventArgs
    {
        public FormLocationInfo(Point location, int width, int height)
        {
            Location = location;
            Width = width;
            Height = height;
        }
        public Point Location { get; }
        public int Width { get; }
        public int Height { get;  }
    }
}
