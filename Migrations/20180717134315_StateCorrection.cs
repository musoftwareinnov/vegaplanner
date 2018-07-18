using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace vega.Migrations
{
    public partial class StateCorrection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
          migrationBuilder.Sql("DELETE FROM StateInitialiserState WHERE StateInitialiserId = (SELECT ID FROM Stateinitialisers WHERE Name = 'General') ");
            migrationBuilder.Sql("INSERT INTO StateInitialiserState (Name, AlertToCompletionTime, CompletionTime, LastUpdate, StateInitialiserId, OrderId) VALUES ('Initial',3, 10, getdate(), (SELECT ID FROM Stateinitialisers WHERE Name = 'General'), 1)");
            migrationBuilder.Sql("INSERT INTO StateInitialiserState (Name, AlertToCompletionTime, CompletionTime, LastUpdate, StateInitialiserId, OrderId) VALUES ('EMail Sent',2, 15, getdate(), (SELECT ID FROM Stateinitialisers WHERE Name = 'General'), 2)");
            migrationBuilder.Sql("INSERT INTO StateInitialiserState (Name, AlertToCompletionTime, CompletionTime, LastUpdate, StateInitialiserId, OrderId) VALUES ('Client Visited',3, 6, getdate(), (SELECT ID FROM Stateinitialisers WHERE Name = 'General'), 3)");
            migrationBuilder.Sql("INSERT INTO StateInitialiserState (Name, AlertToCompletionTime, CompletionTime, LastUpdate, StateInitialiserId, OrderId) VALUES ('T&C Sent',4, 10, getdate(), (SELECT ID FROM Stateinitialisers WHERE Name = 'General'), 4)");
            migrationBuilder.Sql("INSERT INTO StateInitialiserState (Name, AlertToCompletionTime, CompletionTime, LastUpdate, StateInitialiserId, OrderId) VALUES ('T&C Received',5, 12, getdate(), (SELECT ID FROM Stateinitialisers WHERE Name = 'General'), 5)");
            migrationBuilder.Sql("INSERT INTO StateInitialiserState (Name, AlertToCompletionTime, CompletionTime, LastUpdate, StateInitialiserId, OrderId) VALUES ('Site Survey Booked',3, 10, getdate(), (SELECT ID FROM Stateinitialisers WHERE Name = 'General'), 6)");
            migrationBuilder.Sql("INSERT INTO StateInitialiserState (Name, AlertToCompletionTime, CompletionTime, LastUpdate, StateInitialiserId, OrderId) VALUES ('Site Survey Completed',3, 10, getdate(), (SELECT ID FROM Stateinitialisers WHERE Name = 'General'), 7)");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
