using Microsoft.EntityFrameworkCore;
using WebCalculator.Service.Model;

namespace WebCalculator.Model
{
    public class ApplicationDbContext : DbContext
    {
        private const string connectionString = "server=localhost;database=calculator;user=root;password=Acce$$12##vgd";

      

        public DbSet<User> Users { get; set; }
        public DbSet<CardInfo> CardInfo { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
           
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {         
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.UserName).IsRequired();
                entity.Property(e => e.Email).IsRequired();
                entity.Property(e => e.Password).IsRequired();
                entity.HasIndex(e => e.UserName).IsUnique();
            });
            modelBuilder.Entity<CardInfo>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.LastFourDigit).IsRequired();
                entity.Property(e => e.ExpiryYear).IsRequired();
                entity.Property(e => e.ExpiryMonth).IsRequired();             

            });
           
        }
    }
}
