using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace vega.Migrations
{
    public partial class LinkPlanningStateToPlanningApp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlanningAppState_PlanningApps_PlanningAppId",
                table: "PlanningAppState");

            migrationBuilder.AlterColumn<int>(
                name: "PlanningAppId",
                table: "PlanningAppState",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PlanningAppState_PlanningApps_PlanningAppId",
                table: "PlanningAppState",
                column: "PlanningAppId",
                principalTable: "PlanningApps",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlanningAppState_PlanningApps_PlanningAppId",
                table: "PlanningAppState");

            migrationBuilder.AlterColumn<int>(
                name: "PlanningAppId",
                table: "PlanningAppState",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_PlanningAppState_PlanningApps_PlanningAppId",
                table: "PlanningAppState",
                column: "PlanningAppId",
                principalTable: "PlanningApps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
