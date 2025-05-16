using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiagnosticApi.Migrations
{
    /// <inheritdoc />
    public partial class RemoveGenderFromPatient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Patients");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Patients",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
