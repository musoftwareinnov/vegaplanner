﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;
using vega.Persistence;

namespace vega.Migrations
{
    [DbContext(typeof(VegaDbContext))]
    partial class VegaDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("vega.Core.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address1")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("Address2");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("Notes")
                        .HasMaxLength(1024);

                    b.Property<string>("Postcode")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("SearchCriteria");

                    b.Property<string>("TelephoneHome")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("TelephoneMobile")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("vega.Core.Models.Drawing", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<int>("PlanningAppId");

                    b.HasKey("Id");

                    b.HasIndex("PlanningAppId");

                    b.ToTable("Drawings");
                });

            modelBuilder.Entity("vega.Core.Models.Feature", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.ToTable("Features");
                });

            modelBuilder.Entity("vega.Core.Models.Make", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.ToTable("Makes");
                });

            modelBuilder.Entity("vega.Core.Models.Model", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("MakeId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.HasIndex("MakeId");

                    b.ToTable("Models");
                });

            modelBuilder.Entity("vega.Core.Models.Photo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<int>("VehicleId");

                    b.HasKey("Id");

                    b.HasIndex("VehicleId");

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("vega.Core.Models.PlanningApp", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CurrentPlanningStatusId");

                    b.Property<int>("CustomerId");

                    b.Property<DateTime>("LastUpdate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("Notes");

                    b.Property<int>("StateInitialiserId");

                    b.HasKey("Id");

                    b.HasIndex("CurrentPlanningStatusId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("StateInitialiserId");

                    b.ToTable("PlanningApps");
                });

            modelBuilder.Entity("vega.Core.Models.PlanningAppState", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("CompletionDate");

                    b.Property<bool>("CurrentState");

                    b.Property<int>("CustomDuration");

                    b.Property<bool>("CustomDurationSet");

                    b.Property<DateTime>("DueByDate");

                    b.Property<string>("Notes");

                    b.Property<int>("PlanningAppId");

                    b.Property<int>("StateInitialiserStateId");

                    b.Property<int>("StateStatusId");

                    b.Property<bool>("userModifiedDate");

                    b.HasKey("Id");

                    b.HasIndex("PlanningAppId");

                    b.HasIndex("StateInitialiserStateId");

                    b.HasIndex("StateStatusId");

                    b.ToTable("PlanningAppState");
                });

            modelBuilder.Entity("vega.Core.Models.StateInitialiser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<DateTime>("LastUpdate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.ToTable("StateInitialisers");
                });

            modelBuilder.Entity("vega.Core.Models.States.StateInitialiserState", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AlertToCompletionTime");

                    b.Property<int>("CompletionTime");

                    b.Property<DateTime>("LastUpdate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<int>("OrderId");

                    b.Property<int>("StateInitialiserId");

                    b.Property<bool>("isDeleted");

                    b.HasKey("Id");

                    b.HasIndex("StateInitialiserId");

                    b.ToTable("StateInitialiserState");
                });

            modelBuilder.Entity("vega.Core.Models.States.StateStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("GroupType");

                    b.Property<DateTime>("LastUpdate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<int>("OrderId");

                    b.HasKey("Id");

                    b.ToTable("StateStatus");
                });

            modelBuilder.Entity("vega.Core.Models.Vehicle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsRegistered");

                    b.Property<DateTime>("LastUpdate");

                    b.Property<int>("ModelId");

                    b.HasKey("Id");

                    b.HasIndex("ModelId");

                    b.ToTable("Vehicles");
                });

            modelBuilder.Entity("vega.Core.Models.VehicleFeature", b =>
                {
                    b.Property<int>("VehicleId");

                    b.Property<int>("FeatureId");

                    b.HasKey("VehicleId", "FeatureId");

                    b.HasIndex("FeatureId");

                    b.ToTable("VehicleFeatures");
                });

            modelBuilder.Entity("vega.Core.Models.Drawing", b =>
                {
                    b.HasOne("vega.Core.Models.PlanningApp")
                        .WithMany("Drawings")
                        .HasForeignKey("PlanningAppId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("vega.Core.Models.Model", b =>
                {
                    b.HasOne("vega.Core.Models.Make", "Make")
                        .WithMany("Models")
                        .HasForeignKey("MakeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("vega.Core.Models.Photo", b =>
                {
                    b.HasOne("vega.Core.Models.Vehicle")
                        .WithMany("Photos")
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("vega.Core.Models.PlanningApp", b =>
                {
                    b.HasOne("vega.Core.Models.States.StateStatus", "CurrentPlanningStatus")
                        .WithMany()
                        .HasForeignKey("CurrentPlanningStatusId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("vega.Core.Models.Customer", "Customer")
                        .WithMany("planningApps")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("vega.Core.Models.StateInitialiser", "StateInitialiser")
                        .WithMany()
                        .HasForeignKey("StateInitialiserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("vega.Core.Models.PlanningAppState", b =>
                {
                    b.HasOne("vega.Core.Models.PlanningApp", "PlanningApp")
                        .WithMany("PlanningAppStates")
                        .HasForeignKey("PlanningAppId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("vega.Core.Models.States.StateInitialiserState", "state")
                        .WithMany()
                        .HasForeignKey("StateInitialiserStateId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("vega.Core.Models.States.StateStatus", "StateStatus")
                        .WithMany()
                        .HasForeignKey("StateStatusId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("vega.Core.Models.States.StateInitialiserState", b =>
                {
                    b.HasOne("vega.Core.Models.StateInitialiser")
                        .WithMany("States")
                        .HasForeignKey("StateInitialiserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("vega.Core.Models.Vehicle", b =>
                {
                    b.HasOne("vega.Core.Models.Model", "Model")
                        .WithMany()
                        .HasForeignKey("ModelId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.OwnsOne("vega.Core.Models.Contact", "Contact", b1 =>
                        {
                            b1.Property<int>("VehicleId");

                            b1.Property<string>("Email")
                                .IsRequired()
                                .HasMaxLength(25);

                            b1.Property<int>("Id");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasMaxLength(255);

                            b1.Property<string>("Phone")
                                .IsRequired()
                                .HasMaxLength(255);

                            b1.ToTable("Vehicles");

                            b1.HasOne("vega.Core.Models.Vehicle")
                                .WithOne("Contact")
                                .HasForeignKey("vega.Core.Models.Contact", "VehicleId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });

            modelBuilder.Entity("vega.Core.Models.VehicleFeature", b =>
                {
                    b.HasOne("vega.Core.Models.Feature", "Feature")
                        .WithMany()
                        .HasForeignKey("FeatureId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("vega.Core.Models.Vehicle", "Vehicle")
                        .WithMany("Features")
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
