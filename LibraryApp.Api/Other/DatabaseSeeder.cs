namespace LibraryApp.Api.Other;

using System;
using System.Threading.Tasks;
using LibraryApp.DAL.EF;
using Microsoft.EntityFrameworkCore;

public class DatabaseSeeder : IDatabaseSeeder
{
    private readonly LibContext _context;

    public DatabaseSeeder(LibContext context)
    {
        _context = context;
    }

    public async Task SeedAsync()
    {
        _context.Database.SetCommandTimeout(TimeSpan.FromMinutes(10));
        await _context.Database.MigrateAsync();
    }
}