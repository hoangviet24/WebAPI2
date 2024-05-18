using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAnimals.Migrations
{
    /// <inheritdoc />
    public partial class Init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Animals_Animal_Id",
                table: "Images");

            migrationBuilder.AlterColumn<int>(
                name: "Animal_Id",
                table: "Images",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Animals_Animal_Id",
                table: "Images",
                column: "Animal_Id",
                principalTable: "Animals",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Animals_Animal_Id",
                table: "Images");

            migrationBuilder.AlterColumn<int>(
                name: "Animal_Id",
                table: "Images",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Animals_Animal_Id",
                table: "Images",
                column: "Animal_Id",
                principalTable: "Animals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
