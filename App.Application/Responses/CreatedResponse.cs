namespace App.Application.Responses;

public record CreatedResponse<TResult> : OkResponse<TResult>
{
    public CreatedResponse(TResult Data) : base(Data) => this.StatusCode = 201;
}
