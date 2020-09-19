using Microsoft.EntityFrameworkCore.Migrations;

namespace CarParts.Infrastructure.Migrations
{
    public partial class revertChangeSchema2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Carts_CartId",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "CartId",
                table: "Orders",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Carts_CartId",
                table: "Orders",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "CartId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Carts_CartId",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "CartId",
                table: "Orders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Carts_CartId",
                table: "Orders",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "CartId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
