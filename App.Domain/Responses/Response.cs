namespace App.Domain.Responses;

public record Response(int StatusCode, string Message)
{
    public static Response Valid(int statusCode) => new(statusCode, null!);
}
