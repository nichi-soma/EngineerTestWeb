using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace EngineerTest.Data.Models
{
    class DBContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
    {
        public ApplicationContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            optionsBuilder.UseSqlServer(@"Server = NIS-PC093\SQLEXPRESS; Database = TMDB; Integrated Security=True; Trusted_Connection = True; MultipleActiveResultSets = true");

            return new ApplicationContext(optionsBuilder.Options);
        }
    }
}
