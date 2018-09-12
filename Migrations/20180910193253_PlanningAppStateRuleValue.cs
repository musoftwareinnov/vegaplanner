using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace vega.Migrations
{
    public partial class PlanningAppStateRuleValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlanningAppStateRuleValue",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateValue = table.Column<DateTime>(nullable: false),
                    IntValue = table.Column<int>(nullable: false),
                    PlanningAppStateId = table.Column<int>(nullable: true),
                    RuleId = table.Column<int>(nullable: false),
                    StrValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanningAppStateRuleValue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlanningAppStateRuleValue_PlanningAppState_PlanningAppStateId",
                        column: x => x.PlanningAppStateId,
                        principalTable: "PlanningAppState",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlanningAppStateRuleValue_PlanningAppStateId",
                table: "PlanningAppStateRuleValue",
                column: "PlanningAppStateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlanningAppStateRuleValue");
        }
    }
}
