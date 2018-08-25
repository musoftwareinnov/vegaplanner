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

        public VegaDbContext(DbContextOptions<VegaDbContext> options) : base (options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
                modelBuilder.Entity<VehicleFeature>().HasKey(vf => new { vf.VehicleId, vf.FeatureId });
                modelBuilder.Entity<Vehicle>().OwnsOne(c => c.Contact);

                // modelBuilder.Entity<PlanningAppState>()
                //     .HasOne(b => b.PlanningApp)
                //     .WithMany(a => a.PlanningAppStates)
                //     .IsRequired()
                //     .OnDelete(DeleteBehavior.Cascade);       



                    // modelBuilder.Entity<PlanningAppState>()
                    //     .HasOne(b => b.PlanningApp)
                    //     .WithMany(p => p.PlanningAppStates)
                    //    // .HasForeignKey(e => e.PlanningAppId)
                    //     .OnDelete(DeleteBehavior.Restrict);

                // modelBuilder.Entity<PlanningApp>()
                //     .HasMany(p => p.PlanningAppStates)
                //     .WithOne(t => t.PlanningApp);
                // modelBuilder.Entity<InitialiseState>()
                //     .HasIndex(s => s.OrderId);

                    //.IsUnique();

                //modelBuilder.Entity<StateInitialiserState>().HasKey(vf => new { vf.OrderId });
        }
    }
}