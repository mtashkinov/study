using AdvancedWorld.HasName;
using System;

namespace AdvancedWorld.Fabrics
{
    internal sealed class SmartGirlFabric : IHumanFabric
    {
        Random rnd = new Random();
        public Human CreateHuman()
        {
            return new SmartGirl(NamesHelper.GenerateName(Sex.Woman));
        }
    }
}
