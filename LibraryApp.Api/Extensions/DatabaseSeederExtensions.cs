namespace LibraryApp.Api.Extensions;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Other;

public static class DatabaseSeederExtensions
{
    public static IApplicationBuilder UseDatabaseSeeder(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        scope.ServiceProvider.GetRequiredService<IDatabaseSeeder>().SeedAsync().GetAwaiter().GetResult();

        return app;
    }
}