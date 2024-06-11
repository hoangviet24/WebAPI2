﻿// <auto-generated />
using System;
using DataAnimals.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataAnimals.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240611062526_New Contact")]
    partial class NewContact
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DataAnimals.Models.Animal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<float>("AgeAvg")
                        .HasColumnType("real");

                    b.Property<int>("CatergoryAnimal_Id")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Animals");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AgeAvg = 12.5f,
                            CatergoryAnimal_Id = 1,
                            Description = "Một loại hung dữ",
                            Name = "Tiger"
                        },
                        new
                        {
                            Id = 2,
                            AgeAvg = 17.5f,
                            CatergoryAnimal_Id = 2,
                            Description = "Một loại ăn cỏ",
                            Name = "Bò"
                        },
                        new
                        {
                            Id = 3,
                            AgeAvg = 12.5f,
                            CatergoryAnimal_Id = 3,
                            Description = "thú nuôi trong nhà",
                            Name = "Mèo"
                        });
                });

            modelBuilder.Entity("DataAnimals.Models.AnimalCategory", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("Id"));

                    b.Property<int?>("Animal_Id")
                        .HasColumnType("int");

                    b.Property<int?>("Category_Id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Animal_Id");

                    b.HasIndex("Category_Id");

                    b.ToTable("AnimalCategories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Animal_Id = 1,
                            Category_Id = 1
                        },
                        new
                        {
                            Id = 2,
                            Animal_Id = 2,
                            Category_Id = 2
                        },
                        new
                        {
                            Id = 3,
                            Animal_Id = 3,
                            Category_Id = 3
                        });
                });

            modelBuilder.Entity("DataAnimals.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CatergoryAnimal_Id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CatergoryAnimal_Id = 1,
                            Name = "Ăn thịt"
                        },
                        new
                        {
                            Id = 2,
                            CatergoryAnimal_Id = 2,
                            Name = "Ăn cỏ"
                        },
                        new
                        {
                            Id = 3,
                            CatergoryAnimal_Id = 3,
                            Name = "Ăn thịt"
                        });
                });

            modelBuilder.Entity("DataAnimals.Models.Contact", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PhoneNumber")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("Contacts");
                });

            modelBuilder.Entity("DataAnimals.Models.AnimalCategory", b =>
                {
                    b.HasOne("DataAnimals.Models.Animal", "animal")
                        .WithMany("AnimalCategory")
                        .HasForeignKey("Animal_Id");

                    b.HasOne("DataAnimals.Models.Category", "category")
                        .WithMany("AnimalCategory")
                        .HasForeignKey("Category_Id");

                    b.Navigation("animal");

                    b.Navigation("category");
                });

            modelBuilder.Entity("DataAnimals.Models.Animal", b =>
                {
                    b.Navigation("AnimalCategory");
                });

            modelBuilder.Entity("DataAnimals.Models.Category", b =>
                {
                    b.Navigation("AnimalCategory");
                });
#pragma warning restore 612, 618
        }
    }
}
