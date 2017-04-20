namespace BeerTracker.Data
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models.DataModels;
    using Models.DataModels.UserModels;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()   
            : base("BeerTracker")
        {
            
        }

        public DbSet<Location> Locations { get; set; }

        public DbSet<RegularUser> RegularUsers { get; set; }

        public DbSet<Beer> Beers { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Beer>().HasKey(b => b.Id);
            modelBuilder.Entity<Beer>().Property(b => b.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Beer>().HasRequired(b => b.Location).WithRequiredPrincipal(l => l.Beer);

            base.OnModelCreating(modelBuilder);

        }
    }
}
