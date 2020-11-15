﻿// <auto-generated />
using System;
using MapperAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MapperAPI.Migrations
{
    [DbContext(typeof(MapperContext))]
    partial class MapperContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MapperAPI.Entities.AdminUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("account34")
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.HasKey("Id");

                    b.ToTable("AdminUsers");
                });

            modelBuilder.Entity("MapperAPI.Entities.AuthorizedPlanViewProject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("alt_pm")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("plan_id")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ppl_Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("proj_mgr")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("projectSponsor")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AuthorizedPlanViewProjects");
                });

            modelBuilder.Entity("MapperAPI.Entities.PlanViewProject", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<Guid>("ProjectGuid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ProjectName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("mappedBy34")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("mappedByName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ppl_Code")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ProjectGuid");

                    b.ToTable("PlanViewProjects");
                });

            modelBuilder.Entity("MapperAPI.Entities.Project", b =>
                {
                    b.Property<Guid>("ProjectGuid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ProjectName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProjectGuid");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("MapperAPI.Entities.PlanViewProject", b =>
                {
                    b.HasOne("MapperAPI.Entities.Project", "Project")
                        .WithMany("PlanViewProjects")
                        .HasForeignKey("ProjectGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
