using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Passion_Project.Data.Migrations
{
    /// <inheritdoc />
    public partial class UserTimeline : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_timelines_entries_entries_Id",
                table: "timelines");

            migrationBuilder.DropIndex(
                name: "IX_timelines_entries_Id",
                table: "timelines");

            migrationBuilder.DropColumn(
                name: "entries_Id",
                table: "timelines");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_timelines_entries_entry_Id",
                table: "timelines");

            migrationBuilder.DropIndex(
                name: "IX_timelines_entry_Id",
                table: "timelines");

            migrationBuilder.AddColumn<int>(
                name: "entries_Id",
                table: "timelines",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_timelines_entries_Id",
                table: "timelines",
                column: "entries_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_timelines_entries_entries_Id",
                table: "timelines",
                column: "entries_Id",
                principalTable: "entries",
                principalColumn: "entries_Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
