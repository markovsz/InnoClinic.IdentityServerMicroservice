using Microsoft.EntityFrameworkCore;

namespace Api.Extensions
{
    public static class DbContextOptionsBuilderExtensions
    {
        public static DbContextOptionsBuilder ConfigureDb(this DbContextOptionsBuilder builder, IConfiguration configuration) => 
            builder.UseSqlServer(configuration.GetConnectionString("dbConnection"),
                b => b.MigrationsAssembly("Infrastructure"));
    }
}
