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
        public DbSet<Image> Images { get; set; }    
        public DbSet<AnimalImage> AnimalImages { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Animal - Image
            modelBuilder.Entity<AnimalImage>().HasOne(ai => ai.Animal)
                .WithMany(ai => ai.AnimalImage)
                .HasForeignKey(ai => ai.Animal_Id);
            modelBuilder.Entity<AnimalImage>().HasOne(ai => ai.Image)
                .WithMany(ai => ai.AnimalImages)
                .HasForeignKey(ai => ai.Image_Id);
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