using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace vega.Migrations
{
    public partial class PlanningApp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlanningApps",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LastUpdate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    StateInitialiserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanningApps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlanningApps_StateInitialisers_StateInitialiserId",
                        column: x => x.StateInitialiserId,
                        principalTable: "StateInitialisers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StateStatus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LastUpdate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StateStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlanningAppState",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CompletionDate = table.Column<DateTime>(nullable: false),
                    DueByDate = table.Column<DateTime>(nullable: false),
                    LastUpdate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    PlanningAppId = table.Column<int>(nullable: true),
                    StateInitialiserStateId = table.Column<int>(nullable: false),
                    StateStatusId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanningAppState", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlanningAppState_PlanningApps_PlanningAppId",
                        column: x => x.PlanningAppId,
                        principalTable: "PlanningApps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlanningAppState_StateInitialiserState_StateInitialiserStateId",
                        column: x => x.StateInitialiserStateId,
                        principalTable: "StateInitialiserState",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlanningAppState_StateStatus_StateStatusId",
                        column: x => x.StateStatusId,
                        principalTable: "StateStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlanningApps_StateInitialiserId",
                table: "PlanningApps",
                column: "StateInitialiserId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanningAppState_PlanningAppId",
                table: "PlanningAppState",
                column: "PlanningAppId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanningAppState_StateInitialiserStateId",
                table: "PlanningAppState",
                column: "StateInitialiserStateId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanningAppState_StateStatusId",
                table: "PlanningAppState",
                column: "StateStatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlanningAppState");

            migrationBuilder.DropTable(
                name: "PlanningApps");

            migrationBuilder.DropTable(
                name: "StateStatus");
        }
    }
}
