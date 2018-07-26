using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace vega.Migrations
{
    public partial class AddNewAppPlanningStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrentPlanningStatusId",
                table: "PlanningApps",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PlanningApps_CurrentPlanningStatusId",
                table: "PlanningApps",
                column: "CurrentPlanningStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlanningApps_StateStatus_CurrentPlanningStatusId",
                table: "PlanningApps",
                column: "CurrentPlanningStatusId",
                principalTable: "StateStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlanningApps_StateStatus_CurrentPlanningStatusId",
                table: "PlanningApps");

            migrationBuilder.DropIndex(
                name: "IX_PlanningApps_CurrentPlanningStatusId",
                table: "PlanningApps");

            migrationBuilder.DropColumn(
                name: "CurrentPlanningStatusId",
                table: "PlanningApps");
        }
    }
}
