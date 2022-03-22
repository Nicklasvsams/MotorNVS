using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MotorNVS.DAL.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Fuel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FuelName = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fuel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Login",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Login", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Zipcode",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ZipcodeNo = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zipcode", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vehicle",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Make = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "date", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    FuelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vehicle_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vehicle_Fuel_FuelId",
                        column: x => x.FuelId,
                        principalTable: "Fuel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StreetAndNo = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "date", nullable: false),
                    ZipCodeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Address_Zipcode_ZipCodeId",
                        column: x => x.ZipCodeId,
                        principalTable: "Zipcode",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "date", nullable: false),
                    AddressId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customer_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Registration",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegistrationDate = table.Column<DateTime>(type: "date", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    VehicleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registration", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Registration_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Registration_Vehicle_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "CategoryName" },
                values: new object[,]
                {
                    { 1, "Convertible" },
                    { 2, "SUV" }
                });

            migrationBuilder.InsertData(
                table: "Fuel",
                columns: new[] { "Id", "FuelName" },
                values: new object[,]
                {
                    { 1, "Gasoline" },
                    { 2, "Diesel" }
                });

            migrationBuilder.InsertData(
                table: "Login",
                columns: new[] { "Id", "Password", "Username" },
                values: new object[] { 1, "Passw0rd", "admin" });

            migrationBuilder.InsertData(
                table: "Zipcode",
                columns: new[] { "Id", "City", "ZipcodeNo" },
                values: new object[,]
                {
                    { 1, "København SV", "2450" },
                    { 2, "Frederiksværk", "3300" }
                });

            migrationBuilder.InsertData(
                table: "Address",
                columns: new[] { "Id", "CreateDate", "StreetAndNo", "ZipCodeId" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 3, 16, 20, 56, 25, 244, DateTimeKind.Local).AddTicks(176), "Bakkevej 18", 2 },
                    { 2, new DateTime(2022, 3, 16, 20, 56, 25, 244, DateTimeKind.Local).AddTicks(252), "Pladehalebakke 15", 1 }
                });

            migrationBuilder.InsertData(
                table: "Vehicle",
                columns: new[] { "Id", "CategoryId", "CreateDate", "FuelId", "Make", "Model" },
                values: new object[,]
                {
                    { 1, 2, new DateTime(2022, 3, 16, 20, 56, 25, 244, DateTimeKind.Local).AddTicks(601), 1, "Suzuki", "Vitara" },
                    { 2, 1, new DateTime(2022, 3, 16, 20, 56, 25, 244, DateTimeKind.Local).AddTicks(624), 2, "Volkswagen", "Beetle Turbo" }
                });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Id", "AddressId", "CreateDate", "FirstName", "LastName" },
                values: new object[] { 1, 2, new DateTime(2022, 3, 16, 20, 56, 25, 244, DateTimeKind.Local).AddTicks(354), "Nicklas", "Sams" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Id", "AddressId", "CreateDate", "FirstName", "LastName" },
                values: new object[] { 2, 1, new DateTime(2022, 3, 16, 20, 56, 25, 244, DateTimeKind.Local).AddTicks(374), "Henning", "Bjarkesen" });

            migrationBuilder.InsertData(
                table: "Registration",
                columns: new[] { "Id", "CustomerId", "RegistrationDate", "VehicleId" },
                values: new object[] { 1, 1, new DateTime(2022, 3, 16, 20, 56, 25, 244, DateTimeKind.Local).AddTicks(712), 2 });

            migrationBuilder.InsertData(
                table: "Registration",
                columns: new[] { "Id", "CustomerId", "RegistrationDate", "VehicleId" },
                values: new object[] { 2, 2, new DateTime(2022, 3, 16, 20, 56, 25, 244, DateTimeKind.Local).AddTicks(730), 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Address_ZipCodeId",
                table: "Address",
                column: "ZipCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_AddressId",
                table: "Customer",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Registration_CustomerId",
                table: "Registration",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Registration_VehicleId",
                table: "Registration",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicle_CategoryId",
                table: "Vehicle",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicle_FuelId",
                table: "Vehicle",
                column: "FuelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Login");

            migrationBuilder.DropTable(
                name: "Registration");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Vehicle");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Fuel");

            migrationBuilder.DropTable(
                name: "Zipcode");
        }
    }
}
