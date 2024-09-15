using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PaymentBlockAPI.Migrations
{
    public partial class EditBlockMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blocks_Clients_ClientId",
                table: "Blocks");

            migrationBuilder.DropIndex(
                name: "IX_Blocks_ClientId",
                table: "Blocks");

            migrationBuilder.AlterColumn<string>(
                name: "UnlockDateTime",
                table: "Blocks",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "BlockDateTime",
                table: "Blocks",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UnlockDateTime",
                table: "Blocks",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "BlockDateTime",
                table: "Blocks",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Blocks_ClientId",
                table: "Blocks",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Blocks_Clients_ClientId",
                table: "Blocks",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
