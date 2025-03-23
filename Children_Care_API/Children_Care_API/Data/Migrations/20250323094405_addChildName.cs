using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Children_Care_API.Data.Migrations
{
    /// <inheritdoc />
    public partial class addChildName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ChildName",
                table: "Reservations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChildName",
                table: "Reservations");
        }
    }
}
