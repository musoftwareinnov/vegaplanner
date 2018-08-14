using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace vega.Migrations
{
    public partial class SeedDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
   migrationBuilder.Sql("INSERT INTO StateStatus (Name, LastUpdate) VALUES ('OnTime', getdate())");
          migrationBuilder.Sql("INSERT INTO StateStatus (Name, LastUpdate) VALUES ('Due', getdate())");
          migrationBuilder.Sql("INSERT INTO StateStatus (Name, LastUpdate) VALUES ('Overdue', getdate())");          
          migrationBuilder.Sql("INSERT INTO StateStatus (Name, LastUpdate) VALUES ('Complete', getdate())");  
          migrationBuilder.Sql("INSERT INTO StateStatus (Name, LastUpdate) VALUES ('Overran', getdate())");  
          migrationBuilder.Sql("INSERT INTO StateStatus (Name, LastUpdate) VALUES ('InProgress', getdate())"); 
          migrationBuilder.Sql("INSERT INTO StateStatus (Name, LastUpdate) VALUES ('Archived', getdate())"); 
          migrationBuilder.Sql("INSERT INTO StateStatus (Name, LastUpdate) VALUES ('Terminated', getdate())");
          migrationBuilder.Sql("INSERT INTO StateStatus (Name, LastUpdate) VALUES ('Error', getdate())");  

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
            migrationBuilder.Sql("INSERT INTO StateInitialiserState (Name, AlertToCompletionTime, CompletionTime, LastUpdate, StateInitialiserId, OrderId, isDeleted) VALUES ('Initial',3, 10, getdate(), (SELECT ID FROM Stateinitialisers WHERE Name = 'General'), 1,0)");
            migrationBuilder.Sql("INSERT INTO StateInitialiserState (Name, AlertToCompletionTime, CompletionTime, LastUpdate, StateInitialiserId, OrderId, isDeleted) VALUES ('EMail Sent',2, 15, getdate(), (SELECT ID FROM Stateinitialisers WHERE Name = 'General'), 2, 0)");
            migrationBuilder.Sql("INSERT INTO StateInitialiserState (Name, AlertToCompletionTime, CompletionTime, LastUpdate, StateInitialiserId, OrderId, isDeleted) VALUES ('Client Visited',3, 6, getdate(), (SELECT ID FROM Stateinitialisers WHERE Name = 'General'), 3,0)");
            migrationBuilder.Sql("INSERT INTO StateInitialiserState (Name, AlertToCompletionTime, CompletionTime, LastUpdate, StateInitialiserId, OrderId, isDeleted) VALUES ('T&C Sent',4, 10, getdate(), (SELECT ID FROM Stateinitialisers WHERE Name = 'General'), 4, 0)");
            migrationBuilder.Sql("INSERT INTO StateInitialiserState (Name, AlertToCompletionTime, CompletionTime, LastUpdate, StateInitialiserId, OrderId, isDeleted) VALUES ('T&C Received',5, 12, getdate(), (SELECT ID FROM Stateinitialisers WHERE Name = 'General'), 5, 0)");
            migrationBuilder.Sql("INSERT INTO StateInitialiserState (Name, AlertToCompletionTime, CompletionTime, LastUpdate, StateInitialiserId, OrderId, isDeleted) VALUES ('Site Survey Booked',3, 10, getdate(), (SELECT ID FROM Stateinitialisers WHERE Name = 'General'), 6, 0)");
            migrationBuilder.Sql("INSERT INTO StateInitialiserState (Name, AlertToCompletionTime, CompletionTime, LastUpdate, StateInitialiserId, OrderId, isDeleted) VALUES ('Site Survey Completed',3, 10, getdate(), (SELECT ID FROM Stateinitialisers WHERE Name = 'General'), 7, 0)");

            migrationBuilder.Sql("INSERT INTO Customers (FirstName, LastName, Address1, Address2, Postcode, TelephoneHome, TelephoneMobile,EmailAddress)  VALUES ('Paul', 'Scollay', 'TyGwyn', 'Rose Truro', 'TR4 9PF', '01872 572143', '0783828172', 'pscollay@yahoo.co.uk')");      
            migrationBuilder.Sql("INSERT INTO Customers (FirstName, LastName, Address1, Address2, Postcode, TelephoneHome, TelephoneMobile,EmailAddress)  VALUES ('Bob', 'Smith', '32 Acacia Drive', 'London', 'N1 6BL', '0208 342342', '0764352333', 'bsmith@yahoo.co.uk')");   

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
