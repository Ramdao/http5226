using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Passion_Project.Data.Migrations
{
    /// <inheritdoc />
    public partial class fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_timelines_entries_entry_Id",
                table: "timelines");

            migrationBuilder.DropIndex(
                name: "IX_timelines_entry_Id",
                table: "timelines");

            migrationBuilder.DropColumn(
                name: "entry_Id",
                table: "timelines");

            migrationBuilder.AddColumn<int>(
                name: "timeline_Id",
                table: "entries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_entries_timeline_Id",
                table: "entries",
                column: "timeline_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_entries_timelines_timeline_Id",
                table: "entries",
                column: "timeline_Id",
                principalTable: "timelines",
                principalColumn: "timeline_Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_entries_timelines_timeline_Id",
                table: "entries");

            migrationBuilder.DropIndex(
                name: "IX_entries_timeline_Id",
                table: "entries");

            migrationBuilder.DropColumn(
                name: "timeline_Id",
                table: "entries");

            migrationBuilder.AddColumn<int>(
                name: "entry_Id",
                table: "timelines",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_timelines_entry_Id",
                table: "timelines",
                column: "entry_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_timelines_entries_entry_Id",
                table: "timelines",
                column: "entry_Id",
                principalTable: "entries",
                principalColumn: "entries_Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
