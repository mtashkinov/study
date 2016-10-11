using AdvancedWorld.HasName;
using System;

namespace AdvancedWorld.Fabrics
{
    internal sealed class GirlFabric : IHumanFabric
    {
        Random rnd = new Random();
        public Human CreateHuman()
        {
            return new Girl(NamesHelper.GenerateName(Sex.Woman));
        }
    }
}
