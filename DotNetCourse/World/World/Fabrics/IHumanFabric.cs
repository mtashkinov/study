using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace World.Fabrics
{
    internal interface IHumanFabric
    {
        Human CreateHuman(Sex sex);
    }
}
