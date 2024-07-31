using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodOrderApp.Migrations
{
    /// <inheritdoc />
    public partial class AddTabletoContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItem_Carts_CartId",
                table: "CartItem");

            migrationBuilder.DropForeignKey(
                name: "FK_CartItem_Dishes_DishId",
                table: "CartItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CartItem",
                table: "CartItem");

            migrationBuilder.RenameTable(
                name: "CartItem",
                newName: "cartItems");

            migrationBuilder.RenameIndex(
                name: "IX_CartItem_DishId",
                table: "cartItems",
                newName: "IX_cartItems_DishId");

            migrationBuilder.RenameIndex(
                name: "IX_CartItem_CartId",
                table: "cartItems",
                newName: "IX_cartItems_CartId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_cartItems",
                table: "cartItems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_cartItems_Carts_CartId",
                table: "cartItems",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_cartItems_Dishes_DishId",
                table: "cartItems",
                column: "DishId",
                principalTable: "Dishes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cartItems_Carts_CartId",
                table: "cartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_cartItems_Dishes_DishId",
                table: "cartItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_cartItems",
                table: "cartItems");

            migrationBuilder.RenameTable(
                name: "cartItems",
                newName: "CartItem");

            migrationBuilder.RenameIndex(
                name: "IX_cartItems_DishId",
                table: "CartItem",
                newName: "IX_CartItem_DishId");

            migrationBuilder.RenameIndex(
                name: "IX_cartItems_CartId",
                table: "CartItem",
                newName: "IX_CartItem_CartId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CartItem",
                table: "CartItem",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItem_Carts_CartId",
                table: "CartItem",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CartItem_Dishes_DishId",
                table: "CartItem",
                column: "DishId",
                principalTable: "Dishes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
