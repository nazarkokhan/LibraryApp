namespace LibraryApp.Api.Controllers.Abstract;

using System.Collections.Generic;

public record ErrorResponse(List<string> Errors);