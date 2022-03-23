using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QROrganizer.Data.Migrations
{
    public partial class AddSubscriptionLevels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SubscriptionLevelId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ItemBarcodeInformation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpcCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EanCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentCategory = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MpnCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Manufacturer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Publisher = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AsinCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Weight = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsAdult = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LowestPrice = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HighestPrice = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemBarcodeInformation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionLevels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubscriptionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubscriptionFeature = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionLevels", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_SubscriptionLevelId",
                table: "AspNetUsers",
                column: "SubscriptionLevelId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_SubscriptionLevels_SubscriptionLevelId",
                table: "AspNetUsers",
                column: "SubscriptionLevelId",
                principalTable: "SubscriptionLevels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_SubscriptionLevels_SubscriptionLevelId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "ItemBarcodeInformation");

            migrationBuilder.DropTable(
                name: "SubscriptionLevels");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_SubscriptionLevelId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SubscriptionLevelId",
                table: "AspNetUsers");
        }
    }
}
