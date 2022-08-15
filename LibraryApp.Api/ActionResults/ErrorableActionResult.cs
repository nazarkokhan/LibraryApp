using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Api.ActionResults;

using Core.ResultModel.Abstraction;

public class ErrorableActionResult : ActionResult
{
    public ErrorableActionResult(IResult result)
    {
        Result = result;
    }

    public IResult Result { get; }
}