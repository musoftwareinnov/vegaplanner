using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace vega.Migrations
{
    public partial class SetMinDateSettings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AllowStateEdit",
                table: "PlanningAppState");

            migrationBuilder.AddColumn<bool>(
                name: "AllowDateEdit",
                table: "PlanningAppState",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "PlanningAppState",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "PlanningApps",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AllowDateEdit",
                table: "PlanningAppState");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "PlanningAppState");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "PlanningApps");

            migrationBuilder.AddColumn<int>(
                name: "AllowStateEdit",
                table: "PlanningAppState",
                nullable: false,
                defaultValue: 0);
        }
    }
}
