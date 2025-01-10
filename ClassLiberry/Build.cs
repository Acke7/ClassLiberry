using ClassLiberry;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace MyClassLibrary
{
    public class Build
    {
        public ApplicationDbContext BuildDb()
        {
            // Makes it possible to connect to appsettings.json
            var builder = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json", true, true);
            var config = builder.Build();

            // Create options & connectionstring variables(boiler plate code).
            var options = new DbContextOptionsBuilder<ApplicationDbContext>();
            var connectionString = config.GetConnectionString("DefaultConnection");
            options.UseSqlServer(connectionString);

            // Om inte databasen redan finns... så skapas den nu.
            using (var dbContext = new ApplicationDbContext(options.Options))
            {
                dbContext.Database.Migrate();
            }

            var dbContextReturned = new ApplicationDbContext(options.Options);
            return dbContextReturned;
        }
    }
}