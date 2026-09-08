using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniERP.Migrations
{
    /// <inheritdoc />
    public partial class SalesOrderRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrderLines_SalesOrders_SalesOrderId",
                table: "SalesOrderLines");

            migrationBuilder.AlterColumn<Guid>(
                name: "SalesOrderId",
                table: "SalesOrderLines",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SalesOrderId1",
                table: "SalesOrderLines",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderLines_ProductId",
                table: "SalesOrderLines",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderLines_SalesOrderId1",
                table: "SalesOrderLines",
                column: "SalesOrderId1");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrderLines_Products_ProductId",
                table: "SalesOrderLines",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrderLines_SalesOrders_SalesOrderId",
                table: "SalesOrderLines",
                column: "SalesOrderId",
                principalTable: "SalesOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrderLines_SalesOrders_SalesOrderId1",
                table: "SalesOrderLines",
                column: "SalesOrderId1",
                principalTable: "SalesOrders",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrderLines_Products_ProductId",
                table: "SalesOrderLines");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrderLines_SalesOrders_SalesOrderId",
                table: "SalesOrderLines");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrderLines_SalesOrders_SalesOrderId1",
                table: "SalesOrderLines");

            migrationBuilder.DropIndex(
                name: "IX_SalesOrderLines_ProductId",
                table: "SalesOrderLines");

            migrationBuilder.DropIndex(
                name: "IX_SalesOrderLines_SalesOrderId1",
                table: "SalesOrderLines");

            migrationBuilder.DropColumn(
                name: "SalesOrderId1",
                table: "SalesOrderLines");

            migrationBuilder.AlterColumn<Guid>(
                name: "SalesOrderId",
                table: "SalesOrderLines",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrderLines_SalesOrders_SalesOrderId",
                table: "SalesOrderLines",
                column: "SalesOrderId",
                principalTable: "SalesOrders",
                principalColumn: "Id");
        }
    }
}
