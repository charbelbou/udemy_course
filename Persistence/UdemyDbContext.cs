using Microsoft.EntityFrameworkCore;
using udemy.Models;

namespace udemy.Persistence
{
    // Custom DbContext
    public class UdemyDbContext : DbContext
    {
        // Makes Db set
        public DbSet<Make> Makes { get; set; }
        // Features Db set
        public DbSet<Feature> Features { get; set; }
        // Models Db Set
        public DbSet<Model> Models { get; set; }
        // Vehicles Db set
        public DbSet<Vehicle> Vehicles { get; set; }
    
        public UdemyDbContext(DbContextOptions<UdemyDbContext> options) : base(options){

        }

        // Used to create composite primary key for Vehicle Feature
        // Key consists of the Vehicle ID and Feature ID
        // Could not be done with data annotations, had to be done through Fluent API
        protected override void OnModelCreating(ModelBuilder modelBuilder){
            modelBuilder.Entity<VehicleFeature>().HasKey(vf=> new {vf.VehicleId,vf.FeatureId});
        }
    }
}