using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace World.Humans
{
    internal sealed class Randomizer
    {
        private static Random rnd = new Random();

        internal static int GetRandomStudentAge()
        {
            return rnd.Next(5, 25);
        }

        internal static int GetRandomParentAge()
        {
            return rnd.Next(20, 50);
        }

        internal static int GetRandomParentAgeFromStudent(int studentAge)
        {
            return studentAge + rnd.Next(20, 30);
        }

        internal static Sex GetRandomSex()
        {
            Array values = Enum.GetValues(typeof(Sex));
            return (Sex)values.GetValue(rnd.Next(values.Length));
        }
    }
}
