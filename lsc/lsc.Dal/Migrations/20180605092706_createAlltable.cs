using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace bnuxq.Dal.Migrations
{
    public partial class createAlltable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Districtinfos",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true),
                    Pid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Districtinfos", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "EmailResourcess",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true),
                    Password = table.Column<string>(type: "varchar(11)", maxLength: 11, nullable: true),
                    Port = table.Column<string>(type: "varchar(6)", maxLength: 6, nullable: true),
                    SenderServerIp = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true),
                    UserName = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailResourcess", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmailTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EmailContent = table.Column<string>(type: "varchar(2048)", maxLength: 2048, nullable: true),
                    Title = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailTemplates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EnterCustContactss",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Address = table.Column<string>(type: "longtext", nullable: true),
                    Business = table.Column<string>(type: "longtext", nullable: true),
                    Department = table.Column<string>(type: "longtext", nullable: true),
                    Duties = table.Column<string>(type: "longtext", nullable: true),
                    Email = table.Column<string>(type: "longtext", nullable: true),
                    EnterCustID = table.Column<int>(type: "int", nullable: false),
                    Landline = table.Column<string>(type: "longtext", nullable: true),
                    Name = table.Column<string>(type: "longtext", nullable: true),
                    QQ = table.Column<string>(type: "longtext", nullable: true),
                    Rem = table.Column<string>(type: "longtext", nullable: true),
                    Sex = table.Column<int>(type: "int", nullable: false),
                    Telephone = table.Column<string>(type: "longtext", nullable: true),
                    WeChart = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnterCustContactss", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "EnterCustomers",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Abbreviation = table.Column<string>(type: "longtext", nullable: true),
                    Address = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    City = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreateUserID = table.Column<int>(type: "int", nullable: false),
                    CustAbstract = table.Column<string>(type: "varchar(1024)", maxLength: 1024, nullable: true),
                    CustomerType = table.Column<int>(type: "int", nullable: false),
                    DegreeOfHeat = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: true),
                    EnterName = table.Column<string>(type: "varchar(126)", maxLength: 126, nullable: true),
                    FaxNumber = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: true),
                    HeatMsg = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    HeatTYPE = table.Column<int>(type: "int", nullable: false),
                    InvoiceMsg = table.Column<string>(type: "varchar(1024)", maxLength: 1024, nullable: true),
                    IsHeat = table.Column<bool>(type: "bit", nullable: false),
                    Landline = table.Column<string>(type: "longtext", nullable: true),
                    Phase = table.Column<int>(type: "int", nullable: false),
                    Province = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: true),
                    Relationship = table.Column<int>(type: "int", nullable: false),
                    Rem = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    Source = table.Column<int>(type: "int", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    Telephone = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: true),
                    UpdateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    ValueGrade = table.Column<int>(type: "int", nullable: false),
                    WebSit = table.Column<string>(type: "varchar(126)", maxLength: 126, nullable: true),
                    ZipCode = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnterCustomers", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "EnterCustPhaseLogs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EnterCustomerID = table.Column<int>(type: "int", nullable: false),
                    Phase = table.Column<int>(type: "int", nullable: false),
                    Rem = table.Column<string>(type: "longtext", nullable: true),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnterCustPhaseLogs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ModuleInfos",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModuleInfos", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Options",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Content = table.Column<string>(type: "varchar(126)", maxLength: 126, nullable: true),
                    IsOk = table.Column<bool>(type: "bit", nullable: false),
                    ItemIndex = table.Column<string>(type: "varchar(2)", maxLength: 2, nullable: true),
                    QuestionsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Options", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuestionsDbSet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Content = table.Column<string>(type: "varchar(126)", maxLength: 126, nullable: true),
                    QuestionsType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionsDbSet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReceivedPaymentsLogs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Amt = table.Column<double>(type: "double", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Rem = table.Column<string>(type: "longtext", nullable: true),
                    SalesProjectID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceivedPaymentsLogs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SalesProjects",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreateUserID = table.Column<int>(type: "int", nullable: false),
                    EnterCustomerID = table.Column<int>(type: "int", nullable: false),
                    HeadID = table.Column<int>(type: "int", nullable: false),
                    ProjectAbstract = table.Column<string>(type: "longtext", nullable: true),
                    ProjectAmt = table.Column<double>(type: "double", nullable: false),
                    ProjectState = table.Column<int>(type: "int", nullable: false),
                    ProjectTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ProjectType = table.Column<int>(type: "int", nullable: false),
                    ReceoverPay = table.Column<double>(type: "double", nullable: false),
                    ReceoverPayTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Title = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesProjects", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SalesProjectStateLogs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ProjectState = table.Column<int>(type: "int", nullable: false),
                    Rem = table.Column<string>(type: "longtext", nullable: true),
                    SalesProjectID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesProjectStateLogs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SendEmailLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true),
                    EmailTempId = table.Column<int>(type: "int", nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    IsSend = table.Column<bool>(type: "bit", nullable: false),
                    IsSendOk = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: true),
                    SendEmailTaskId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SendEmailLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SendEmailTasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EmailTempId = table.Column<int>(type: "int", nullable: false),
                    TaskName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SendEmailTasks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TargetEmails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true),
                    Name = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TargetEmails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserAccounts",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Password = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAccounts", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "UserAnswer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Content = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true),
                    IsOk = table.Column<bool>(type: "bit", nullable: false),
                    LogId = table.Column<int>(type: "int", nullable: false),
                    OptionId = table.Column<int>(type: "int", nullable: false),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    Score = table.Column<double>(type: "double", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAnswer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserAnswerLog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Duration = table.Column<double>(type: "double", nullable: false),
                    TotalScore = table.Column<double>(type: "double", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAnswerLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserQuestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    LogId = table.Column<int>(type: "int", nullable: false),
                    QIndex = table.Column<int>(type: "int", nullable: false),
                    QuestionsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserQuestions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserRoleJurisdictions",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IsAdd = table.Column<bool>(type: "bit", nullable: false),
                    IsAssignment = table.Column<bool>(type: "bit", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    IsEdit = table.Column<bool>(type: "bit", nullable: false),
                    IsQuery = table.Column<bool>(type: "bit", nullable: false),
                    ModuleID = table.Column<int>(type: "int", nullable: false),
                    UserRoleID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoleJurisdictions", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RoleName = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: true),
                    State = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Name = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: true),
                    RoleID = table.Column<int>(type: "int", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    TargetAmt = table.Column<double>(type: "double", nullable: false),
                    TelPhone = table.Column<string>(type: "varchar(11)", maxLength: 11, nullable: true),
                    UserName = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "WorkPlans",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EnterCustID = table.Column<int>(type: "int", nullable: false),
                    PlanContent = table.Column<string>(type: "longtext", nullable: true),
                    PlanTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    WorkPlanState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkPlans", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Districtinfos");

            migrationBuilder.DropTable(
                name: "EmailResourcess");

            migrationBuilder.DropTable(
                name: "EmailTemplates");

            migrationBuilder.DropTable(
                name: "EnterCustContactss");

            migrationBuilder.DropTable(
                name: "EnterCustomers");

            migrationBuilder.DropTable(
                name: "EnterCustPhaseLogs");

            migrationBuilder.DropTable(
                name: "ModuleInfos");

            migrationBuilder.DropTable(
                name: "Options");

            migrationBuilder.DropTable(
                name: "QuestionsDbSet");

            migrationBuilder.DropTable(
                name: "ReceivedPaymentsLogs");

            migrationBuilder.DropTable(
                name: "SalesProjects");

            migrationBuilder.DropTable(
                name: "SalesProjectStateLogs");

            migrationBuilder.DropTable(
                name: "SendEmailLogs");

            migrationBuilder.DropTable(
                name: "SendEmailTasks");

            migrationBuilder.DropTable(
                name: "TargetEmails");

            migrationBuilder.DropTable(
                name: "UserAccounts");

            migrationBuilder.DropTable(
                name: "UserAnswer");

            migrationBuilder.DropTable(
                name: "UserAnswerLog");

            migrationBuilder.DropTable(
                name: "UserQuestions");

            migrationBuilder.DropTable(
                name: "UserRoleJurisdictions");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "WorkPlans");
        }
    }
}
