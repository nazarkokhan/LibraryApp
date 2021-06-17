using LibraryApp.ActionResults;
using LibraryApp.Core.ResultModel.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Extensions
{
    public static class ActionResultExtensions
    {
        public static IActionResult ToActionResult(this IResult result)
            => new ErrorableActionResult(result);
    }
}