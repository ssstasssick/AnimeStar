using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class NewTestMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_animeAndStudios_Animes_AnimeId",
                table: "animeAndStudios");

            migrationBuilder.DropForeignKey(
                name: "FK_animeAndStudios_Studios_StudioId",
                table: "animeAndStudios");

            migrationBuilder.DropForeignKey(
                name: "FK_Animes_MPAA_MPAAId",
                table: "Animes");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonalList_Animes_AnimeId",
                table: "PersonalList");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonalList_AspNetUsers_UserId1",
                table: "PersonalList");

            migrationBuilder.DropPrimaryKey(
                name: "PK_animeAndStudios",
                table: "animeAndStudios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonalList",
                table: "PersonalList");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MPAA",
                table: "MPAA");

            migrationBuilder.RenameTable(
                name: "animeAndStudios",
                newName: "AnimeAndStudios");

            migrationBuilder.RenameTable(
                name: "PersonalList",
                newName: "PersonalLists");

            migrationBuilder.RenameTable(
                name: "MPAA",
                newName: "MPAAs");

            migrationBuilder.RenameIndex(
                name: "IX_animeAndStudios_StudioId",
                table: "AnimeAndStudios",
                newName: "IX_AnimeAndStudios_StudioId");

            migrationBuilder.RenameIndex(
                name: "IX_animeAndStudios_AnimeId",
                table: "AnimeAndStudios",
                newName: "IX_AnimeAndStudios_AnimeId");

            migrationBuilder.RenameIndex(
                name: "IX_PersonalList_UserId1",
                table: "PersonalLists",
                newName: "IX_PersonalLists_UserId1");

            migrationBuilder.RenameIndex(
                name: "IX_PersonalList_AnimeId",
                table: "PersonalLists",
                newName: "IX_PersonalLists_AnimeId");

            migrationBuilder.AlterColumn<string>(
                name: "UserId1",
                table: "PersonalLists",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AnimeAndStudios",
                table: "AnimeAndStudios",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonalLists",
                table: "PersonalLists",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MPAAs",
                table: "MPAAs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AnimeAndStudios_Animes_AnimeId",
                table: "AnimeAndStudios",
                column: "AnimeId",
                principalTable: "Animes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AnimeAndStudios_Studios_StudioId",
                table: "AnimeAndStudios",
                column: "StudioId",
                principalTable: "Studios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Animes_MPAAs_MPAAId",
                table: "Animes",
                column: "MPAAId",
                principalTable: "MPAAs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalLists_Animes_AnimeId",
                table: "PersonalLists",
                column: "AnimeId",
                principalTable: "Animes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalLists_AspNetUsers_UserId1",
                table: "PersonalLists",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnimeAndStudios_Animes_AnimeId",
                table: "AnimeAndStudios");

            migrationBuilder.DropForeignKey(
                name: "FK_AnimeAndStudios_Studios_StudioId",
                table: "AnimeAndStudios");

            migrationBuilder.DropForeignKey(
                name: "FK_Animes_MPAAs_MPAAId",
                table: "Animes");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonalLists_Animes_AnimeId",
                table: "PersonalLists");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonalLists_AspNetUsers_UserId1",
                table: "PersonalLists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AnimeAndStudios",
                table: "AnimeAndStudios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonalLists",
                table: "PersonalLists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MPAAs",
                table: "MPAAs");

            migrationBuilder.RenameTable(
                name: "AnimeAndStudios",
                newName: "animeAndStudios");

            migrationBuilder.RenameTable(
                name: "PersonalLists",
                newName: "PersonalList");

            migrationBuilder.RenameTable(
                name: "MPAAs",
                newName: "MPAA");

            migrationBuilder.RenameIndex(
                name: "IX_AnimeAndStudios_StudioId",
                table: "animeAndStudios",
                newName: "IX_animeAndStudios_StudioId");

            migrationBuilder.RenameIndex(
                name: "IX_AnimeAndStudios_AnimeId",
                table: "animeAndStudios",
                newName: "IX_animeAndStudios_AnimeId");

            migrationBuilder.RenameIndex(
                name: "IX_PersonalLists_UserId1",
                table: "PersonalList",
                newName: "IX_PersonalList_UserId1");

            migrationBuilder.RenameIndex(
                name: "IX_PersonalLists_AnimeId",
                table: "PersonalList",
                newName: "IX_PersonalList_AnimeId");

            migrationBuilder.AlterColumn<string>(
                name: "UserId1",
                table: "PersonalList",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_animeAndStudios",
                table: "animeAndStudios",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonalList",
                table: "PersonalList",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MPAA",
                table: "MPAA",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_animeAndStudios_Animes_AnimeId",
                table: "animeAndStudios",
                column: "AnimeId",
                principalTable: "Animes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_animeAndStudios_Studios_StudioId",
                table: "animeAndStudios",
                column: "StudioId",
                principalTable: "Studios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Animes_MPAA_MPAAId",
                table: "Animes",
                column: "MPAAId",
                principalTable: "MPAA",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalList_Animes_AnimeId",
                table: "PersonalList",
                column: "AnimeId",
                principalTable: "Animes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalList_AspNetUsers_UserId1",
                table: "PersonalList",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
