using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class Form1 : Form
    {
        private Intersect.Set set1 = null, set2 = null;
        Random rnd = new System.Random();
        private const int size = 500;
        private const int padding = 50;
        public Form1()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            set1 = getSet();
            set2 = getSet();
            label1.Text = (Intersect.intersect(set1, set2) == Intersect.Set.NoPoint) ? "no" : "yes";
            this.Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if ((set1 != null) && (set2 != null))
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                paintSet(Color.Red, e, set1);
                paintSet(Color.Green, e, set2);
            }
        }

        private Intersect.Set getSet()
        {
            int type = rnd.Next(0, 4);

            switch (type)
            {
                case 0:
                    return Intersect.Set.NewPoint(rnd.Next(padding, size - padding), rnd.Next(padding, size - padding));
                case 1:
                    return Intersect.Set.NewLine((double)rnd.Next(1, 100)/10, rnd.Next(0, size/2));
                case 2:
                    return Intersect.Set.NewVerticalLine(rnd.Next(padding, size - padding));
                case 3:
                    return Intersect.Set.NewLineSegment(Tuple.Create<double, double>(rnd.Next(padding, size - padding), rnd.Next(padding, size - padding)),
                                                        Tuple.Create<double, double>(rnd.Next(padding, size - padding), rnd.Next(padding, size - padding)));
                default:
                    return Intersect.Set.NoPoint;
            }
        }

        private void paintSet(Color color, PaintEventArgs e, Intersect.Set set)
        {
            if (set.IsPoint)
            {
                Point point = new Point((int)((Intersect.Set.Point)set).Item1, (int)((Intersect.Set.Point)set).Item2);
                e.Graphics.DrawEllipse(new Pen(color), new Rectangle((int)((Intersect.Set.Point)set).Item1, (int)((Intersect.Set.Point)set).Item2, 10, 10));
            }

            if (set.IsLine)
            {
                Point point1 = new Point(0, (int)((Intersect.Set.Line)set).Item2);
                Point point2 = new Point(size, (int)((Intersect.Set.Line)set).Item1 * size + (int)((Intersect.Set.Line)set).Item2);
                e.Graphics.DrawLine(new Pen(color), point1, point2);
            }

            if (set.IsVerticalLine)
            {
                Point point1 = new Point((int)((Intersect.Set.VerticalLine)set).Item, 0);
                Point point2 = new Point((int)((Intersect.Set.VerticalLine)set).Item, size);
                e.Graphics.DrawLine(new Pen(color), point1, point2);
            }

            if (set.IsLineSegment)
            {
                Point point1 = new Point((int)((Intersect.Set.LineSegment)set).Item1.Item1, (int)((Intersect.Set.LineSegment)set).Item1.Item2);
                Point point2 = new Point((int)((Intersect.Set.LineSegment)set).Item2.Item1, (int)((Intersect.Set.LineSegment)set).Item2.Item2);
                e.Graphics.DrawLine(new Pen(color), point1, point2);
            }
        }
    }
}
