using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Manero_Backoffice.Migrations
{
    public partial class removedfirstnamelastnameandaddressestable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // migrationBuilder.DropForeignKey(
            //     name: "FK_AspNetUsers_Addresses_AddressId",
            //     table: "AspNetUsers");

            //migrationBuilder.DropTable(
            //    name: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_AddressId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AddressId",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            //migrationBuilder.CreateTable(
            //    name: "Addresses",
            //    columns: table => new
            //    {
            //        Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
            //        AddressLine_1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        AddressLine_2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        City = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Addresses", x => x.Id);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_AspNetUsers_AddressId",
            //    table: "AspNetUsers",
            //    column: "AddressId");

            // migrationBuilder.AddForeignKey(
            //     name: "FK_AspNetUsers_Addresses_AddressId",
            //     table: "AspNetUsers",
            //     column: "AddressId",
            //     principalTable: "Addresses",
            //     principalColumn: "Id");
        }
    }
}
