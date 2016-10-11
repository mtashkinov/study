using AdvancedWorld.HasName;

namespace AdvancedWorld
{
    internal interface IGod
    {
        Human CreateHuman();
        IHasName MakeCouple(Human human1, Human human2);
    }
}
