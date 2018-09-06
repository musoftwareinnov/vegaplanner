using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace vega.Migrations
{
    public partial class AddStateRules : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StateRule",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    isMandatory = table.Column<bool>(nullable: false),
                    isPlanningAppField = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StateRule", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StateInitialiserStateRules",
                columns: table => new
                {
                    StateInitialiserStateId = table.Column<int>(nullable: false),
                    StateRuleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StateInitialiserStateRules", x => new { x.StateInitialiserStateId, x.StateRuleId });
                    table.ForeignKey(
                        name: "FK_StateInitialiserStateRules_StateInitialiserState_StateInitialiserStateId",
                        column: x => x.StateInitialiserStateId,
                        principalTable: "StateInitialiserState",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StateInitialiserStateRules_StateRule_StateRuleId",
                        column: x => x.StateRuleId,
                        principalTable: "StateRule",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StateInitialiserStateRules_StateRuleId",
                table: "StateInitialiserStateRules",
                column: "StateRuleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StateInitialiserStateRules");

            migrationBuilder.DropTable(
                name: "StateRule");
        }
    }
}
