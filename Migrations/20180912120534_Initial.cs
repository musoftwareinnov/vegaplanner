using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace vega.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Notes = table.Column<string>(maxLength: 1024, nullable: true),
                    SearchCriteria = table.Column<string>(nullable: true),
                    CustomerAddress_AddressLine1 = table.Column<string>(maxLength: 255, nullable: true),
                    CustomerAddress_AddressLine2 = table.Column<string>(nullable: true),
                    CustomerAddress_CompanyName = table.Column<string>(maxLength: 255, nullable: true),
                    CustomerAddress_GeoLocation = table.Column<string>(maxLength: 20, nullable: true),
                    CustomerAddress_Postcode = table.Column<string>(maxLength: 10, nullable: true),
                    CustomerContact_EmailAddress = table.Column<string>(maxLength: 30, nullable: true),
                    CustomerContact_FirstName = table.Column<string>(maxLength: 30, nullable: true),
                    CustomerContact_LastName = table.Column<string>(maxLength: 30, nullable: true),
                    CustomerContact_TelephoneHome = table.Column<string>(maxLength: 30, nullable: true),
                    CustomerContact_TelephoneMobile = table.Column<string>(maxLength: 30, nullable: true),
                    CustomerContact_TelephoneWork = table.Column<string>(maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Features",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Features", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Makes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Makes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StateInitialiserCustomFields",
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
                    table.PrimaryKey("PK_StateInitialiserCustomFields", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StateInitialisers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    LastUpdate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StateInitialisers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StateStatus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GroupType = table.Column<string>(nullable: true),
                    LastUpdate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    OrderId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StateStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Models",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MakeId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Models", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Models_Makes_MakeId",
                        column: x => x.MakeId,
                        principalTable: "Makes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StateInitialiserState",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AlertToCompletionTime = table.Column<int>(nullable: false),
                    CompletionTime = table.Column<int>(nullable: false),
                    LastUpdate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    OrderId = table.Column<int>(nullable: false),
                    StateInitialiserId = table.Column<int>(nullable: false),
                    isDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StateInitialiserState", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StateInitialiserState_StateInitialisers_StateInitialiserId",
                        column: x => x.StateInitialiserId,
                        principalTable: "StateInitialisers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlanningApps",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ApplicationNo = table.Column<string>(nullable: true),
                    CurrentPlanningStatusId = table.Column<int>(nullable: false),
                    CustomerId = table.Column<int>(nullable: false),
                    LastUpdate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    Notes = table.Column<string>(nullable: true),
                    StateInitialiserId = table.Column<int>(nullable: false),
                    DevelopmentAddress_AddressLine1 = table.Column<string>(maxLength: 255, nullable: true),
                    DevelopmentAddress_AddressLine2 = table.Column<string>(nullable: true),
                    DevelopmentAddress_CompanyName = table.Column<string>(maxLength: 255, nullable: true),
                    DevelopmentAddress_GeoLocation = table.Column<string>(maxLength: 20, nullable: true),
                    DevelopmentAddress_Postcode = table.Column<string>(maxLength: 10, nullable: true),
                    Developer_EmailAddress = table.Column<string>(maxLength: 30, nullable: true),
                    Developer_FirstName = table.Column<string>(maxLength: 30, nullable: true),
                    Developer_LastName = table.Column<string>(maxLength: 30, nullable: true),
                    Developer_TelephoneHome = table.Column<string>(maxLength: 30, nullable: true),
                    Developer_TelephoneMobile = table.Column<string>(maxLength: 30, nullable: true),
                    Developer_TelephoneWork = table.Column<string>(maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanningApps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlanningApps_StateStatus_CurrentPlanningStatusId",
                        column: x => x.CurrentPlanningStatusId,
                        principalTable: "StateStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlanningApps_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlanningApps_StateInitialisers_StateInitialiserId",
                        column: x => x.StateInitialiserId,
                        principalTable: "StateInitialisers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsRegistered = table.Column<bool>(nullable: false),
                    LastUpdate = table.Column<DateTime>(nullable: false),
                    ModelId = table.Column<int>(nullable: false),
                    Contact_EmailAddress = table.Column<string>(maxLength: 30, nullable: true),
                    Contact_FirstName = table.Column<string>(maxLength: 30, nullable: true),
                    Contact_LastName = table.Column<string>(maxLength: 30, nullable: true),
                    Contact_TelephoneHome = table.Column<string>(maxLength: 30, nullable: true),
                    Contact_TelephoneMobile = table.Column<string>(maxLength: 30, nullable: true),
                    Contact_TelephoneWork = table.Column<string>(maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vehicles_Models_ModelId",
                        column: x => x.ModelId,
                        principalTable: "Models",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StateInitialiserStateCustomField",
                columns: table => new
                {
                    StateInitialiserStateId = table.Column<int>(nullable: false),
                    StateInitialiserCustomFieldId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StateInitialiserStateCustomField", x => new { x.StateInitialiserStateId, x.StateInitialiserCustomFieldId });
                    table.ForeignKey(
                        name: "FK_StateInitialiserStateCustomField_StateInitialiserCustomFields_StateInitialiserCustomFieldId",
                        column: x => x.StateInitialiserCustomFieldId,
                        principalTable: "StateInitialiserCustomFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StateInitialiserStateCustomField_StateInitialiserState_StateInitialiserStateId",
                        column: x => x.StateInitialiserStateId,
                        principalTable: "StateInitialiserState",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Drawings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FileName = table.Column<string>(maxLength: 255, nullable: false),
                    PlanningAppId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drawings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Drawings_PlanningApps_PlanningAppId",
                        column: x => x.PlanningAppId,
                        principalTable: "PlanningApps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlanningAppState",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CompletionDate = table.Column<DateTime>(nullable: true),
                    CurrentState = table.Column<bool>(nullable: false),
                    CustomDuration = table.Column<int>(nullable: false),
                    CustomDurationSet = table.Column<bool>(nullable: false),
                    DueByDate = table.Column<DateTime>(nullable: false),
                    Notes = table.Column<string>(nullable: true),
                    PlanningAppId = table.Column<int>(nullable: false),
                    StateInitialiserStateId = table.Column<int>(nullable: false),
                    StateStatusId = table.Column<int>(nullable: false),
                    userModifiedDate = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanningAppState", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlanningAppState_PlanningApps_PlanningAppId",
                        column: x => x.PlanningAppId,
                        principalTable: "PlanningApps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_PlanningAppState_StateInitialiserState_StateInitialiserStateId",
                        column: x => x.StateInitialiserStateId,
                        principalTable: "StateInitialiserState",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlanningAppState_StateStatus_StateStatusId",
                        column: x => x.StateStatusId,
                        principalTable: "StateStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FileName = table.Column<string>(maxLength: 255, nullable: false),
                    VehicleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Photos_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VehicleFeatures",
                columns: table => new
                {
                    VehicleId = table.Column<int>(nullable: false),
                    FeatureId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleFeatures", x => new { x.VehicleId, x.FeatureId });
                    table.ForeignKey(
                        name: "FK_VehicleFeatures_Features_FeatureId",
                        column: x => x.FeatureId,
                        principalTable: "Features",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VehicleFeatures_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlanningAppStateCustomFields",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateValue = table.Column<DateTime>(nullable: false),
                    IntValue = table.Column<int>(nullable: false),
                    PlanningAppStateId = table.Column<int>(nullable: true),
                    StateInitialiserStateCustomFieldId = table.Column<int>(nullable: false),
                    StrValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanningAppStateCustomFields", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlanningAppStateCustomFields_PlanningAppState_PlanningAppStateId",
                        column: x => x.PlanningAppStateId,
                        principalTable: "PlanningAppState",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Drawings_PlanningAppId",
                table: "Drawings",
                column: "PlanningAppId");

            migrationBuilder.CreateIndex(
                name: "IX_Models_MakeId",
                table: "Models",
                column: "MakeId");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_VehicleId",
                table: "Photos",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanningApps_CurrentPlanningStatusId",
                table: "PlanningApps",
                column: "CurrentPlanningStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanningApps_CustomerId",
                table: "PlanningApps",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanningApps_StateInitialiserId",
                table: "PlanningApps",
                column: "StateInitialiserId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanningAppState_PlanningAppId",
                table: "PlanningAppState",
                column: "PlanningAppId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanningAppState_StateInitialiserStateId",
                table: "PlanningAppState",
                column: "StateInitialiserStateId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanningAppState_StateStatusId",
                table: "PlanningAppState",
                column: "StateStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanningAppStateCustomFields_PlanningAppStateId",
                table: "PlanningAppStateCustomFields",
                column: "PlanningAppStateId");

            migrationBuilder.CreateIndex(
                name: "IX_StateInitialiserState_StateInitialiserId",
                table: "StateInitialiserState",
                column: "StateInitialiserId");

            migrationBuilder.CreateIndex(
                name: "IX_StateInitialiserStateCustomField_StateInitialiserCustomFieldId",
                table: "StateInitialiserStateCustomField",
                column: "StateInitialiserCustomFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleFeatures_FeatureId",
                table: "VehicleFeatures",
                column: "FeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_ModelId",
                table: "Vehicles",
                column: "ModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Drawings");

            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.DropTable(
                name: "PlanningAppStateCustomFields");

            migrationBuilder.DropTable(
                name: "StateInitialiserStateCustomField");

            migrationBuilder.DropTable(
                name: "VehicleFeatures");

            migrationBuilder.DropTable(
                name: "PlanningAppState");

            migrationBuilder.DropTable(
                name: "StateInitialiserCustomFields");

            migrationBuilder.DropTable(
                name: "Features");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "PlanningApps");

            migrationBuilder.DropTable(
                name: "StateInitialiserState");

            migrationBuilder.DropTable(
                name: "Models");

            migrationBuilder.DropTable(
                name: "StateStatus");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "StateInitialisers");

            migrationBuilder.DropTable(
                name: "Makes");
        }
    }
}
