using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class playlist : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Playlist_Users_CreatedById",
                table: "Playlist");

            migrationBuilder.DropForeignKey(
                name: "FK_Playlist_Image_FrontImageId",
                table: "Playlist");

            migrationBuilder.DropForeignKey(
                name: "FK_Track_Playlist_PlaylistId",
                table: "Track");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Playlist",
                table: "Playlist");

            migrationBuilder.RenameTable(
                name: "Playlist",
                newName: "Playlists");

            migrationBuilder.RenameIndex(
                name: "IX_Playlist_FrontImageId",
                table: "Playlists",
                newName: "IX_Playlists_FrontImageId");

            migrationBuilder.RenameIndex(
                name: "IX_Playlist_CreatedById",
                table: "Playlists",
                newName: "IX_Playlists_CreatedById");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Playlists",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Playlists",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Playlists",
                table: "Playlists",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "PlayerFavouritePlaylists",
                columns: table => new
                {
                    PlayerId = table.Column<int>(nullable: false),
                    FavouritePlaylistId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerFavouritePlaylists", x => new { x.PlayerId, x.FavouritePlaylistId });
                    table.ForeignKey(
                        name: "FK_PlayerFavouritePlaylists_Playlists_FavouritePlaylistId",
                        column: x => x.FavouritePlaylistId,
                        principalTable: "Playlists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerFavouritePlaylists_Users_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayerFavouritePlaylists_FavouritePlaylistId",
                table: "PlayerFavouritePlaylists",
                column: "FavouritePlaylistId");

            migrationBuilder.AddForeignKey(
                name: "FK_Playlists_Users_CreatedById",
                table: "Playlists",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Playlists_Image_FrontImageId",
                table: "Playlists",
                column: "FrontImageId",
                principalTable: "Image",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Track_Playlists_PlaylistId",
                table: "Track",
                column: "PlaylistId",
                principalTable: "Playlists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Playlists_Users_CreatedById",
                table: "Playlists");

            migrationBuilder.DropForeignKey(
                name: "FK_Playlists_Image_FrontImageId",
                table: "Playlists");

            migrationBuilder.DropForeignKey(
                name: "FK_Track_Playlists_PlaylistId",
                table: "Track");

            migrationBuilder.DropTable(
                name: "PlayerFavouritePlaylists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Playlists",
                table: "Playlists");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Playlists");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Playlists");

            migrationBuilder.RenameTable(
                name: "Playlists",
                newName: "Playlist");

            migrationBuilder.RenameIndex(
                name: "IX_Playlists_FrontImageId",
                table: "Playlist",
                newName: "IX_Playlist_FrontImageId");

            migrationBuilder.RenameIndex(
                name: "IX_Playlists_CreatedById",
                table: "Playlist",
                newName: "IX_Playlist_CreatedById");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Playlist",
                table: "Playlist",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Playlist_Users_CreatedById",
                table: "Playlist",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Playlist_Image_FrontImageId",
                table: "Playlist",
                column: "FrontImageId",
                principalTable: "Image",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Track_Playlist_PlaylistId",
                table: "Track",
                column: "PlaylistId",
                principalTable: "Playlist",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
