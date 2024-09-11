using Microsoft.EntityFrameworkCore;

namespace ContactManagerWebApplication.Models
{
    public class ApplicationDbContext: DbContext
    {
        public DbSet<PersonEntity> Persons => Set<PersonEntity>();

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :base(options)
        {
            Database.EnsureCreated();
        }

    }
}
