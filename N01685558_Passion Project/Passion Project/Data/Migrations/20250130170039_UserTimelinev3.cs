using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Passion_Project.Data.Migrations
{
    /// <inheritdoc />
    public partial class UserTimelinev3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersTimeline_timelines_timeline_Id",
                table: "UsersTimeline");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersTimeline_users_user_id",
                table: "UsersTimeline");

            migrationBuilder.DropIndex(
                name: "IX_UsersTimeline_timeline_Id",
                table: "UsersTimeline");

            migrationBuilder.DropIndex(
                name: "IX_UsersTimeline_user_id",
                table: "UsersTimeline");

            migrationBuilder.AddColumn<int>(
                name: "Usersuser_Id",
                table: "UsersTimeline",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "timeline_Id1",
                table: "UsersTimeline",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UsersTimeline_timeline_Id1",
                table: "UsersTimeline",
                column: "timeline_Id1");

            migrationBuilder.CreateIndex(
                name: "IX_UsersTimeline_Usersuser_Id",
                table: "UsersTimeline",
                column: "Usersuser_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersTimeline_timelines_timeline_Id1",
                table: "UsersTimeline",
                column: "timeline_Id1",
                principalTable: "timelines",
                principalColumn: "timeline_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersTimeline_users_Usersuser_Id",
                table: "UsersTimeline",
                column: "Usersuser_Id",
                principalTable: "users",
                principalColumn: "user_Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersTimeline_timelines_timeline_Id1",
                table: "UsersTimeline");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersTimeline_users_Usersuser_Id",
                table: "UsersTimeline");

            migrationBuilder.DropIndex(
                name: "IX_UsersTimeline_timeline_Id1",
                table: "UsersTimeline");

            migrationBuilder.DropIndex(
                name: "IX_UsersTimeline_Usersuser_Id",
                table: "UsersTimeline");

            migrationBuilder.DropColumn(
                name: "Usersuser_Id",
                table: "UsersTimeline");

            migrationBuilder.DropColumn(
                name: "timeline_Id1",
                table: "UsersTimeline");

            migrationBuilder.CreateIndex(
                name: "IX_UsersTimeline_timeline_Id",
                table: "UsersTimeline",
                column: "timeline_Id");

            migrationBuilder.CreateIndex(
                name: "IX_UsersTimeline_user_id",
                table: "UsersTimeline",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersTimeline_timelines_timeline_Id",
                table: "UsersTimeline",
                column: "timeline_Id",
                principalTable: "timelines",
                principalColumn: "timeline_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersTimeline_users_user_id",
                table: "UsersTimeline",
                column: "user_id",
                principalTable: "users",
                principalColumn: "user_Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
