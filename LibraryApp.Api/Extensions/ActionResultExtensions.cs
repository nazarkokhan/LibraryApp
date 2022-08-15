namespace LibraryApp.Api.Extensions;

using ActionResults;
using Core.ResultModel.Abstraction;
using Microsoft.AspNetCore.Mvc;

public static class ActionResultExtensions
{
    public static IActionResult ToActionResult(this IResult result)
        => new ErrorableActionResult(result);
}