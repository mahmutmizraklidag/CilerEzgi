using CilerEzgi.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CilerEzgi.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<WhyChooseUs> WhyChooseUs { get; set; }
        public DbSet<Pricing> Pricings { get; set; }
        public DbSet<ContactForm> ContactForms { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<About> Abouts { get; set; }
        public DbSet<SiteSetting> SiteSettings { get; set; }
        public DbSet<Policies> Policies { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 1,
                Username = "admin",
                password = "xyz123456"
            });
        }

    }
}
