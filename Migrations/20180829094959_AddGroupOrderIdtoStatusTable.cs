using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace vega.Migrations
{
    public partial class AddGroupOrderIdtoStatusTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AllowDateEdit",
                table: "PlanningAppState");

            migrationBuilder.AddColumn<string>(
                name: "GroupType",
                table: "StateStatus",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "StateStatus",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GroupType",
                table: "StateStatus");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "StateStatus");

            migrationBuilder.AddColumn<bool>(
                name: "AllowDateEdit",
                table: "PlanningAppState",
                nullable: false,
                defaultValue: false);
        }
    }
}
