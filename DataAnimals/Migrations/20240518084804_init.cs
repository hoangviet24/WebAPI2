﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAnimals.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Animals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AgeAvg = table.Column<float>(type: "real", nullable: false),
                    CatergoryAnimal_Id = table.Column<int>(type: "int", nullable: false),
                    IamgeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CatergoryAnimal_Id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileExtension = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Size = table.Column<long>(type: "bigint", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Animal_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_Animals_Animal_Id",
                        column: x => x.Animal_Id,
                        principalTable: "Animals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnimalCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Animal_Id = table.Column<int>(type: "int", nullable: true),
                    Category_Id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimalCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnimalCategories_Animals_Animal_Id",
                        column: x => x.Animal_Id,
                        principalTable: "Animals",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AnimalCategories_Categories_Category_Id",
                        column: x => x.Category_Id,
                        principalTable: "Categories",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Animals",
                columns: new[] { "Id", "AgeAvg", "CatergoryAnimal_Id", "Description", "IamgeId", "Name" },
                values: new object[,]
                {
                    { 1, 12.5f, 1, "Một loại hung dữ", 0, "Tiger" },
                    { 2, 17.5f, 2, "Một loại ăn cỏ", 0, "Bò" },
                    { 3, 12.5f, 3, "thú nuôi trong nhà", 0, "Mèo" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CatergoryAnimal_Id", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Ăn thịt" },
                    { 2, 2, "Ăn cỏ" },
                    { 3, 3, "Ăn thịt" }
                });

            migrationBuilder.InsertData(
                table: "AnimalCategories",
                columns: new[] { "Id", "Animal_Id", "Category_Id" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 2 },
                    { 3, 3, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnimalCategories_Animal_Id",
                table: "AnimalCategories",
                column: "Animal_Id");

            migrationBuilder.CreateIndex(
                name: "IX_AnimalCategories_Category_Id",
                table: "AnimalCategories",
                column: "Category_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Images_Animal_Id",
                table: "Images",
                column: "Animal_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnimalCategories");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Animals");
        }
    }
}
