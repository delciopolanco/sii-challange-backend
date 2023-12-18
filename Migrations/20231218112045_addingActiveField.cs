using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POS_service_customers.Migrations
{
    /// <inheritdoc />
    public partial class addingActiveField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "customer",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "contact",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "address",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "customer");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "contact");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "address");
        }
    }
}
