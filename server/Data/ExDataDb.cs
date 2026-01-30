using ExData.Models;
using Microsoft.EntityFrameworkCore;

namespace ExData.Data
{
    public class ExDataDb : DbContext
    {
        private string _dbNow = "SYSUTCDATETIME()";

        public ExDataDb(DbContextOptions<ExDataDb> options)
            : base(options) { }

        public DbSet<Exercise> Exercises => Set<Exercise>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Exercise>()
                .Property(e => e.CreatedAt)
                .HasDefaultValueSql(_dbNow)
                .ValueGeneratedOnAdd();

            modelBuilder
                .Entity<Exercise>()
                .Property(e => e.UpdatedAt)
                .HasDefaultValueSql(_dbNow)
                .ValueGeneratedOnAddOrUpdate();

            modelBuilder.Entity<Exercise>().HasQueryFilter(e => e.DeletedAt == null);
        }
    }
}
