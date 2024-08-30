using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Libook_API.Migrations.LibookDb
{
    /// <inheritdoc />
    public partial class AddOrderInfotable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OrderInfoId",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

          
           
            migrationBuilder.CreateTable(
                name: "OrderInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<int>(type: "int", nullable: false),
                    ProvinceId = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    DistrictId = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    WardId = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderInfo_districts_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "districts",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderInfo_provinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "provinces",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderInfo_wards_WardId",
                        column: x => x.WardId,
                        principalTable: "wards",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderInfoId",
                table: "Orders",
                column: "OrderInfoId");

    

            migrationBuilder.CreateIndex(
                name: "IX_OrderInfo_DistrictId",
                table: "OrderInfo",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderInfo_ProvinceId",
                table: "OrderInfo",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderInfo_WardId",
                table: "OrderInfo",
                column: "WardId");

           

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_OrderInfo_OrderInfoId",
                table: "Orders",
                column: "OrderInfoId",
                principalTable: "OrderInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_OrderInfo_OrderInfoId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "OrderInfo");

            migrationBuilder.DropIndex(
                name: "IX_Orders_OrderInfoId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderInfoId",
                table: "Orders");
        }
    }
}
