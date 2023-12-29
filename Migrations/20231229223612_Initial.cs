using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace addCard_backend.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CreditCard",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreditcardHolderName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreditcardNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreditcardCVV = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    CreditcardExpireDate = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModificationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationUser = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastModificationUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditCard", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CreditCard");
        }
    }
}
