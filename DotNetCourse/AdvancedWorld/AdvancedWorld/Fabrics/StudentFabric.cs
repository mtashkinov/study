using AdvancedWorld.HasName;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedWorld.Fabrics
{
    internal sealed class StudentFabric : IHumanFabric
    {
        Random rnd = new Random();
        public Human CreateHuman()
        {
            return new Student(NamesHelper.GenerateName(Sex.Man),  NamesHelper.GeneratePatronymic(Sex.Man));
        }
    }
}
