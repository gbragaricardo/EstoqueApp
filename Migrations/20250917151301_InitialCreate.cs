using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EstoqueApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
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
                name: "UnitOfMeasure",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "NVARCHAR(80)", maxLength: 80, nullable: false),
                    Abbreviation = table.Column<string>(type: "NVARCHAR(8)", maxLength: 8, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitOfMeasure", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "NVARCHAR(160)", maxLength: 160, nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    Description = table.Column<string>(type: "NVARCHAR(256)", maxLength: 256, nullable: true),
                    SKU = table.Column<string>(type: "NVARCHAR(8)", maxLength: 8, nullable: false),
                    MinStock = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    CurrentStock = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    UnitOfMeasureId = table.Column<int>(type: "int", nullable: false)
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
                    table.ForeignKey(
                        name: "FK_Product_UnitOfMeasure",
                        column: x => x.UnitOfMeasureId,
                        principalTable: "UnitOfMeasure",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StockByCostCenter",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    CostCenterId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockByCostCenter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockByCC_CostCenter",
                        column: x => x.CostCenterId,
                        principalTable: "CostCenter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StockByCC_Product",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StockMovements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "varchar(16)", unicode: false, maxLength: 16, nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    Description = table.Column<string>(type: "NVARCHAR(256)", maxLength: 256, nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    OriginCostCenterId = table.Column<int>(type: "int", nullable: true),
                    DestinationCostCenterId = table.Column<int>(type: "int", nullable: true),
                    CostCenterId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockMovements", x => x.Id);
                    table.CheckConstraint("CK_StockMovement_Quantity_Positive", "[Quantity] > 0");
                    table.CheckConstraint("CK_StockMovement_Type_Allowed", "[Type] IN ('Entry','Exit','Transfer')");
                    table.ForeignKey(
                        name: "FK_Movement_DestinationCostCenter",
                        column: x => x.DestinationCostCenterId,
                        principalTable: "CostCenter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Movement_OriginCostCenter",
                        column: x => x.OriginCostCenterId,
                        principalTable: "CostCenter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Movement_Product",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StockMovements_CostCenter_CostCenterId",
                        column: x => x.CostCenterId,
                        principalTable: "CostCenter",
                        principalColumn: "Id");
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
                name: "IX_Product_UnitOfMeasureId",
                table: "Product",
                column: "UnitOfMeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductName",
                table: "Product",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StockByCostCenter_CostCenterId",
                table: "StockByCostCenter",
                column: "CostCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_StockByCostCenter_ProductId_CostCenterId",
                table: "StockByCostCenter",
                columns: new[] { "ProductId", "CostCenterId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StockMovement_Date",
                table: "StockMovements",
                column: "Date");

            migrationBuilder.CreateIndex(
                name: "IX_StockMovement_DestinationCC",
                table: "StockMovements",
                column: "DestinationCostCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_StockMovement_OriginCC",
                table: "StockMovements",
                column: "OriginCostCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_StockMovement_Product",
                table: "StockMovements",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_StockMovements_CostCenterId",
                table: "StockMovements",
                column: "CostCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_UnitOfMeasure_Abbreviation",
                table: "UnitOfMeasure",
                column: "Abbreviation",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StockByCostCenter");

            migrationBuilder.DropTable(
                name: "StockMovements");

            migrationBuilder.DropTable(
                name: "CostCenter");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "UnitOfMeasure");
        }
    }
}
