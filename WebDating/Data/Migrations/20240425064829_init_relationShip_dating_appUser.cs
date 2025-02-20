using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebDating.Data.Migrations
{
    /// <inheritdoc />
    public partial class init_relationShip_dating_appUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserInterests_DatingProfile_DatingProfileId",
                table: "UserInterests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DatingProfile",
                table: "DatingProfile");

            migrationBuilder.RenameTable(
                name: "DatingProfile",
                newName: "DatingProfiles");

            migrationBuilder.AddColumn<int>(
                name: "DatingProfileId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "DatingProfiles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DatingProfiles",
                table: "DatingProfiles",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_DatingProfileId",
                table: "AspNetUsers",
                column: "DatingProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_DatingProfiles_UserId",
                table: "DatingProfiles",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_DatingProfiles_DatingProfileId",
                table: "AspNetUsers",
                column: "DatingProfileId",
                principalTable: "DatingProfiles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DatingProfiles_AspNetUsers_UserId",
                table: "DatingProfiles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserInterests_DatingProfiles_DatingProfileId",
                table: "UserInterests",
                column: "DatingProfileId",
                principalTable: "DatingProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_DatingProfiles_DatingProfileId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_DatingProfiles_AspNetUsers_UserId",
                table: "DatingProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserInterests_DatingProfiles_DatingProfileId",
                table: "UserInterests");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_DatingProfileId",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DatingProfiles",
                table: "DatingProfiles");

            migrationBuilder.DropIndex(
                name: "IX_DatingProfiles_UserId",
                table: "DatingProfiles");

            migrationBuilder.DropColumn(
                name: "DatingProfileId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "DatingProfiles");

            migrationBuilder.RenameTable(
                name: "DatingProfiles",
                newName: "DatingProfile");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DatingProfile",
                table: "DatingProfile",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserInterests_DatingProfile_DatingProfileId",
                table: "UserInterests",
                column: "DatingProfileId",
                principalTable: "DatingProfile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
