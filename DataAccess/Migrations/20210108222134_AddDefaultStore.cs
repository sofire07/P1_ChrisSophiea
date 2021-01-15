using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class AddDefaultStore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_ItemPurchase_Purchase_ItemPurchaseId",
            //    table: "ItemPurchase");

            //migrationBuilder.DropIndex(
            //    name: "IX_ItemPurchase_ItemPurchaseId",
            //    table: "ItemPurchase");

            //migrationBuilder.DropColumn(
            //    name: "ItemPurchaseId",
            //    table: "Purchase");

            //migrationBuilder.DropColumn(
            //    name: "ItemPurchaseId",
            //    table: "ItemPurchase");

            migrationBuilder.AddColumn<int>(
                name: "DefaultStoreId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_ItemPurchase_PurchasesPurchaseId",
            //    table: "ItemPurchase",
            //    column: "PurchasesPurchaseId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_DefaultStoreId",
                table: "AspNetUsers",
                column: "DefaultStoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Store_DefaultStoreId",
                table: "AspNetUsers",
                column: "DefaultStoreId",
                principalTable: "Store",
                principalColumn: "StoreId",
                onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_ItemPurchase_Purchase_PurchasesPurchaseId",
            //    table: "ItemPurchase",
            //    column: "PurchasesPurchaseId",
            //    principalTable: "Purchase",
            //    principalColumn: "PurchaseId",
            //    onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Store_DefaultStoreId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemPurchase_Purchase_PurchasesPurchaseId",
                table: "ItemPurchase");

            migrationBuilder.DropIndex(
                name: "IX_ItemPurchase_PurchasesPurchaseId",
                table: "ItemPurchase");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_DefaultStoreId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DefaultStoreId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "ItemPurchaseId",
                table: "Purchase",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ItemPurchaseId",
                table: "ItemPurchase",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItemPurchase_ItemPurchaseId",
                table: "ItemPurchase",
                column: "ItemPurchaseId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemPurchase_Purchase_ItemPurchaseId",
                table: "ItemPurchase",
                column: "ItemPurchaseId",
                principalTable: "Purchase",
                principalColumn: "PurchaseId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
