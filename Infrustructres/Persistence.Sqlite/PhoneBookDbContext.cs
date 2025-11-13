using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Sqlite.Configurations;

namespace Persistence.Sqlite
{
    public class PhoneBookDbContext : DbContext
    {
        public PhoneBookDbContext(DbContextOptions options): base(options)
        { 
        }

        public DbSet<PhoneBook> PhoneBooks { get; set; }

        public DbSet<Row> Rows { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new PhoneBookConfiguration());
            modelBuilder.ApplyConfiguration(new RowConfiguration());
        }
    }
}
