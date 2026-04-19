using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangesInConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExpenseTagMapping_Expenses_ExpenseId",
                table: "ExpenseTagMapping");

            migrationBuilder.DropForeignKey(
                name: "FK_ExpenseTagMapping_Tags_TagId",
                table: "ExpenseTagMapping");

            migrationBuilder.AddForeignKey(
                name: "FK_ExpenseTagMapping_Expenses_ExpenseId",
                table: "ExpenseTagMapping",
                column: "ExpenseId",
                principalTable: "Expenses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExpenseTagMapping_Tags_TagId",
                table: "ExpenseTagMapping",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExpenseTagMapping_Expenses_ExpenseId",
                table: "ExpenseTagMapping");

            migrationBuilder.DropForeignKey(
                name: "FK_ExpenseTagMapping_Tags_TagId",
                table: "ExpenseTagMapping");

            migrationBuilder.AddForeignKey(
                name: "FK_ExpenseTagMapping_Expenses_ExpenseId",
                table: "ExpenseTagMapping",
                column: "ExpenseId",
                principalTable: "Expenses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ExpenseTagMapping_Tags_TagId",
                table: "ExpenseTagMapping",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id");
        }
    }
}
