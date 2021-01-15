using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class AddCustomer1FK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchase_AspNetUsers_Customer1Id1",
                table: "Purchase");

            migrationBuilder.DropIndex(
                name: "IX_Purchase_Customer1Id1",
                table: "Purchase");

            migrationBuilder.DropColumn(
                name: "Customer1Id1",
                table: "Purchase");

            migrationBuilder.AlterColumn<string>(
                name: "Customer1Id",
                table: "Purchase",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Purchase_Customer1Id",
                table: "Purchase",
                column: "Customer1Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchase_AspNetUsers_Customer1Id",
                table: "Purchase",
                column: "Customer1Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchase_AspNetUsers_Customer1Id",
                table: "Purchase");

            migrationBuilder.DropIndex(
                name: "IX_Purchase_Customer1Id",
                table: "Purchase");

            migrationBuilder.AlterColumn<int>(
                name: "Customer1Id",
                table: "Purchase",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Customer1Id1",
                table: "Purchase",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Purchase_Customer1Id1",
                table: "Purchase",
                column: "Customer1Id1");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchase_AspNetUsers_Customer1Id1",
                table: "Purchase",
                column: "Customer1Id1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
