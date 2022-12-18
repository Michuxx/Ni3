using back_end.Models;
using Microsoft.EntityFrameworkCore;

namespace back_end.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Comment>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Account>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Course>()
                .HasMany(x => x.Comments)
                .WithOne(x => x.Course)
                .HasForeignKey(x => x.CourseId);

            modelBuilder.Entity<Account>()
                .HasMany(x => x.Comments)
                .WithOne(x => x.Account)
                .HasForeignKey(x => x.UserId);
        }
        public DbSet<Course> Courses { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Account> Accounts { get; set; }

    }
}
