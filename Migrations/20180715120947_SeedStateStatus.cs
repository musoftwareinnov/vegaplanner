using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace vega.Migrations
{
    public partial class SeedStateStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
          migrationBuilder.Sql("INSERT INTO StateStatus (Name, LastUpdate) VALUES ('OnTime', getdate())");
          migrationBuilder.Sql("INSERT INTO StateStatus (Name, LastUpdate) VALUES ('Due', getdate())");
          migrationBuilder.Sql("INSERT INTO StateStatus (Name, LastUpdate) VALUES ('Overdue', getdate())");          
          migrationBuilder.Sql("INSERT INTO StateStatus (Name, LastUpdate) VALUES ('Complete', getdate())");  
          migrationBuilder.Sql("INSERT INTO StateStatus (Name, LastUpdate) VALUES ('Overran', getdate())");         
          migrationBuilder.Sql("INSERT INTO StateStatus (Name, LastUpdate) VALUES ('Error', getdate())");         
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
          migrationBuilder.Sql("Delete StateStatus");                  
        }
    }
}
