using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace PH.Data;

public class IdentityUserContextFactory : IDesignTimeDbContextFactory<IdentityUserContext>
{
    public IdentityUserContext CreateDbContext(string[] args)
    {
        // Build configuration from appsettings.json
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Path.Combine( Directory.GetCurrentDirectory() ,"../../Presentation/","PH.Web"))
            .AddJsonFile("appsettings.json")
            .Build();

        // Create DbContextOptionsBuilder and set the connection string
        var optionsBuilder = new DbContextOptionsBuilder<IdentityUserContext>();
        var connectionString = configuration.GetConnectionString("IdentityConnection");
        optionsBuilder.UseSqlServer(connectionString);
        return 
            new IdentityUserContext(optionsBuilder.Options);
    }
}