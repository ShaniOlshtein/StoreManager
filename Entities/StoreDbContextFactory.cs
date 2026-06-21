using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Entities;

public class StoreDbContextFactory : IDesignTimeDbContextFactory<StoreDbContext>
{
    public StoreDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<StoreDbContext>();
        optionsBuilder.UseMySql(
            "Server=mysql-20638794-rivkakatz200-41fb.i.aivencloud.com;Port=13227;Database=defaultdb;User=avnadmin;Password=AVNS_x5Ne9hSMqBGGNyyldi8;SslMode=Required;",
            new MySqlServerVersion(new Version(8, 0, 0))
        );
        return new StoreDbContext(optionsBuilder.Options);
    }
}
