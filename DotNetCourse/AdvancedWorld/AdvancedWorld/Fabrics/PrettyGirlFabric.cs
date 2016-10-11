using AdvancedWorld.HasName;
using System;

namespace AdvancedWorld.Fabrics
{
    internal sealed class PrettyGirlFabric : IHumanFabric
    {
        Random rnd = new Random();
        public Human CreateHuman()
        {
            return new PrettyGirl(NamesHelper.GenerateName(Sex.Woman));
        }
    }
}
