using LibraryApp.BLL.Services;
using LibraryApp.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace LibraryApp
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            string con = "Server=(localdb)\\mssqllocaldb;Database=librarydb;Trusted_Connection=True;";

            services
                .AddDbContext<LibContext>((sp, options) => options.UseSqlServer(con).UseLoggerFactory(LoggerFactory.Create(lb => lb.AddConsole())))
                .AddScoped<DatabaseInitializer>()
                .AddTransient<AuthorService>()
                .AddTransient<BookService>()
                .AddControllers();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
