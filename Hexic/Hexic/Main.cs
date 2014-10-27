/* Mikhail Tashkinov 2014
 * Hexic game
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexic
{
    static class HexicGame
    {        
        static void Main(string[] args)
        {
            Field field = new Field();
            Algorithm algorithm = new Algorithm();
            System.Console.WriteLine(algorithm.play(field));
        }
    }
}
