namespace World
{
    internal interface IGod
    {
        Human CreateHuman();
        Human CreateHuman(Sex sex);
        Human CreatePair(Human human);
    }
}
