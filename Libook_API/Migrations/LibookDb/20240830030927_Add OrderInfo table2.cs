using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Libook_API.Migrations.LibookDb
{
    /// <inheritdoc />
    public partial class AddOrderInfotable2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderInfo_districts_DistrictId",
                table: "OrderInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderInfo_provinces_ProvinceId",
                table: "OrderInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderInfo_wards_WardId",
                table: "OrderInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_OrderInfo_OrderInfoId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderInfo",
                table: "OrderInfo");

            migrationBuilder.RenameTable(
                name: "OrderInfo",
                newName: "OrderInfos");

            migrationBuilder.RenameIndex(
                name: "IX_OrderInfo_WardId",
                table: "OrderInfos",
                newName: "IX_OrderInfos_WardId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderInfo_ProvinceId",
                table: "OrderInfos",
                newName: "IX_OrderInfos_ProvinceId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderInfo_DistrictId",
                table: "OrderInfos",
                newName: "IX_OrderInfos_DistrictId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderInfos",
                table: "OrderInfos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderInfos_districts_DistrictId",
                table: "OrderInfos",
                column: "DistrictId",
                principalTable: "districts",
                principalColumn: "code",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderInfos_provinces_ProvinceId",
                table: "OrderInfos",
                column: "ProvinceId",
                principalTable: "provinces",
                principalColumn: "code",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderInfos_wards_WardId",
                table: "OrderInfos",
                column: "WardId",
                principalTable: "wards",
                principalColumn: "code",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_OrderInfos_OrderInfoId",
                table: "Orders",
                column: "OrderInfoId",
                principalTable: "OrderInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderInfos_districts_DistrictId",
                table: "OrderInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderInfos_provinces_ProvinceId",
                table: "OrderInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderInfos_wards_WardId",
                table: "OrderInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_OrderInfos_OrderInfoId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderInfos",
                table: "OrderInfos");

            migrationBuilder.RenameTable(
                name: "OrderInfos",
                newName: "OrderInfo");

            migrationBuilder.RenameIndex(
                name: "IX_OrderInfos_WardId",
                table: "OrderInfo",
                newName: "IX_OrderInfo_WardId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderInfos_ProvinceId",
                table: "OrderInfo",
                newName: "IX_OrderInfo_ProvinceId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderInfos_DistrictId",
                table: "OrderInfo",
                newName: "IX_OrderInfo_DistrictId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderInfo",
                table: "OrderInfo",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderInfo_districts_DistrictId",
                table: "OrderInfo",
                column: "DistrictId",
                principalTable: "districts",
                principalColumn: "code",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderInfo_provinces_ProvinceId",
                table: "OrderInfo",
                column: "ProvinceId",
                principalTable: "provinces",
                principalColumn: "code",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderInfo_wards_WardId",
                table: "OrderInfo",
                column: "WardId",
                principalTable: "wards",
                principalColumn: "code",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_OrderInfo_OrderInfoId",
                table: "Orders",
                column: "OrderInfoId",
                principalTable: "OrderInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
