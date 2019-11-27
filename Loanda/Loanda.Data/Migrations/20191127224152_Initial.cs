using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Loanda.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "Applicants",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "date", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "date", nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    DeletedOn = table.Column<DateTime>(type: "date", nullable: true),
                    EGN = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 120, nullable: false),
                    MiddleName = table.Column<string>(maxLength: 120, nullable: true),
                    LastName = table.Column<string>(maxLength: 120, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "date", nullable: true),
                    PhoneNumber = table.Column<string>(nullable: false),
                    Address = table.Column<string>(maxLength: 255, nullable: false),
                    City = table.Column<string>(maxLength: 40, nullable: false),
                    EmailAddress = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applicants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationStatuses",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    IsFirstLogin = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmailStatuses",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "public",
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "public",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                schema: "public",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "public",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                schema: "public",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "public",
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "public",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                schema: "public",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "public",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReceivedEmails",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    SenderEmail = table.Column<string>(nullable: true),
                    SenderName = table.Column<string>(nullable: true),
                    DateReceived = table.Column<string>(nullable: true),
                    Subject = table.Column<string>(nullable: true),
                    Body = table.Column<string>(nullable: true),
                    EmailStatusId = table.Column<int>(nullable: true, defaultValue: -1),
                    ApplicantId = table.Column<Guid>(nullable: true),
                    GmailEmailId = table.Column<string>(nullable: true),
                    TotalAttachments = table.Column<int>(nullable: false),
                    AttachmentsTotalSizeInMB = table.Column<double>(nullable: false),
                    ProcessedById = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "date", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "date", nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    DeletedOn = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceivedEmails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReceivedEmails_Applicants_ApplicantId",
                        column: x => x.ApplicantId,
                        principalSchema: "public",
                        principalTable: "Applicants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceivedEmails_EmailStatuses_EmailStatusId",
                        column: x => x.EmailStatusId,
                        principalSchema: "public",
                        principalTable: "EmailStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceivedEmails_AspNetUsers_ProcessedById",
                        column: x => x.ProcessedById,
                        principalSchema: "public",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmailAttachments",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    FileSizeInMb = table.Column<double>(nullable: false),
                    AttachmentName = table.Column<string>(nullable: false),
                    ReceivedEmailId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmailAttachments_ReceivedEmails_ReceivedEmailId",
                        column: x => x.ReceivedEmailId,
                        principalSchema: "public",
                        principalTable: "ReceivedEmails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LoanApplications",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "date", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "date", nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    DeletedOn = table.Column<DateTime>(type: "date", nullable: true),
                    ApplicantId = table.Column<Guid>(nullable: false),
                    ApplicationStatusId = table.Column<int>(nullable: false),
                    LoanAmount = table.Column<decimal>(nullable: false),
                    IsApproved = table.Column<bool>(nullable: true),
                    OpenedById = table.Column<string>(nullable: true),
                    ClosedById = table.Column<string>(nullable: true),
                    EmailId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanApplications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoanApplications_Applicants_ApplicantId",
                        column: x => x.ApplicantId,
                        principalSchema: "public",
                        principalTable: "Applicants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LoanApplications_ApplicationStatuses_ApplicationStatusId",
                        column: x => x.ApplicationStatusId,
                        principalSchema: "public",
                        principalTable: "ApplicationStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LoanApplications_AspNetUsers_ClosedById",
                        column: x => x.ClosedById,
                        principalSchema: "public",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LoanApplications_ReceivedEmails_EmailId",
                        column: x => x.EmailId,
                        principalSchema: "public",
                        principalTable: "ReceivedEmails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LoanApplications_AspNetUsers_OpenedById",
                        column: x => x.OpenedById,
                        principalSchema: "public",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                schema: "public",
                table: "ApplicationStatuses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { -1, "Processing" },
                    { -2, "Approved" },
                    { -3, "Closed" }
                });

            migrationBuilder.InsertData(
                schema: "public",
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "959596e5-93e4-4272-8cfb-6e71a4254370", "20d35162-b35c-4b2e-80c1-81a15bc1b2f3", "Administrator", "ADMINISTRATOR" },
                    { "5197310d-5d42-4337-bb59-2fd06e6a8fcd", "a3bc9d45-276b-442f-bc6b-b1a5763df30d", "User", "USER" }
                });

            migrationBuilder.InsertData(
                schema: "public",
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedOn", "DeletedOn", "Email", "EmailConfirmed", "IsDeleted", "IsFirstLogin", "LockoutEnabled", "LockoutEnd", "ModifiedOn", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "010dbb8b-5eb3-47ef-9c53-22018551b422", 0, "578ddff5-8f49-48f0-a5c8-69b2bbb20552", null, null, "admin@admin.com", false, false, false, false, null, null, "ADMIN@ADMIN.COM", "ADMIN100@ABV.BG", "AQAAAAEAACcQAAAAED1sUbN+sPoFdrWJk/ICn2vfzPbC9s6bV/ae5bApThXRg9oKJOUmtPfdAVyfpIsVRw==", null, false, "CFQ3UGEFW7IBYB5GSVN2XPABAZW4DMYC", false, "Admin100@abv.bg" },
                    { "31d4807f-7f5f-4ffa-90c1-a131e2d3855e", 0, "715dad2a-9a3f-4a7d-bca1-e40799bb172c", null, null, "user_pesho@abv.bg", false, false, false, false, null, null, "USER_PESHO@ABV.BG", "USER_PESHO@ABV.BG", "AQAAAAEAACcQAAAAECewgbwibVC/7nEpYLbJB26wOJyT9i8Dfcx6WFFCTnGy5xqwptVYNBIZEWK37eaaMA==", null, false, "WNDRYHCTXU3MSZ7NYBDFJQDL5VU2LBXS", false, "user_pesho@abv.bg" }
                });

            migrationBuilder.InsertData(
                schema: "public",
                table: "EmailStatuses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { -1, "Not Reviewed" },
                    { -2, "New" },
                    { -3, "Invalid" },
                    { -4, "Open" },
                    { -5, "Closed" }
                });

            migrationBuilder.InsertData(
                schema: "public",
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[,]
                {
                    { "010dbb8b-5eb3-47ef-9c53-22018551b422", "959596e5-93e4-4272-8cfb-6e71a4254370" },
                    { "31d4807f-7f5f-4ffa-90c1-a131e2d3855e", "5197310d-5d42-4337-bb59-2fd06e6a8fcd" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                schema: "public",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "public",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                schema: "public",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                schema: "public",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                schema: "public",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "public",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "public",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmailAttachments_ReceivedEmailId",
                schema: "public",
                table: "EmailAttachments",
                column: "ReceivedEmailId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanApplications_ApplicantId",
                schema: "public",
                table: "LoanApplications",
                column: "ApplicantId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanApplications_ApplicationStatusId",
                schema: "public",
                table: "LoanApplications",
                column: "ApplicationStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanApplications_ClosedById",
                schema: "public",
                table: "LoanApplications",
                column: "ClosedById");

            migrationBuilder.CreateIndex(
                name: "IX_LoanApplications_EmailId",
                schema: "public",
                table: "LoanApplications",
                column: "EmailId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LoanApplications_OpenedById",
                schema: "public",
                table: "LoanApplications",
                column: "OpenedById");

            migrationBuilder.CreateIndex(
                name: "IX_ReceivedEmails_ApplicantId",
                schema: "public",
                table: "ReceivedEmails",
                column: "ApplicantId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceivedEmails_EmailStatusId",
                schema: "public",
                table: "ReceivedEmails",
                column: "EmailStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceivedEmails_ProcessedById",
                schema: "public",
                table: "ReceivedEmails",
                column: "ProcessedById");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims",
                schema: "public");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims",
                schema: "public");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins",
                schema: "public");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles",
                schema: "public");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens",
                schema: "public");

            migrationBuilder.DropTable(
                name: "EmailAttachments",
                schema: "public");

            migrationBuilder.DropTable(
                name: "LoanApplications",
                schema: "public");

            migrationBuilder.DropTable(
                name: "AspNetRoles",
                schema: "public");

            migrationBuilder.DropTable(
                name: "ApplicationStatuses",
                schema: "public");

            migrationBuilder.DropTable(
                name: "ReceivedEmails",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Applicants",
                schema: "public");

            migrationBuilder.DropTable(
                name: "EmailStatuses",
                schema: "public");

            migrationBuilder.DropTable(
                name: "AspNetUsers",
                schema: "public");
        }
    }
}
