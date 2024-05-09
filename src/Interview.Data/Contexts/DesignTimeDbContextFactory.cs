using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Interview.Data.Contexts;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<InterviewContext>
{
    public InterviewContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath($"{Directory.GetParent(Directory.GetCurrentDirectory()).Parent}/src/Interview.Api")
            .AddJsonFile("appsettings.json")
            .Build();

        var connectionString = configuration.GetConnectionString("InterviewDb");

        var builder = new DbContextOptionsBuilder<InterviewContext>().UseSqlServer(connectionString);

        return new InterviewContext(builder.Options);
    }
}
