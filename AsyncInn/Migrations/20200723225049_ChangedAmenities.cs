using Microsoft.EntityFrameworkCore.Migrations;

namespace Async_Inn.Migrations
{
    public partial class ChangedAmenities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomAmenities_Amenities_AmenityID",
                table: "RoomAmenities");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomAmenities_Rooms_RoomID",
                table: "RoomAmenities");

            migrationBuilder.RenameColumn(
                name: "RoomID",
                table: "RoomAmenities",
                newName: "RoomId");

            migrationBuilder.RenameColumn(
                name: "AmenityID",
                table: "RoomAmenities",
                newName: "AmenityId");

            migrationBuilder.RenameIndex(
                name: "IX_RoomAmenities_RoomID",
                table: "RoomAmenities",
                newName: "IX_RoomAmenities_RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomAmenities_Amenities_AmenityId",
                table: "RoomAmenities",
                column: "AmenityId",
                principalTable: "Amenities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomAmenities_Rooms_RoomId",
                table: "RoomAmenities",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomAmenities_Amenities_AmenityId",
                table: "RoomAmenities");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomAmenities_Rooms_RoomId",
                table: "RoomAmenities");

            migrationBuilder.RenameColumn(
                name: "RoomId",
                table: "RoomAmenities",
                newName: "RoomID");

            migrationBuilder.RenameColumn(
                name: "AmenityId",
                table: "RoomAmenities",
                newName: "AmenityID");

            migrationBuilder.RenameIndex(
                name: "IX_RoomAmenities_RoomId",
                table: "RoomAmenities",
                newName: "IX_RoomAmenities_RoomID");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomAmenities_Amenities_AmenityID",
                table: "RoomAmenities",
                column: "AmenityID",
                principalTable: "Amenities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomAmenities_Rooms_RoomID",
                table: "RoomAmenities",
                column: "RoomID",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
