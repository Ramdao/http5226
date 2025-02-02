using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Passion_Project.Data.Migrations
{
    /// <inheritdoc />
    public partial class Entries : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "entry_id",
                table: "timelines",
                newName: "entry_Id");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "timelines",
                newName: "timeline_name");

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "timelines",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "entries_Id",
                table: "timelines",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Entries",
                columns: table => new
                {
                    entries_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    entries_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    images = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entries", x => x.entries_Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_timelines_entries_Id",
                table: "timelines",
                column: "entries_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_timelines_Entries_entries_Id",
                table: "timelines",
                column: "entries_Id",
                principalTable: "Entries",
                principalColumn: "entries_Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_timelines_Entries_entries_Id",
                table: "timelines");

            migrationBuilder.DropTable(
                name: "Entries");

            migrationBuilder.DropIndex(
                name: "IX_timelines_entries_Id",
                table: "timelines");

            migrationBuilder.DropColumn(
                name: "description",
                table: "timelines");

            migrationBuilder.DropColumn(
                name: "entries_Id",
                table: "timelines");

            migrationBuilder.RenameColumn(
                name: "entry_Id",
                table: "timelines",
                newName: "entry_id");

            migrationBuilder.RenameColumn(
                name: "timeline_name",
                table: "timelines",
                newName: "name");
        }
    }
}
