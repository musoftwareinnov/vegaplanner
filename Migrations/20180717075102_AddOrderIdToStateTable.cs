using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace vega.Migrations
{
    public partial class AddOrderIdToStateTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
       
            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "StateInitialiserState",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_StateInitialiserState_OrderId_Id",
                table: "StateInitialiserState",
                columns: new[] { "OrderId", "Id" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StateInitialiserState_OrderId_Id",
                table: "StateInitialiserState");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "StateInitialiserState");
        }
    }
}
