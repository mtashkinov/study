using AdvancedWorld.HasName;
using System;

namespace AdvancedWorld.Fabrics
{
    internal sealed class BotanFabric : IHumanFabric
    {
        Random rnd = new Random();
        public Human CreateHuman()
        {
            var avgMark = 3 + rnd.NextDouble() * 2;
            return new Botan(NamesHelper.GenerateName(Sex.Man), NamesHelper.GeneratePatronymic(Sex.Man));
        }
    }
}
