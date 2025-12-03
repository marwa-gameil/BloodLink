using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using App.Domain.Responses;

namespace App.API.Controllers;

[ApiController, Route("Api/[controller]")]
public abstract class ApiBaseController : ControllerBase
{
    protected ActionResult<TResult> HandleResult<TResult>(Result<TResult> result)
    {
        if(!result.Succeeded) return ProcessError(result.Response);
        return result.Response.StatusCode switch
        {
            StatusCodes.Status200OK => Ok(result.Data),
            StatusCodes.Status201Created => StatusCode(StatusCodes.Status201Created, result.Data),
            _ => StatusCode(result.Response.StatusCode)
        };
    }

    protected ActionResult HandleResult(Result result)
    {
        if (!result.Succeeded) return ProcessError(result.Response);
        return result.Response.StatusCode switch
        {
            StatusCodes.Status201Created => StatusCode(StatusCodes.Status201Created),
            StatusCodes.Status204NoContent => NoContent(),
            _ => StatusCode(result.Response.StatusCode)
        };
    }

    protected ActionResult ProcessError(Response response)
    {
        return response.StatusCode switch
        {
            StatusCodes.Status400BadRequest => BadRequest(response),
            StatusCodes.Status401Unauthorized => Unauthorized(),
            StatusCodes.Status403Forbidden => Forbid(response.Message),
            StatusCodes.Status404NotFound => NotFound(response),
            StatusCodes.Status500InternalServerError => StatusCode(response.StatusCode, response.Message),
            _ => StatusCode(response.StatusCode, response.Message)
        };
    }
}
