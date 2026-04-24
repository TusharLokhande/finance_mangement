using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Added_User : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EmailId",
                table: "Users",
                newName: "Email");

            migrationBuilder.AddColumn<string>(
                name: "AzureOid",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Expenses",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_Category_Amount",
                table: "Expenses",
                columns: new[] { "Category", "Amount" });

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_UserId_IsActive",
                table: "Expenses",
                columns: new[] { "UserId", "IsActive" });

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Users_UserId",
                table: "Expenses",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Users_UserId",
                table: "Expenses");

            migrationBuilder.DropIndex(
                name: "IX_Expenses_Category_Amount",
                table: "Expenses");

            migrationBuilder.DropIndex(
                name: "IX_Expenses_UserId_IsActive",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "AzureOid",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Expenses");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Users",
                newName: "EmailId");
        }
    }
}
