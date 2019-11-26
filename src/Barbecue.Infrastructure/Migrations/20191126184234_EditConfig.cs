using Microsoft.EntityFrameworkCore.Migrations;

namespace Barbecue.Infrastructure.Migrations
{
    public partial class EditConfig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventUser_Events_EventId1",
                table: "EventUser");

            migrationBuilder.DropForeignKey(
                name: "FK_EventUser_Users_UserId1",
                table: "EventUser");

            migrationBuilder.DropIndex(
                name: "IX_EventUser_EventId1",
                table: "EventUser");

            migrationBuilder.DropIndex(
                name: "IX_EventUser_UserId1",
                table: "EventUser");

            migrationBuilder.DropColumn(
                name: "EventId1",
                table: "EventUser");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "EventUser");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EventId1",
                table: "EventUser",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId1",
                table: "EventUser",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EventUser_EventId1",
                table: "EventUser",
                column: "EventId1");

            migrationBuilder.CreateIndex(
                name: "IX_EventUser_UserId1",
                table: "EventUser",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_EventUser_Events_EventId1",
                table: "EventUser",
                column: "EventId1",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EventUser_Users_UserId1",
                table: "EventUser",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
