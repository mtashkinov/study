using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedWorld.HasName
{
    internal interface IHasName
    {
        string Name { get; }

        void ToConsole();
    }
}
