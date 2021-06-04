using Microsoft.EntityFrameworkCore;

namespace SecretSanta.Data
{
    public class DbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbContext()
            : base(new DbContextOptionsBuilder<DbContext>().UseSqlite("Data Source=main.db").Options)
        { }

        public DbSet<User> Users => Set<User>();
        public DbSet<Group> Gifts => Set<Group>();
        public DbSet<Group> Groups  => Set<Group>();

    }
}