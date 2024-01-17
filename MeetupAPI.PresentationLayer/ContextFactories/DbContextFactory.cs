using MeetupAPI.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MeetupAPI.PresentationLayer.ContextFactories
{
    public class DbContextFactory : IDesignTimeDbContextFactory<MeetupDatabaseContext>
    {
        public MeetupDatabaseContext CreateDbContext(string[] args)
        {
            var configurationBuilder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

            var optionsBuilder = new DbContextOptionsBuilder<MeetupDatabaseContext>()
                .UseSqlServer(configurationBuilder.GetConnectionString("DefaultConnection"), optBldr => optBldr.MigrationsAssembly("MeetupAPI.PresentationLayer"));

            return new MeetupDatabaseContext(optionsBuilder.Options);
        }
    }
}
