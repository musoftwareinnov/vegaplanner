using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace vega.Migrations
{
    public partial class AddDevelopmentAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DevelopmentAddress_AddressLine1",
                table: "PlanningApps",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DevelopmentAddress_AddressLine2",
                table: "PlanningApps",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DevelopmentAddress_CompanyName",
                table: "PlanningApps",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DevelopmentAddress_GeoLocation",
                table: "PlanningApps",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DevelopmentAddress_Postcode",
                table: "PlanningApps",
                maxLength: 10,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DevelopmentAddress_AddressLine1",
                table: "PlanningApps");

            migrationBuilder.DropColumn(
                name: "DevelopmentAddress_AddressLine2",
                table: "PlanningApps");

            migrationBuilder.DropColumn(
                name: "DevelopmentAddress_CompanyName",
                table: "PlanningApps");

            migrationBuilder.DropColumn(
                name: "DevelopmentAddress_GeoLocation",
                table: "PlanningApps");

            migrationBuilder.DropColumn(
                name: "DevelopmentAddress_Postcode",
                table: "PlanningApps");
        }
    }
}
