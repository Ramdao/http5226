using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Passion_Project.Data.Migrations
{
    /// <inheritdoc />
    public partial class Entriesv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_timelines_Entries_entries_Id",
                table: "timelines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Entries",
                table: "Entries");

            migrationBuilder.RenameTable(
                name: "Entries",
                newName: "entries");

            migrationBuilder.AddPrimaryKey(
                name: "PK_entries",
                table: "entries",
                column: "entries_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_timelines_entries_entries_Id",
                table: "timelines",
                column: "entries_Id",
                principalTable: "entries",
                principalColumn: "entries_Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_timelines_entries_entries_Id",
                table: "timelines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_entries",
                table: "entries");

            migrationBuilder.RenameTable(
                name: "entries",
                newName: "Entries");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Entries",
                table: "Entries",
                column: "entries_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_timelines_Entries_entries_Id",
                table: "timelines",
                column: "entries_Id",
                principalTable: "Entries",
                principalColumn: "entries_Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
