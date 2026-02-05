
namespace Domain.Abstractions;

public class Result
{
    public bool IsSuccessful { get; }
    public bool HasFailed => !IsSuccessful;
    public string? ErrorMessage { get; }

    public Result(bool isSuccessful, string? errorMessage = null)
    {
        IsSuccessful = isSuccessful;
        ErrorMessage = errorMessage;
    }

    public static Result Ok() => new(true);
    public static Result Fail(string errorMessage) => new(false, errorMessage);
    public static Result<T> Ok<T>(T? value) => new(value, true, null);
    public static Result<T> Fail<T>(string errorMessage) => new(default, false, errorMessage);
    public static Result<T> FromValue<T>(T? value) => value != null ? Ok(value) : Fail<T>("El valor no puede ser nulo");
}

public class Result<T> : Result
{
    public T? Value { get; }
    protected internal Result(T? value, bool isSuccessful, string? errorMessage) : base(isSuccessful, errorMessage)
    {
        Value = value;
    }

    public static implicit operator Result<T>(T? value) => FromValue(value);
    public static implicit operator T?(Result<T> response) => FromValue(response.Value);
}