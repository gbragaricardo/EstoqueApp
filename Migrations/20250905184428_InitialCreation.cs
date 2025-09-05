using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EstoqueApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "NVARCHAR(80)", maxLength: 80, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CostCenter",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "NVARCHAR(80)", maxLength: 80, nullable: false),
                    Code = table.Column<string>(type: "NVARCHAR(8)", maxLength: 8, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CostCenter", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "NVARCHAR(80)", maxLength: 80, nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    Description = table.Column<string>(type: "NVARCHAR(256)", maxLength: 256, nullable: true),
                    SKU = table.Column<string>(type: "NVARCHAR(8)", maxLength: 8, nullable: false),
                    CurrentStock = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.CheckConstraint("CK_Product_CurrentStock_NonNegative", "[CurrentStock] >= 0");
                    table.CheckConstraint("CK_Product_UnitPrice_NonNegative", "[UnitPrice]   >= 0");
                    table.ForeignKey(
                        name: "FK_Product_Category",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StockMovements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "VARCHAR(10)", unicode: false, maxLength: 10, nullable: false),
                    UnitCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    Description = table.Column<string>(type: "NVARCHAR(256)", maxLength: 256, nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    CostCenterId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockMovements", x => x.Id);
                    table.CheckConstraint("CK_StockMovement_Quantity_Positive", "[Quantity] > 0");
                    table.CheckConstraint("CK_StockMovement_Type_Allowed", "[Type] IN ('In','Out')");
                    table.CheckConstraint("CK_StockMovement_UnitCost_NonNegative", "[UnitCost] >= 0");
                    table.ForeignKey(
                        name: "FK_Movement_CostCenter",
                        column: x => x.CostCenterId,
                        principalTable: "CostCenter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Movement_Product",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CostCenter_Code",
                table: "CostCenter",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_CategoryId",
                table: "Product",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_Sku",
                table: "Product",
                column: "SKU",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StockMovements_CostCenterId",
                table: "StockMovements",
                column: "CostCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_StockMovements_ProductId",
                table: "StockMovements",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StockMovements");

            migrationBuilder.DropTable(
                name: "CostCenter");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Category");
        }
    }
}
