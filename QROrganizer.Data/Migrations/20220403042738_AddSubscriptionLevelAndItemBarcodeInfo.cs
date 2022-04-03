using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QROrganizer.Data.Migrations
{
    public partial class AddSubscriptionLevelAndItemBarcodeInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BarcodeNumber",
                table: "Items");

            migrationBuilder.AddColumn<string>(
                name: "UpcCode",
                table: "Items",
                type: "nvarchar(450)",
                nullable: true);

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
                    UpcCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                    HighestPrice = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WasSuccessful = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemBarcodeInformation", x => x.Id);
                    table.UniqueConstraint("AK_ItemBarcodeInformation_UpcCode", x => x.UpcCode);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionFeatures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionFeatures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionLevels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubscriptionName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionLevels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionFeatureSubscriptionLevel",
                columns: table => new
                {
                    FeaturesId = table.Column<int>(type: "int", nullable: false),
                    SubscriptionLevelsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionFeatureSubscriptionLevel", x => new { x.FeaturesId, x.SubscriptionLevelsId });
                    table.ForeignKey(
                        name: "FK_SubscriptionFeatureSubscriptionLevel_SubscriptionFeatures_FeaturesId",
                        column: x => x.FeaturesId,
                        principalTable: "SubscriptionFeatures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubscriptionFeatureSubscriptionLevel_SubscriptionLevels_SubscriptionLevelsId",
                        column: x => x.SubscriptionLevelsId,
                        principalTable: "SubscriptionLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Items_UpcCode",
                table: "Items",
                column: "UpcCode");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_SubscriptionLevelId",
                table: "AspNetUsers",
                column: "SubscriptionLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemBarcodeInformation_UpcCode",
                table: "ItemBarcodeInformation",
                column: "UpcCode");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionFeatureSubscriptionLevel_SubscriptionLevelsId",
                table: "SubscriptionFeatureSubscriptionLevel",
                column: "SubscriptionLevelsId");

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

            migrationBuilder.DropForeignKey(
                name: "FK_Items_ItemBarcodeInformation_UpcCode",
                table: "Items");

            migrationBuilder.DropTable(
                name: "ItemBarcodeInformation");

            migrationBuilder.DropTable(
                name: "SubscriptionFeatureSubscriptionLevel");

            migrationBuilder.DropTable(
                name: "SubscriptionFeatures");

            migrationBuilder.DropTable(
                name: "SubscriptionLevels");

            migrationBuilder.DropIndex(
                name: "IX_Items_UpcCode",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_SubscriptionLevelId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UpcCode",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "SubscriptionLevelId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "BarcodeNumber",
                table: "Items",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
