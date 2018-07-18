using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace vega.Migrations
{
    public partial class LinkStateToInitialiser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StateInitialiserState_StateInitialisers_StateInitialiserId",
                table: "StateInitialiserState");

            migrationBuilder.AlterColumn<int>(
                name: "StateInitialiserId",
                table: "StateInitialiserState",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_StateInitialiserState_StateInitialisers_StateInitialiserId",
                table: "StateInitialiserState",
                column: "StateInitialiserId",
                principalTable: "StateInitialisers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StateInitialiserState_StateInitialisers_StateInitialiserId",
                table: "StateInitialiserState");

            migrationBuilder.AlterColumn<int>(
                name: "StateInitialiserId",
                table: "StateInitialiserState",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_StateInitialiserState_StateInitialisers_StateInitialiserId",
                table: "StateInitialiserState",
                column: "StateInitialiserId",
                principalTable: "StateInitialisers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
