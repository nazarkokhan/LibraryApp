namespace LibraryApp.Api.Other;

using System.Threading.Tasks;

public interface IDatabaseSeeder
{
    Task SeedAsync();
}