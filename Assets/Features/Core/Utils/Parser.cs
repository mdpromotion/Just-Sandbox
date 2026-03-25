#nullable enable
public static class Parser
{
    public static bool TryGetValue<T>(T? input, out T value) where T : struct
    {
        if (!input.HasValue)
        {
            value = default;
            return false;
        }

        value = input.Value;
        return true;
    }
}
