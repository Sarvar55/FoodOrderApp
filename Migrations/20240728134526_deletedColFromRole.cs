using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodOrderApp.Migrations
{
    /// <inheritdoc />
    public partial class deletedColFromRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyRole");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompanyRole",
                columns: table => new
                {
                    CompaniesId = table.Column<int>(type: "int", nullable: false),
                    RolesId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyRole", x => new { x.CompaniesId, x.RolesId });
                    table.ForeignKey(
                        name: "FK_CompanyRole_AspNetRoles_RolesId",
                        column: x => x.RolesId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyRole_Companies_CompaniesId",
                        column: x => x.CompaniesId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyRole_RolesId",
                table: "CompanyRole",
                column: "RolesId");
        }
    }
}
