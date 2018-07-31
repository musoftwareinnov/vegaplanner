using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace vega.Migrations
{
    public partial class SeedCustomers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
migrationBuilder.Sql("INSERT INTO Customers (FirstName, LastName, Address1, Address2, Postcode, TelephoneHome, TelephoneMobile,EmailAddress)  VALUES ('Paul', 'Scollay', 'TyGwyn', 'Rose Truro', 'TR4 9PF', '01872 572143', '0783828172', 'pscollay@yahoo.co.uk')");      
          migrationBuilder.Sql("INSERT INTO Customers (FirstName, LastName, Address1, Address2, Postcode, TelephoneHome, TelephoneMobile,EmailAddress)  VALUES ('Bob', 'Smith', '32 Acacia Drive', 'London', 'N1 6BL', '0208 342342', '0764352333', 'bsmith@yahoo.co.uk')");   
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
