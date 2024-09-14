using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PaymentBlockAPI.Migrations
{
    public partial class AddStatusMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "Активен");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Clients");
        }
    }
}
