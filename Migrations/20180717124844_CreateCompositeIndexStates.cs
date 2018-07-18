using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace vega.Migrations
{
    public partial class CreateCompositeIndexStates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StateInitialiserState_OrderId_Id",
                table: "StateInitialiserState");

            migrationBuilder.DropIndex(
                name: "IX_StateInitialiserState_StateInitialiserId",
                table: "StateInitialiserState");

            migrationBuilder.CreateIndex(
                name: "IX_StateInitialiserState_StateInitialiserId",
                table: "StateInitialiserState",
                columns: new string[] { "StateInitialiserId", "OrderId" } );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_StateInitialiserState_OrderId_Id",
                table: "StateInitialiserState",
                columns: new[] { "OrderId", "Id" },
                unique: true);
        }
    }
}
