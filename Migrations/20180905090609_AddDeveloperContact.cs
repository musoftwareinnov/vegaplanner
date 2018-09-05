using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace vega.Migrations
{
    public partial class AddDeveloperContact : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Developer_EmailAddress",
                table: "PlanningApps",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Developer_FirstName",
                table: "PlanningApps",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Developer_LastName",
                table: "PlanningApps",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Developer_TelephoneHome",
                table: "PlanningApps",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Developer_TelephoneMobile",
                table: "PlanningApps",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Developer_TelephoneWork",
                table: "PlanningApps",
                maxLength: 30,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Developer_EmailAddress",
                table: "PlanningApps");

            migrationBuilder.DropColumn(
                name: "Developer_FirstName",
                table: "PlanningApps");

            migrationBuilder.DropColumn(
                name: "Developer_LastName",
                table: "PlanningApps");

            migrationBuilder.DropColumn(
                name: "Developer_TelephoneHome",
                table: "PlanningApps");

            migrationBuilder.DropColumn(
                name: "Developer_TelephoneMobile",
                table: "PlanningApps");

            migrationBuilder.DropColumn(
                name: "Developer_TelephoneWork",
                table: "PlanningApps");
        }
    }
}
