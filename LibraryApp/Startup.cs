using LibraryApp.BLL.Services;
using LibraryApp.BLL.Services.Abstraction;
using LibraryApp.DAL.EF;
using LibraryApp.DAL.Entities;
using LibraryApp.DAL.Repository;
using LibraryApp.DAL.Repository.Abstraction;
using LibraryApp.Extensions;
using LibraryApp.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace LibraryApp
{
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
                .AddIdentity<User, Role>(options => options.ConfigurePassword())
                .AddUserManager<UserManager<User>>()
                .AddEntityFrameworkStores<LibContext>()
                .AddDefaultTokenProviders();

            services
                .AddDbContext<LibContext>(options => options
                    .UseSqlServer(Configuration.GetConnectionString($"{nameof(LibContext)}"))
                    .UseLoggerFactory(LoggerFactory.Create(lb => lb.AddConsole()))
                );

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
                .AddControllers(options => options.Filters.Add<ErrorableResultFilterAttribute>());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}