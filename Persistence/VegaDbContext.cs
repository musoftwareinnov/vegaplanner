using Microsoft.EntityFrameworkCore;
using vega.Core.Models;
using vega.Core.Models.States;

namespace vega.Persistence
{
    public class VegaDbContext : DbContext
    {
        
        public DbSet<Make> Makes { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Vehicle> Vehicle { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<StateInitialiser> StateInitialisers { get; set; }
        public DbSet<StateInitialiserState> StateInitialiserState { get; set; }
        public DbSet<PlanningApp> PlanningApps { get; set; }
        public DbSet<PlanningAppState> PlanningAppState { get; set; }
        public DbSet<Drawing> Drawings { get; set; }
        public DbSet<StateStatus> StateStatus { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Address> CustomerAddress { get; set; }
        public DbSet<Address> DevelopmentAddress { get; set; }

        public VegaDbContext(DbContextOptions<VegaDbContext> options) : base (options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
                modelBuilder.Entity<VehicleFeature>().HasKey(vf => new { vf.VehicleId, vf.FeatureId });
                modelBuilder.Entity<Vehicle>().OwnsOne(c => c.Contact);

                modelBuilder.Entity<PlanningApp>().OwnsOne(c => c.Developer);
                modelBuilder.Entity<PlanningApp>().OwnsOne(c => c.DevelopmentAddress);

                modelBuilder.Entity<Customer>().OwnsOne(c => c.CustomerAddress);
                modelBuilder.Entity<Customer>().OwnsOne(c => c.CustomerContact);
                

                // modelBuilder.Entity<PlanningAppState>()
                //     .HasOne(b => b.PlanningApp)
                //     .WithMany(a => a.PlanningAppStates)
                //     .IsRequired()
                //     .OnDelete(DeleteBehavior.Cascade);       

        }
    }
}