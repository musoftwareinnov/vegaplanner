using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace vega.Migrations
{
    public partial class SeedInitialModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Makes (Name) VALUES ('Make1')");
            migrationBuilder.Sql("INSERT INTO Makes (Name) VALUES ('Make2')");
            migrationBuilder.Sql("INSERT INTO Makes (Name) VALUES ('Make3')");

            migrationBuilder.Sql("INSERT INTO Models (Name, MakeId) VALUES ('Make1-ModelA', (SELECT ID FROM Makes WHERE Name = 'Make1') )");
            migrationBuilder.Sql("INSERT INTO Models (Name, MakeId) VALUES ('Make1-ModelB', (SELECT ID FROM Makes WHERE Name = 'Make1') )");
            migrationBuilder.Sql("INSERT INTO Models (Name, MakeId) VALUES ('Make1-ModelC', (SELECT ID FROM Makes WHERE Name = 'Make1') )");

            migrationBuilder.Sql("INSERT INTO Models (Name, MakeId) VALUES ('Make2-ModelA', (SELECT ID FROM Makes WHERE Name = 'Make2') )");
            migrationBuilder.Sql("INSERT INTO Models (Name, MakeId) VALUES ('Make2-ModelB', (SELECT ID FROM Makes WHERE Name = 'Make2') )");
            migrationBuilder.Sql("INSERT INTO Models (Name, MakeId) VALUES ('Make2-ModelC', (SELECT ID FROM Makes WHERE Name = 'Make2') )");

            migrationBuilder.Sql("INSERT INTO Models (Name, MakeId) VALUES ('Make3-ModelA', (SELECT ID FROM Makes WHERE Name = 'Make3') )");
            migrationBuilder.Sql("INSERT INTO Models (Name, MakeId) VALUES ('Make3-ModelB', (SELECT ID FROM Makes WHERE Name = 'Make3') )");
            migrationBuilder.Sql("INSERT INTO Models (Name, MakeId) VALUES ('Make3-ModelC', (SELECT ID FROM Makes WHERE Name = 'Make3') )");

            migrationBuilder.Sql("INSERT INTO Features (Name) VALUES ('Feature1')");
            migrationBuilder.Sql("INSERT INTO Features (Name) VALUES ('Feature2')");
            migrationBuilder.Sql("INSERT INTO Features (Name) VALUES ('Feature3')");

            migrationBuilder.Sql("INSERT INTO StateInitialisers (Name, LastUpdate) VALUES ('General', getdate())");
            migrationBuilder.Sql("INSERT INTO StateInitialiserState (Name, AlertToCompletionTime, CompletionTime, LastUpdate, StateInitialiserId) VALUES ('Initial',3, 10, getdate(), (SELECT ID FROM Stateinitialisers WHERE Name = 'General'))");
            migrationBuilder.Sql("INSERT INTO StateInitialiserState (Name, AlertToCompletionTime, CompletionTime, LastUpdate, StateInitialiserId) VALUES ('EMail Sent',2, 15, getdate(), (SELECT ID FROM Stateinitialisers WHERE Name = 'General'))");
            migrationBuilder.Sql("INSERT INTO StateInitialiserState (Name, AlertToCompletionTime, CompletionTime, LastUpdate, StateInitialiserId) VALUES ('Client Visited',3, 6, getdate(), (SELECT ID FROM Stateinitialisers WHERE Name = 'General'))");
            migrationBuilder.Sql("INSERT INTO StateInitialiserState (Name, AlertToCompletionTime, CompletionTime, LastUpdate, StateInitialiserId) VALUES ('T&C Sent',4, 10, getdate(), (SELECT ID FROM Stateinitialisers WHERE Name = 'General'))");
            migrationBuilder.Sql("INSERT INTO StateInitialiserState (Name, AlertToCompletionTime, CompletionTime, LastUpdate, StateInitialiserId) VALUES ('T&C Received',5, 12, getdate(), (SELECT ID FROM Stateinitialisers WHERE Name = 'General'))");
            migrationBuilder.Sql("INSERT INTO StateInitialiserState (Name, AlertToCompletionTime, CompletionTime, LastUpdate, StateInitialiserId) VALUES ('Site Survey Booked',3, 10, getdate(), (SELECT ID FROM Stateinitialisers WHERE Name = 'General'))");
            migrationBuilder.Sql("INSERT INTO StateInitialiserState (Name, AlertToCompletionTime, CompletionTime, LastUpdate, StateInitialiserId) VALUES ('Site Survey Booked',3, 10, getdate(), (SELECT ID FROM Stateinitialisers WHERE Name = 'General'))");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Makes");
            migrationBuilder.Sql("DELETE FROM Models");

            migrationBuilder.Sql("DELETE Features WHERE NAME IN ('Feature1', 'Feature2', 'Feature3')");
                        migrationBuilder.Sql("DELETE StateInitialiserState WHERE StateInitialiserId = (SELECT ID FROM Stateinitialisers WHERE Name = 'General')");
            migrationBuilder.Sql("DELETE Stateinitialisers WHERE Name = 'General'");
        }
    }
}
