using Feature.Toolbox.Presentation;

public readonly struct SpawnButtonData
{
    public readonly int Id;
    public readonly SpawnCategory Category;

    public SpawnButtonData(int id, SpawnCategory category)
    {
        Id = id;
        Category = category;
    }
}