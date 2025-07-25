using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnviosYa.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTableCartItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "CartItems",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "CartItems");
        }
    }
}
