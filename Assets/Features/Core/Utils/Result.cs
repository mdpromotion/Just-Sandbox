#nullable enable
public sealed class Result
{
    public bool IsSuccess { get; }
    public string? Error { get; }
    private static readonly Result _cachedSuccess = new(true);
    private Result(bool isSuccess, string? error = null)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result Success() => _cachedSuccess;
    public static Result Failure(string Error) => new(false, Error);
}
public sealed class Result<T>
{
    public bool IsSuccess { get; }
    public string? Error { get; }
    public T? Value { get; }

    private Result(T value)
    {
        IsSuccess = true;
        Value = value;
    }
    private Result(string error)
    {
        IsSuccess = false;
        Error = error;
    }

    public static Result<T> Success(T value) => new(value);
    public static Result<T> Failure(string error) => new(error);
}
