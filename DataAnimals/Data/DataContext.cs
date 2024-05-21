using DataAnimals.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAnimals.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options) { }
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<AnimalCategory> AnimalCategories { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Animal - Category
            modelBuilder.Entity<AnimalCategory>().HasOne(ac => ac.animal)
                .WithMany(ac => ac.AnimalCategory)
                .HasForeignKey(ac => ac.Animal_Id);
            modelBuilder.Entity<AnimalCategory>().HasOne(ac => ac.category)
                .WithMany(ac => ac.AnimalCategory)
                .HasForeignKey(ac => ac.Category_Id);
            new DbInitalizer(modelBuilder).Seed();
        }
    }
}