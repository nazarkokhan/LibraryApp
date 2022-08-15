using System;
using System.Collections.Generic;

namespace LibraryApp.Core.ResultModel.Abstraction;

public interface IResult
{
    IReadOnlyCollection<string> Messages { get; }

    bool Success { get; }

    Exception? Exception { get; }
}