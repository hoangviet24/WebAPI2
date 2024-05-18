using DataAnimals.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAnimals.Data
{
    internal class DbInitalizer
    {
        private ModelBuilder modelBuilder;

        public DbInitalizer(ModelBuilder modelBuilder)
        {
            this.modelBuilder = modelBuilder;
        }

        public void Seed()
        {
            modelBuilder.Entity<Animal>(a =>
            {
                a.HasData(new Animal
                {
                    Id = 1,
                    Name = "Tiger",
                    Description = "Một loại hung dữ",
                    AgeAvg = (float)12.5,
                    CatergoryAnimal_Id = 1,
                });
                a.HasData(new Animal
                {
                    Id = 2,
                    Name = "Bò",
                    Description = "Một loại ăn cỏ",
                    AgeAvg = (float)17.5,
                    CatergoryAnimal_Id = 2,
                });
                a.HasData(new Animal
                {
                    Id = 3,
                    Name = "Mèo",
                    Description = "thú nuôi trong nhà",
                    AgeAvg = (float)12.5,
                    CatergoryAnimal_Id = 3,
                });
            });
            modelBuilder.Entity<Category>(c =>
            {
                c.HasData(new Category
                {
                    Id = 1,
                    Name = "Ăn thịt",
                    CatergoryAnimal_Id = 1,
                });
                c.HasData(new Category
                {
                    Id = 2,
                    Name = "Ăn cỏ",
                    CatergoryAnimal_Id = 2,
                });
                c.HasData(new Category
                {
                    Id = 3,
                    Name = "Ăn thịt",
                    CatergoryAnimal_Id = 3,
                });
            });
            modelBuilder.Entity<AnimalCategory>(ac =>
            {
                ac.HasData(new AnimalCategory
                {
                    Id = 1,
                    Category_Id = 1,
                    Animal_Id = 1,
                });
                ac.HasData(new AnimalCategory
                {
                    Id = 2,
                    Category_Id = 2,
                    Animal_Id = 2,
                });
                ac.HasData(new AnimalCategory
                {
                    Id = 3,
                    Category_Id = 3,
                    Animal_Id = 3,
                });
            });
        }
    }
}