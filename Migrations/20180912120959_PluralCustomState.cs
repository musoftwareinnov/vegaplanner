using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace vega.Migrations
{
    public partial class PluralCustomState : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StateInitialiserStateCustomField_StateInitialiserCustomFields_StateInitialiserCustomFieldId",
                table: "StateInitialiserStateCustomField");

            migrationBuilder.DropForeignKey(
                name: "FK_StateInitialiserStateCustomField_StateInitialiserState_StateInitialiserStateId",
                table: "StateInitialiserStateCustomField");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StateInitialiserStateCustomField",
                table: "StateInitialiserStateCustomField");

            migrationBuilder.RenameTable(
                name: "StateInitialiserStateCustomField",
                newName: "StateInitialiserStateCustomFields");

            migrationBuilder.RenameIndex(
                name: "IX_StateInitialiserStateCustomField_StateInitialiserCustomFieldId",
                table: "StateInitialiserStateCustomFields",
                newName: "IX_StateInitialiserStateCustomFields_StateInitialiserCustomFieldId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StateInitialiserStateCustomFields",
                table: "StateInitialiserStateCustomFields",
                columns: new[] { "StateInitialiserStateId", "StateInitialiserCustomFieldId" });

            migrationBuilder.AddForeignKey(
                name: "FK_StateInitialiserStateCustomFields_StateInitialiserCustomFields_StateInitialiserCustomFieldId",
                table: "StateInitialiserStateCustomFields",
                column: "StateInitialiserCustomFieldId",
                principalTable: "StateInitialiserCustomFields",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StateInitialiserStateCustomFields_StateInitialiserState_StateInitialiserStateId",
                table: "StateInitialiserStateCustomFields",
                column: "StateInitialiserStateId",
                principalTable: "StateInitialiserState",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StateInitialiserStateCustomFields_StateInitialiserCustomFields_StateInitialiserCustomFieldId",
                table: "StateInitialiserStateCustomFields");

            migrationBuilder.DropForeignKey(
                name: "FK_StateInitialiserStateCustomFields_StateInitialiserState_StateInitialiserStateId",
                table: "StateInitialiserStateCustomFields");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StateInitialiserStateCustomFields",
                table: "StateInitialiserStateCustomFields");

            migrationBuilder.RenameTable(
                name: "StateInitialiserStateCustomFields",
                newName: "StateInitialiserStateCustomField");

            migrationBuilder.RenameIndex(
                name: "IX_StateInitialiserStateCustomFields_StateInitialiserCustomFieldId",
                table: "StateInitialiserStateCustomField",
                newName: "IX_StateInitialiserStateCustomField_StateInitialiserCustomFieldId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StateInitialiserStateCustomField",
                table: "StateInitialiserStateCustomField",
                columns: new[] { "StateInitialiserStateId", "StateInitialiserCustomFieldId" });

            migrationBuilder.AddForeignKey(
                name: "FK_StateInitialiserStateCustomField_StateInitialiserCustomFields_StateInitialiserCustomFieldId",
                table: "StateInitialiserStateCustomField",
                column: "StateInitialiserCustomFieldId",
                principalTable: "StateInitialiserCustomFields",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StateInitialiserStateCustomField_StateInitialiserState_StateInitialiserStateId",
                table: "StateInitialiserStateCustomField",
                column: "StateInitialiserStateId",
                principalTable: "StateInitialiserState",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
