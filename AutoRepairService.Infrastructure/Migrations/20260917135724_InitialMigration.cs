using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoRepairService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Parts_Table",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Part_Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parts_Table", x => x.Id);
                    table.CheckConstraint("CK_Parts_Quantity", "[Quantity] >= 0");
                });

            migrationBuilder.CreateTable(
                name: "Roles_Table",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Role = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles_Table", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User_table",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Email = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    PasswordHash = table.Column<string>(type: "varchar(250)", nullable: false),
                    Token = table.Column<string>(type: "varchar(max)", nullable: true),
                    RefreshToken = table.Column<string>(type: "varchar(500)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSDATETIME()"),
                    LastLoginAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsEmailVerified = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    EmailVerificationToken = table.Column<string>(type: "varchar(500)", nullable: true),
                    EmailVerificationTokenExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_table", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customer_Profile_Table",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Users_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DefaultAddress = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer_Profile_Table", x => x.Id);
                    table.UniqueConstraint("AK_Customer_Profile_Table_Users_Id", x => x.Users_Id);
                    table.ForeignKey(
                        name: "FK_Customer_Profile_Table_User_table_Users_Id",
                        column: x => x.Users_Id,
                        principalTable: "User_table",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Mechanic_Profile_Table",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Users_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Specialization = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ExperienceYears = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    HourlyRate = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0.00m),
                    Bio = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    IsVerified = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Rating = table.Column<decimal>(type: "decimal(3,2)", precision: 3, scale: 2, nullable: false, defaultValue: 0.00m),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Latitude = table.Column<decimal>(type: "decimal(9,6)", precision: 9, scale: 6, nullable: false),
                    Longitude = table.Column<decimal>(type: "decimal(9,6)", precision: 9, scale: 6, nullable: false),
                    CompletedJobsCount = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mechanic_Profile_Table", x => x.Id);
                    table.UniqueConstraint("AK_Mechanic_Profile_Table_Users_Id", x => x.Users_Id);
                    table.ForeignKey(
                        name: "FK_Mechanic_Profile_Table_User_table_Users_Id",
                        column: x => x.Users_Id,
                        principalTable: "User_table",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Profile_Table",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Users_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSDATETIME()"),
                    ProfileImage = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profile_Table", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Profile_Table_User_table_Users_Id",
                        column: x => x.Users_Id,
                        principalTable: "User_table",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users_Roles_Table",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Users_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Role_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users_Roles_Table", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_Table_Roles_Table_Role_Id",
                        column: x => x.Role_Id,
                        principalTable: "Roles_Table",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_Roles_Table_User_table_Users_Id",
                        column: x => x.Users_Id,
                        principalTable: "User_table",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vehicle_Table",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Users_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Model = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Engine = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Transmission = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PlateNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicle_Table", x => x.Id);
                    table.CheckConstraint("CK_Vehicle_Engine", "LOWER([Engine]) IN ('gasoline', 'diesel', 'hybrid', 'electric', 'plug-in hybrid')");
                    table.CheckConstraint("CK_Vehicle_PlateNumber", "[PlateNumber] LIKE '[A-Z][A-Z]-[0-9][0-9][0-9]-[A-Z][A-Z]' OR [PlateNumber] LIKE '[A-Z][A-Z][A-Z]-[0-9][0-9][0-9]'");
                    table.CheckConstraint("CK_Vehicle_Transmission", "LOWER([Transmission]) IN ('automatic', 'manual', 'cvt', 'semi-automatic')");
                    table.CheckConstraint("CK_Vehicle_Year", "[Year] <= YEAR(GETDATE())");
                    table.ForeignKey(
                        name: "FK_Vehicle_Table_User_table_Users_Id",
                        column: x => x.Users_Id,
                        principalTable: "User_table",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Card_Table",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Customer_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CardHolderName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Last4Digits = table.Column<string>(type: "char(4)", nullable: false),
                    CardBrand = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProcessorToken = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Card_Table", x => x.Id);
                    table.CheckConstraint("CK_Card_Last4Digits", "[Last4Digits] LIKE '[0-9][0-9][0-9][0-9]'");
                    table.ForeignKey(
                        name: "FK_Card_Table_Customer_Profile_Table_Customer_Id",
                        column: x => x.Customer_Id,
                        principalTable: "Customer_Profile_Table",
                        principalColumn: "Users_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Mechanic_Bank_Accounts_Table",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Mechanic_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IBAN = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Bank_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Holder_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mechanic_Bank_Accounts_Table", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mechanic_Bank_Accounts_Table_Mechanic_Profile_Table_Mechanic_Id",
                        column: x => x.Mechanic_Id,
                        principalTable: "Mechanic_Profile_Table",
                        principalColumn: "Users_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Service_Table",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Customer_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Mechanic_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Vehicle_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Requested_At = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSDATETIME()"),
                    Accepted_by_mechanic_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Done_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Total = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    EstimatedHours = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: true),
                    PaymentMethod = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ServicePrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    PartsPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Service_Table", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Service_Table_Customer_Profile_Table_Customer_Id",
                        column: x => x.Customer_Id,
                        principalTable: "Customer_Profile_Table",
                        principalColumn: "Users_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Service_Table_Mechanic_Profile_Table_Mechanic_Id",
                        column: x => x.Mechanic_Id,
                        principalTable: "Mechanic_Profile_Table",
                        principalColumn: "Users_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Service_Table_Vehicle_Table_Vehicle_Id",
                        column: x => x.Vehicle_Id,
                        principalTable: "Vehicle_Table",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PayMents_Table",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Customer_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Mechanic_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Service_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Customer_Card_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Mechanic_Account_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Status = table.Column<string>(type: "varchar(50)", nullable: false),
                    TransactionId = table.Column<string>(type: "varchar(50)", nullable: false),
                    Paid_At = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSDATETIME()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayMents_Table", x => x.Id);
                    table.CheckConstraint("CK_Payment_Status", "LOWER([Status]) IN ('Pending', 'Done', 'Rejected', 'CoudlNotMake')");
                    table.ForeignKey(
                        name: "FK_PayMents_Table_Card_Table_Customer_Card_Id",
                        column: x => x.Customer_Card_Id,
                        principalTable: "Card_Table",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PayMents_Table_Customer_Profile_Table_Customer_Id",
                        column: x => x.Customer_Id,
                        principalTable: "Customer_Profile_Table",
                        principalColumn: "Users_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PayMents_Table_Mechanic_Bank_Accounts_Table_Mechanic_Account_Id",
                        column: x => x.Mechanic_Account_Id,
                        principalTable: "Mechanic_Bank_Accounts_Table",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PayMents_Table_Mechanic_Profile_Table_Mechanic_Id",
                        column: x => x.Mechanic_Id,
                        principalTable: "Mechanic_Profile_Table",
                        principalColumn: "Users_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PayMents_Table_Service_Table_Service_Id",
                        column: x => x.Service_Id,
                        principalTable: "Service_Table",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Service_Parts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Part_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Service_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Service_Parts", x => x.Id);
                    table.CheckConstraint("CK_ServiceParts_Quantity", "[Quantity] > 0");
                    table.ForeignKey(
                        name: "FK_Service_Parts_Parts_Table_Part_Id",
                        column: x => x.Part_Id,
                        principalTable: "Parts_Table",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Service_Parts_Service_Table_Service_Id",
                        column: x => x.Service_Id,
                        principalTable: "Service_Table",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Card_Table_Customer_Id",
                table: "Card_Table",
                column: "Customer_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Card_Table_ProcessorToken",
                table: "Card_Table",
                column: "ProcessorToken",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customer_Profile_Table_Users_Id",
                table: "Customer_Profile_Table",
                column: "Users_Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Mechanic_Bank_Accounts_Table_IBAN",
                table: "Mechanic_Bank_Accounts_Table",
                column: "IBAN",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Mechanic_Bank_Accounts_Table_Mechanic_Id",
                table: "Mechanic_Bank_Accounts_Table",
                column: "Mechanic_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Mechanic_Profile_Table_Users_Id",
                table: "Mechanic_Profile_Table",
                column: "Users_Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PayMents_Table_Customer_Card_Id",
                table: "PayMents_Table",
                column: "Customer_Card_Id");

            migrationBuilder.CreateIndex(
                name: "IX_PayMents_Table_Customer_Id",
                table: "PayMents_Table",
                column: "Customer_Id");

            migrationBuilder.CreateIndex(
                name: "IX_PayMents_Table_Mechanic_Account_Id",
                table: "PayMents_Table",
                column: "Mechanic_Account_Id");

            migrationBuilder.CreateIndex(
                name: "IX_PayMents_Table_Mechanic_Id",
                table: "PayMents_Table",
                column: "Mechanic_Id");

            migrationBuilder.CreateIndex(
                name: "IX_PayMents_Table_Service_Id",
                table: "PayMents_Table",
                column: "Service_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Profile_Table_Users_Id",
                table: "Profile_Table",
                column: "Users_Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Service_Parts_Part_Id_Service_Id",
                table: "Service_Parts",
                columns: new[] { "Part_Id", "Service_Id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Service_Parts_Service_Id",
                table: "Service_Parts",
                column: "Service_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Service_Table_Customer_Id",
                table: "Service_Table",
                column: "Customer_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Service_Table_Mechanic_Id",
                table: "Service_Table",
                column: "Mechanic_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Service_Table_Vehicle_Id",
                table: "Service_Table",
                column: "Vehicle_Id");

            migrationBuilder.CreateIndex(
                name: "IX_User_table_Email",
                table: "User_table",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Roles_Table_Role_Id",
                table: "Users_Roles_Table",
                column: "Role_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Roles_Table_Users_Id_Role_Id",
                table: "Users_Roles_Table",
                columns: new[] { "Users_Id", "Role_Id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vehicle_Table_PlateNumber",
                table: "Vehicle_Table",
                column: "PlateNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vehicle_Table_Users_Id",
                table: "Vehicle_Table",
                column: "Users_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PayMents_Table");

            migrationBuilder.DropTable(
                name: "Profile_Table");

            migrationBuilder.DropTable(
                name: "Service_Parts");

            migrationBuilder.DropTable(
                name: "Users_Roles_Table");

            migrationBuilder.DropTable(
                name: "Card_Table");

            migrationBuilder.DropTable(
                name: "Mechanic_Bank_Accounts_Table");

            migrationBuilder.DropTable(
                name: "Parts_Table");

            migrationBuilder.DropTable(
                name: "Service_Table");

            migrationBuilder.DropTable(
                name: "Roles_Table");

            migrationBuilder.DropTable(
                name: "Customer_Profile_Table");

            migrationBuilder.DropTable(
                name: "Mechanic_Profile_Table");

            migrationBuilder.DropTable(
                name: "Vehicle_Table");

            migrationBuilder.DropTable(
                name: "User_table");
        }
    }
}
