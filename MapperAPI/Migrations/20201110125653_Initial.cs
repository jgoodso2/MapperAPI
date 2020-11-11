using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MapperAPI.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
               name: "AdminUser",
               columns: table => new
               {
                   Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                   Account34 = table.Column<string>(maxLength: 50, nullable: false)
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_AdminUsers", x => x.Id);
               });


            migrationBuilder.CreateTable(
                name: "AuthorizedPlanViewProjects",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ppl_Code = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    plan_id = table.Column<string>(nullable: true),
                    proj_mgr = table.Column<string>(nullable: true),
                    projectSponsor = table.Column<string>(nullable: true),
                    alt_pm = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorizedPlanViewProjects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    ProjectGuid = table.Column<Guid>(nullable: false),
                    ProjectName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.ProjectGuid);
                });

            migrationBuilder.CreateTable(
                name: "PlanViewProjects",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 200, nullable: false),
                    ProjectName = table.Column<string>(maxLength: 50, nullable: false),
                    ppl_Code = table.Column<string>(nullable: true),
                    ProjectGuid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanViewProjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlanViewProjects_Projects_ProjectGuid",
                        column: x => x.ProjectGuid,
                        principalTable: "Projects",
                        principalColumn: "ProjectGuid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlanViewProjects_ProjectGuid",
                table: "PlanViewProjects",
                column: "ProjectGuid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthorizedPlanViewProjects");

            migrationBuilder.DropTable(
                name: "PlanViewProjects");

            migrationBuilder.DropTable(
                name: "Projects");
        }
    }
}
