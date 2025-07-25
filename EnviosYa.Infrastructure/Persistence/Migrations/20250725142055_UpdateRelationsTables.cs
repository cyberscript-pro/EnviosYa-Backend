using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnviosYa.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRelationsTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "Carts",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "Carts");
        }
    }
}
