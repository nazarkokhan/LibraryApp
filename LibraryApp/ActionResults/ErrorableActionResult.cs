using LibraryApp.Core.ResultModel.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.ActionResults
{
    public class ErrorableActionResult : ActionResult
    {
        public ErrorableActionResult(IResult result)
        {
            Result = result;
        }

        public IResult Result { get; }
    }
}