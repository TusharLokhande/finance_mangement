using InfraStructure.Persistence.Context;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddExpenseStatsFunction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var assembly = typeof(AppDbContext).Assembly;
            var resourceName = "InfraStructure.Persistence.Scripts.GetExpenseStats.sql";
            var resources = assembly.GetManifestResourceNames();
            using var stream = assembly.GetManifestResourceStream(resourceName);
            if (stream == null)
            {
                throw new Exception($"SQL resource not found: {resourceName}");
            }
            using var reader = new StreamReader(stream);
            var sql = reader.ReadToEnd();
            migrationBuilder.Sql(sql);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP FUNCTION IF EXISTS get_expense_stats();");
        }
    }
}
