using Microsoft.EntityFrameworkCore.Migrations;

namespace DebutWebAPI.Migrations
{
    public partial class JoiningTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SmartHomes",
                columns: table => new
                {
                    SmartHomeId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SmartHomeType = table.Column<int>(type: "int", nullable: false),
                    District = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmartHomes", x => x.SmartHomeId);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    RoomId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomType = table.Column<int>(type: "int", nullable: false),
                    OwnerIdCitizenId = table.Column<long>(type: "bigint", nullable: true),
                    SmartHomeId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.RoomId);
                    table.ForeignKey(
                        name: "FK_Rooms_Citizens_OwnerIdCitizenId",
                        column: x => x.OwnerIdCitizenId,
                        principalTable: "Citizens",
                        principalColumn: "CitizenId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Rooms_SmartHomes_SmartHomeId",
                        column: x => x.SmartHomeId,
                        principalTable: "SmartHomes",
                        principalColumn: "SmartHomeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SmartHomeCitizens",
                columns: table => new
                {
                    SmarHomeId = table.Column<long>(type: "bigint", nullable: false),
                    CitizenId = table.Column<long>(type: "bigint", nullable: false),
                    SmartHomeId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmartHomeCitizens", x => new { x.SmarHomeId, x.CitizenId });
                    table.ForeignKey(
                        name: "FK_SmartHomeCitizens_Citizens_CitizenId",
                        column: x => x.CitizenId,
                        principalTable: "Citizens",
                        principalColumn: "CitizenId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SmartHomeCitizens_SmartHomes_SmartHomeId",
                        column: x => x.SmartHomeId,
                        principalTable: "SmartHomes",
                        principalColumn: "SmartHomeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SmartHomeOwners",
                columns: table => new
                {
                    SmarHomeId = table.Column<long>(type: "bigint", nullable: false),
                    OwnerId = table.Column<long>(type: "bigint", nullable: false),
                    SmartHomeId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmartHomeOwners", x => new { x.SmarHomeId, x.OwnerId });
                    table.ForeignKey(
                        name: "FK_SmartHomeOwners_Citizens_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Citizens",
                        principalColumn: "CitizenId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SmartHomeOwners_SmartHomes_SmartHomeId",
                        column: x => x.SmartHomeId,
                        principalTable: "SmartHomes",
                        principalColumn: "SmartHomeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    DeviceId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeviceType = table.Column<int>(type: "int", nullable: false),
                    DeviceBrand = table.Column<int>(type: "int", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    RoomId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.DeviceId);
                    table.ForeignKey(
                        name: "FK_Devices_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "RoomId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RoomOwners",
                columns: table => new
                {
                    RoomId = table.Column<long>(type: "bigint", nullable: false),
                    OwnerId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomOwners", x => new { x.RoomId, x.OwnerId });
                    table.ForeignKey(
                        name: "FK_RoomOwners_Citizens_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Citizens",
                        principalColumn: "CitizenId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoomOwners_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "RoomId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoomDevices",
                columns: table => new
                {
                    RoomId = table.Column<long>(type: "bigint", nullable: false),
                    DeviceId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomDevices", x => new { x.RoomId, x.DeviceId });
                    table.ForeignKey(
                        name: "FK_RoomDevices_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "DeviceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoomDevices_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "RoomId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Devices_RoomId",
                table: "Devices",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomDevices_DeviceId",
                table: "RoomDevices",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomOwners_OwnerId",
                table: "RoomOwners",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_OwnerIdCitizenId",
                table: "Rooms",
                column: "OwnerIdCitizenId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_SmartHomeId",
                table: "Rooms",
                column: "SmartHomeId");

            migrationBuilder.CreateIndex(
                name: "IX_SmartHomeCitizens_CitizenId",
                table: "SmartHomeCitizens",
                column: "CitizenId");

            migrationBuilder.CreateIndex(
                name: "IX_SmartHomeCitizens_SmartHomeId",
                table: "SmartHomeCitizens",
                column: "SmartHomeId");

            migrationBuilder.CreateIndex(
                name: "IX_SmartHomeOwners_OwnerId",
                table: "SmartHomeOwners",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_SmartHomeOwners_SmartHomeId",
                table: "SmartHomeOwners",
                column: "SmartHomeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoomDevices");

            migrationBuilder.DropTable(
                name: "RoomOwners");

            migrationBuilder.DropTable(
                name: "SmartHomeCitizens");

            migrationBuilder.DropTable(
                name: "SmartHomeOwners");

            migrationBuilder.DropTable(
                name: "Devices");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "SmartHomes");
        }
    }
}
