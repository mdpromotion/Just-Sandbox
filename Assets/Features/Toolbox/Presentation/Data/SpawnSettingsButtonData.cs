using Feature.Toolbox.Presentation;

public readonly struct SpawnSettingsButtonData
{
    public readonly SpawnSettings Type;
    public readonly int Value;

    public SpawnSettingsButtonData(SpawnSettings type, int value)
    {
        Type = type;
        Value = value;
    }
}
