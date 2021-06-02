using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LeaveRequest.Migrations
{
    public partial class updateemployee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_M_Department",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_M_Department", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TB_M_NationalHoliday",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_M_NationalHoliday", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TB_M_Parameter",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Value = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_M_Parameter", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TB_M_Role",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_M_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TB_M_Employee",
                columns: table => new
                {
                    NIK = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    BirthDate = table.Column<DateTime>(nullable: false),
                    Gender = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    MaritialStatus = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    JoinDate = table.Column<DateTime>(nullable: false),
                    RemainingQuota = table.Column<int>(nullable: false),
                    NIK_Manager = table.Column<string>(nullable: true),
                    DepartmentId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_M_Employee", x => x.NIK);
                    table.ForeignKey(
                        name: "FK_TB_M_Employee_TB_M_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "TB_M_Department",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_M_Employee_TB_M_Employee_NIK_Manager",
                        column: x => x.NIK_Manager,
                        principalTable: "TB_M_Employee",
                        principalColumn: "NIK",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TB_M_Account",
                columns: table => new
                {
                    NIK = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_M_Account", x => x.NIK);
                    table.ForeignKey(
                        name: "FK_TB_M_Account_TB_M_Employee_NIK",
                        column: x => x.NIK,
                        principalTable: "TB_M_Employee",
                        principalColumn: "NIK",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_T_EmployeeRole",
                columns: table => new
                {
                    NIK = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeNIK = table.Column<string>(nullable: true),
                    RoleId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_T_EmployeeRole", x => x.NIK);
                    table.ForeignKey(
                        name: "FK_TB_T_EmployeeRole_TB_M_Employee_EmployeeNIK",
                        column: x => x.EmployeeNIK,
                        principalTable: "TB_M_Employee",
                        principalColumn: "NIK",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_T_EmployeeRole_TB_M_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "TB_M_Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_T_Request",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    ReasonRequest = table.Column<string>(nullable: true),
                    EmployeeNIK = table.Column<string>(nullable: true),
                    Notes = table.Column<string>(nullable: true),
                    StatusRequest = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_T_Request", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TB_T_Request_TB_M_Employee_EmployeeNIK",
                        column: x => x.EmployeeNIK,
                        principalTable: "TB_M_Employee",
                        principalColumn: "NIK",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_M_Employee_DepartmentId",
                table: "TB_M_Employee",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_M_Employee_NIK_Manager",
                table: "TB_M_Employee",
                column: "NIK_Manager");

            migrationBuilder.CreateIndex(
                name: "IX_TB_T_EmployeeRole_EmployeeNIK",
                table: "TB_T_EmployeeRole",
                column: "EmployeeNIK");

            migrationBuilder.CreateIndex(
                name: "IX_TB_T_EmployeeRole_RoleId",
                table: "TB_T_EmployeeRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_T_Request_EmployeeNIK",
                table: "TB_T_Request",
                column: "EmployeeNIK");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_M_Account");

            migrationBuilder.DropTable(
                name: "TB_M_NationalHoliday");

            migrationBuilder.DropTable(
                name: "TB_M_Parameter");

            migrationBuilder.DropTable(
                name: "TB_T_EmployeeRole");

            migrationBuilder.DropTable(
                name: "TB_T_Request");

            migrationBuilder.DropTable(
                name: "TB_M_Role");

            migrationBuilder.DropTable(
                name: "TB_M_Employee");

            migrationBuilder.DropTable(
                name: "TB_M_Department");
        }
    }
}
