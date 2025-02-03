using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Passion_Project.Data.Migrations
{
    /// <inheritdoc />
    public partial class UserTimelinev2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UsersTimeline",
                columns: table => new
                {
                    usertime_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    timeline_Id = table.Column<int>(type: "int", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersTimeline", x => x.usertime_Id);
                    table.ForeignKey(
                        name: "FK_UsersTimeline_timelines_timeline_Id",
                        column: x => x.timeline_Id,
                        principalTable: "timelines",
                        principalColumn: "timeline_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersTimeline_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "user_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsersTimeline_timeline_Id",
                table: "UsersTimeline",
                column: "timeline_Id");

            migrationBuilder.CreateIndex(
                name: "IX_UsersTimeline_user_id",
                table: "UsersTimeline",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsersTimeline");
        }
    }
}
