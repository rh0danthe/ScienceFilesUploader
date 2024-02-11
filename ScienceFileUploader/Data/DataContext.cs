using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ScienceFileUploader.Entities;
using File = ScienceFileUploader.Entities.File;

namespace ScienceFileUploader.Data
{
    public class DataContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public DataContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_configuration.GetConnectionString("DefaultConnection"));
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Result>().HasOne<File>(r => r.File).WithOne(f => f.Result)
                .HasForeignKey<Result>(r => r.FileId).IsRequired();
            modelBuilder.Entity<Value>()
                .HasOne(v => v.File)
                .WithMany(f => f.Values)
                .HasPrincipalKey(f => f.Name)
                .IsRequired();
            modelBuilder.Entity<Value>()
                .HasOne(v => v.File)
                .WithMany(f => f.Values)
                .HasForeignKey(v => v.FileName)
                .IsRequired();
        }
        
        public DbSet<Value> Values { get; set; }
        public DbSet<File> Clients { get; set; }
        public DbSet<Result> Orders { get; set; }
    }
}