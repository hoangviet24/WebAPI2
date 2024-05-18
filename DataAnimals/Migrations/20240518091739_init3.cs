using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAnimals.Migrations
{
    /// <inheritdoc />
    public partial class init3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Animals_Animal_Id",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_Animal_Id",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "Animal_Id",
                table: "Images");

            migrationBuilder.RenameColumn(
                name: "IamgeId",
                table: "Animals",
                newName: "AnimalImage_Id");

            migrationBuilder.AddColumn<int>(
                name: "AnimalImage_Id",
                table: "Images",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AnimalImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Animal_Id = table.Column<int>(type: "int", nullable: true),
                    Image_Id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimalImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnimalImages_Animals_Animal_Id",
                        column: x => x.Animal_Id,
                        principalTable: "Animals",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AnimalImages_Images_Image_Id",
                        column: x => x.Image_Id,
                        principalTable: "Images",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnimalImages_Animal_Id",
                table: "AnimalImages",
                column: "Animal_Id");

            migrationBuilder.CreateIndex(
                name: "IX_AnimalImages_Image_Id",
                table: "AnimalImages",
                column: "Image_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnimalImages");

            migrationBuilder.DropColumn(
                name: "AnimalImage_Id",
                table: "Images");

            migrationBuilder.RenameColumn(
                name: "AnimalImage_Id",
                table: "Animals",
                newName: "IamgeId");

            migrationBuilder.AddColumn<int>(
                name: "Animal_Id",
                table: "Images",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_Animal_Id",
                table: "Images",
                column: "Animal_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Animals_Animal_Id",
                table: "Images",
                column: "Animal_Id",
                principalTable: "Animals",
                principalColumn: "Id");
        }
    }
}
