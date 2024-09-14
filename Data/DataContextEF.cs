using Learning_Dotnet.Models;
using Microsoft.EntityFrameworkCore;

namespace Learning_Dotnet.Data
{
    public class DataContextEF : DbContext
    {
        private readonly string? _connectionString;

        public DataContextEF(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserSalary> UserSalary { get; set; }
        public virtual DbSet<UserJobInfo> UserJobInfo { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    _connectionString,
                    options => options.EnableRetryOnFailure()
                );
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("TutorialAppSchema");

            modelBuilder.Entity<User>()
                .ToTable("Users", "TutorialAppSchema")
                .HasKey(u => u.UserId);

            modelBuilder.Entity<UserSalary>()
                .HasKey(u => u.UserId);

            modelBuilder.Entity<UserJobInfo>()
                .HasKey(u => u.UserId);
        }
    }
}