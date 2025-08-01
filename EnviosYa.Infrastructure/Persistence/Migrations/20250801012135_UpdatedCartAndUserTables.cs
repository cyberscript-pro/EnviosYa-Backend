using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnviosYa.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedCartAndUserTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsVerifiedEmail",
                table: "Users",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsVerifiedEmail",
                table: "Users");
        }
    }
}
