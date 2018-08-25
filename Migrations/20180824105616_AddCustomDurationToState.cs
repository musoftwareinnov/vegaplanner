using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace vega.Migrations
{
    public partial class AddCustomDurationToState : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AllowStateEdit",
                table: "PlanningAppState",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CustomDuration",
                table: "PlanningAppState",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "CustomDurationSet",
                table: "PlanningAppState",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AllowStateEdit",
                table: "PlanningAppState");

            migrationBuilder.DropColumn(
                name: "CustomDuration",
                table: "PlanningAppState");

            migrationBuilder.DropColumn(
                name: "CustomDurationSet",
                table: "PlanningAppState");
        }
    }
}
