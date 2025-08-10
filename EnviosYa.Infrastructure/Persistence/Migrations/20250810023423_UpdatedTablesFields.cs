using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnviosYa.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedTablesFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Categories");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Categories",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
