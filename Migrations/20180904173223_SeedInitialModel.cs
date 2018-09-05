using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace vega.Migrations
{
    public partial class SeedInitialModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO StateStatus (Name, LastUpdate, GroupType, OrderId) VALUES ('InProgress', getdate(), 'InProgress', 1)");
            migrationBuilder.Sql("INSERT INTO StateStatus (Name, LastUpdate, GroupType, OrderId) VALUES ('Overdue', getdate(), 'InProgress', 2)");
            migrationBuilder.Sql("INSERT INTO StateStatus (Name, LastUpdate, GroupType, OrderId) VALUES ('Due', getdate(), 'InProgress', 3)");
            migrationBuilder.Sql("INSERT INTO StateStatus (Name, LastUpdate, GroupType, OrderId) VALUES ('OnTime', getdate(), 'InProgress', 4)");
            migrationBuilder.Sql("INSERT INTO StateStatus (Name, LastUpdate, GroupType, OrderId) VALUES ('Not InProgress', getdate(), 'Not InProgress', 5)");
            migrationBuilder.Sql("INSERT INTO StateStatus (Name, LastUpdate, GroupType, OrderId) VALUES ('Complete', getdate(), 'Not InProgress', 6)");
            migrationBuilder.Sql("INSERT INTO StateStatus (Name, LastUpdate, GroupType, OrderId) VALUES ('Overran', getdate(), 'Not InProgress', 7)");
            migrationBuilder.Sql("INSERT INTO StateStatus (Name, LastUpdate, GroupType, OrderId) VALUES ('Terminated', getdate(), 'Not InProgress', 8)");
            migrationBuilder.Sql("INSERT INTO StateStatus (Name, LastUpdate, GroupType, OrderId) VALUES ('Archived', getdate(), 'Not InProgress', 9)");
            migrationBuilder.Sql("INSERT INTO StateStatus (Name, LastUpdate, GroupType, OrderId) VALUES ('All', getdate(), 'All', 10)");


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

            migrationBuilder.Sql("DELETE FROM StateInitialiserState WHERE StateInitialiserId = (SELECT ID FROM Stateinitialisers WHERE Name = 'General') ");
            migrationBuilder.Sql("INSERT INTO StateInitialiserState (Name, AlertToCompletionTime, CompletionTime, LastUpdate, StateInitialiserId, OrderId, isDeleted) VALUES ('Initial',3, 10, getdate(), (SELECT ID FROM Stateinitialisers WHERE Name = 'General'), 1, 0)");
            migrationBuilder.Sql("INSERT INTO StateInitialiserState (Name, AlertToCompletionTime, CompletionTime, LastUpdate, StateInitialiserId, OrderId, isDeleted) VALUES ('EMail Sent',2, 15, getdate(), (SELECT ID FROM Stateinitialisers WHERE Name = 'General'), 2,0)");
            migrationBuilder.Sql("INSERT INTO StateInitialiserState (Name, AlertToCompletionTime, CompletionTime, LastUpdate, StateInitialiserId, OrderId, isDeleted) VALUES ('Client Visited',3, 6, getdate(), (SELECT ID FROM Stateinitialisers WHERE Name = 'General'), 3,0)");
            migrationBuilder.Sql("INSERT INTO StateInitialiserState (Name, AlertToCompletionTime, CompletionTime, LastUpdate, StateInitialiserId, OrderId, isDeleted) VALUES ('T&C Sent',4, 10, getdate(), (SELECT ID FROM Stateinitialisers WHERE Name = 'General'), 4,0)");
            migrationBuilder.Sql("INSERT INTO StateInitialiserState (Name, AlertToCompletionTime, CompletionTime, LastUpdate, StateInitialiserId, OrderId, isDeleted) VALUES ('T&C Received',5, 12, getdate(), (SELECT ID FROM Stateinitialisers WHERE Name = 'General'), 5,0)");
            migrationBuilder.Sql("INSERT INTO StateInitialiserState (Name, AlertToCompletionTime, CompletionTime, LastUpdate, StateInitialiserId, OrderId, isDeleted) VALUES ('Site Survey Booked',3, 10, getdate(), (SELECT ID FROM Stateinitialisers WHERE Name = 'General'), 6,0)");
            migrationBuilder.Sql("INSERT INTO StateInitialiserState (Name, AlertToCompletionTime, CompletionTime, LastUpdate, StateInitialiserId, OrderId, isDeleted) VALUES ('Site Survey Completed',3, 10, getdate(), (SELECT ID FROM Stateinitialisers WHERE Name = 'General'), 7,0 )");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
