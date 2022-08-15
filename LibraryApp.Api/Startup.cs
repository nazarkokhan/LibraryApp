namespace LibraryApp.Api;

using BLL.Services;
using BLL.Services.Abstraction;
using DAL.EF;
using DAL.Entities;
using DAL.Repository;
using DAL.Repository.Abstraction;
using Extensions;
using Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Other;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    private IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services
            .AddAuthentication()
            .AddJwtBearer(options => options.JwtBearerOptions());

        services
            .AddSwaggerGen();

        services
            .AddIdentity<User, Role>(options => options.ConfigurePassword())
            .AddUserManager<UserManager<User>>()
            .AddEntityFrameworkStores<LibContext>()
            .AddDefaultTokenProviders();

        services
            .AddDbContext<LibContext>(
                options => options
                    .UseNpgsql(Configuration.GetConnectionString(nameof(LibContext)))
                    .UseLoggerFactory(LoggerFactory.Create(lb => lb.AddConsole())));

        services
            .AddHttpContextAccessor()
            .AddScoped<DataBaseInitializer>()
            .AddScoped<IUnitOfWork, EfUnitOfWork>()
            .AddTransient<IAuthorService, AuthorService>()
            .AddTransient<IBookService, BookService>()
            .AddScoped<IAuthorRepository, AuthorRepository>()
            .AddScoped<IBookRepository, BookRepository>()
            .AddScoped<IUserRepository, UserRepository>()
            .AddScoped<IAccountService, AccountService>()
            .AddScoped<IAdminService, AdminService>()
            .AddSingleton<IEmailService, EmailService>()
            .AddScoped<IDatabaseSeeder, DatabaseSeeder>()
            .AddLogging(builder => builder.AddFile(Configuration.GetLogFileName(), fileSizeLimitBytes: 100_000))
            .AddControllers(options => options.Filters.Add<ErrorableResultFilterAttribute>());
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
            app.UseDeveloperExceptionPage();

        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseDatabaseSeeder();

        app.UseEndpoints(
            endpoints =>
            {
                endpoints.MapGet("/", async context => { await context.Response.WriteAsync($"{env.EnvironmentName} Library app"); });
                endpoints.MapControllers();
            });
    }
}