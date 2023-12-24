using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagmentSystem.Infrastructure.Migrations
{
    public partial class AddTaskOwnerID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TaskOwnerId",
                table: "Tasks",
                type: "varchar(36)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_TaskOwnerId",
                table: "Tasks",
                column: "TaskOwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Users_TaskOwnerId",
                table: "Tasks",
                column: "TaskOwnerId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Users_TaskOwnerId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_TaskOwnerId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "TaskOwnerId",
                table: "Tasks");
        }
    }
}
