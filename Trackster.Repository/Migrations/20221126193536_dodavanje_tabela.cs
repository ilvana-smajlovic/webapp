using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trackster.Repository.Migrations
{
    /// <inheritdoc />
    public partial class dodavanjetabela : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Genders",
                columns: table => new
                {
                    GenderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GenderName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genders", x => x.GenderID);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    GenreID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GenreName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.GenreID);
                });

            migrationBuilder.CreateTable(
                name: "Pictures",
                columns: table => new
                {
                    PictureId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    File = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pictures", x => x.PictureId);
                });

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    RatingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RatingValue = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.RatingID);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    StateID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StateName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.StateID);
                });

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    StatusID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.StatusID);
                });

            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    PersonId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Birthday = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Bio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PictureId = table.Column<int>(type: "int", nullable: false),
                    GenderID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.PersonId);
                    table.ForeignKey(
                        name: "FK_People_Genders_GenderID",
                        column: x => x.GenderID,
                        principalTable: "Genders",
                        principalColumn: "GenderID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_People_Pictures_PictureId",
                        column: x => x.PictureId,
                        principalTable: "Pictures",
                        principalColumn: "PictureId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "RegisteredUsers",
                columns: table => new
                {
                    RegisteredUserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfilePicturePictureId = table.Column<int>(type: "int", nullable: false),
                    Bio = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegisteredUsers", x => x.RegisteredUserId);
                    table.ForeignKey(
                        name: "FK_RegisteredUsers_Pictures_ProfilePicturePictureId",
                        column: x => x.ProfilePicturePictureId,
                        principalTable: "Pictures",
                        principalColumn: "PictureId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Medias",
                columns: table => new
                {
                    MediaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AirDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Synopsis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rating = table.Column<float>(type: "real", nullable: false),
                    PosterPictureId = table.Column<int>(type: "int", nullable: false),
                    StatusID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medias", x => x.MediaId);
                    table.ForeignKey(
                        name: "FK_Medias_Pictures_PosterPictureId",
                        column: x => x.PosterPictureId,
                        principalTable: "Pictures",
                        principalColumn: "PictureId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Medias_Status_StatusID",
                        column: x => x.StatusID,
                        principalTable: "Status",
                        principalColumn: "StatusID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "GenreMedia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GenreID = table.Column<int>(type: "int", nullable: false),
                    MediaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenreMedia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GenreMedia_Genres_GenreID",
                        column: x => x.GenreID,
                        principalTable: "Genres",
                        principalColumn: "GenreID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_GenreMedia_Medias_MediaID",
                        column: x => x.MediaID,
                        principalTable: "Medias",
                        principalColumn: "MediaId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "MediaPersonRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MediaID = table.Column<int>(type: "int", nullable: false),
                    PersonID = table.Column<int>(type: "int", nullable: false),
                    RoleID = table.Column<int>(type: "int", nullable: false),
                    Character = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaPersonRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MediaPersonRoles_Medias_MediaID",
                        column: x => x.MediaID,
                        principalTable: "Medias",
                        principalColumn: "MediaId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_MediaPersonRoles_People_PersonID",
                        column: x => x.PersonID,
                        principalTable: "People",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_MediaPersonRoles_Roles_RoleID",
                        column: x => x.RoleID,
                        principalTable: "Roles",
                        principalColumn: "RoleID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    MovieID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MediaId = table.Column<int>(type: "int", nullable: false),
                    Runtime = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.MovieID);
                    table.ForeignKey(
                        name: "FK_Movies_Medias_MediaId",
                        column: x => x.MediaId,
                        principalTable: "Medias",
                        principalColumn: "MediaId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "TVShows",
                columns: table => new
                {
                    TVShowID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MediaId = table.Column<int>(type: "int", nullable: false),
                    SeasonCount = table.Column<int>(type: "int", nullable: true),
                    EpisodeCount = table.Column<int>(type: "int", nullable: true),
                    EpisodeRuntime = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TVShows", x => x.TVShowID);
                    table.ForeignKey(
                        name: "FK_TVShows_Medias_MediaId",
                        column: x => x.MediaId,
                        principalTable: "Medias",
                        principalColumn: "MediaId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "UserFavourites",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    MediaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFavourites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserFavourites_Medias_MediaID",
                        column: x => x.MediaID,
                        principalTable: "Medias",
                        principalColumn: "MediaId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_UserFavourites_RegisteredUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "RegisteredUsers",
                        principalColumn: "RegisteredUserId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "WatchlistMovies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    MovieID = table.Column<int>(type: "int", nullable: false),
                    StateID = table.Column<int>(type: "int", nullable: false),
                    RatingID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WatchlistMovies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WatchlistMovies_Movies_MovieID",
                        column: x => x.MovieID,
                        principalTable: "Movies",
                        principalColumn: "MovieID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_WatchlistMovies_Ratings_RatingID",
                        column: x => x.RatingID,
                        principalTable: "Ratings",
                        principalColumn: "RatingID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_WatchlistMovies_RegisteredUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "RegisteredUsers",
                        principalColumn: "RegisteredUserId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_WatchlistMovies_States_StateID",
                        column: x => x.StateID,
                        principalTable: "States",
                        principalColumn: "StateID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "WatchlistTVShows",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    TVShowID = table.Column<int>(type: "int", nullable: false),
                    StateID = table.Column<int>(type: "int", nullable: false),
                    RatingID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WatchlistTVShows", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WatchlistTVShows_Ratings_RatingID",
                        column: x => x.RatingID,
                        principalTable: "Ratings",
                        principalColumn: "RatingID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_WatchlistTVShows_RegisteredUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "RegisteredUsers",
                        principalColumn: "RegisteredUserId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_WatchlistTVShows_States_StateID",
                        column: x => x.StateID,
                        principalTable: "States",
                        principalColumn: "StateID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_WatchlistTVShows_TVShows_TVShowID",
                        column: x => x.TVShowID,
                        principalTable: "TVShows",
                        principalColumn: "TVShowID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GenreMedia_GenreID",
                table: "GenreMedia",
                column: "GenreID");

            migrationBuilder.CreateIndex(
                name: "IX_GenreMedia_MediaID",
                table: "GenreMedia",
                column: "MediaID");

            migrationBuilder.CreateIndex(
                name: "IX_MediaPersonRoles_MediaID",
                table: "MediaPersonRoles",
                column: "MediaID");

            migrationBuilder.CreateIndex(
                name: "IX_MediaPersonRoles_PersonID",
                table: "MediaPersonRoles",
                column: "PersonID");

            migrationBuilder.CreateIndex(
                name: "IX_MediaPersonRoles_RoleID",
                table: "MediaPersonRoles",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_Medias_PosterPictureId",
                table: "Medias",
                column: "PosterPictureId");

            migrationBuilder.CreateIndex(
                name: "IX_Medias_StatusID",
                table: "Medias",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_MediaId",
                table: "Movies",
                column: "MediaId");

            migrationBuilder.CreateIndex(
                name: "IX_People_GenderID",
                table: "People",
                column: "GenderID");

            migrationBuilder.CreateIndex(
                name: "IX_People_PictureId",
                table: "People",
                column: "PictureId");

            migrationBuilder.CreateIndex(
                name: "IX_RegisteredUsers_ProfilePicturePictureId",
                table: "RegisteredUsers",
                column: "ProfilePicturePictureId");

            migrationBuilder.CreateIndex(
                name: "IX_TVShows_MediaId",
                table: "TVShows",
                column: "MediaId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFavourites_MediaID",
                table: "UserFavourites",
                column: "MediaID");

            migrationBuilder.CreateIndex(
                name: "IX_UserFavourites_UserID",
                table: "UserFavourites",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_WatchlistMovies_MovieID",
                table: "WatchlistMovies",
                column: "MovieID");

            migrationBuilder.CreateIndex(
                name: "IX_WatchlistMovies_RatingID",
                table: "WatchlistMovies",
                column: "RatingID");

            migrationBuilder.CreateIndex(
                name: "IX_WatchlistMovies_StateID",
                table: "WatchlistMovies",
                column: "StateID");

            migrationBuilder.CreateIndex(
                name: "IX_WatchlistMovies_UserID",
                table: "WatchlistMovies",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_WatchlistTVShows_RatingID",
                table: "WatchlistTVShows",
                column: "RatingID");

            migrationBuilder.CreateIndex(
                name: "IX_WatchlistTVShows_StateID",
                table: "WatchlistTVShows",
                column: "StateID");

            migrationBuilder.CreateIndex(
                name: "IX_WatchlistTVShows_TVShowID",
                table: "WatchlistTVShows",
                column: "TVShowID");

            migrationBuilder.CreateIndex(
                name: "IX_WatchlistTVShows_UserID",
                table: "WatchlistTVShows",
                column: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GenreMedia");

            migrationBuilder.DropTable(
                name: "MediaPersonRoles");

            migrationBuilder.DropTable(
                name: "UserFavourites");

            migrationBuilder.DropTable(
                name: "WatchlistMovies");

            migrationBuilder.DropTable(
                name: "WatchlistTVShows");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "People");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "RegisteredUsers");

            migrationBuilder.DropTable(
                name: "States");

            migrationBuilder.DropTable(
                name: "TVShows");

            migrationBuilder.DropTable(
                name: "Genders");

            migrationBuilder.DropTable(
                name: "Medias");

            migrationBuilder.DropTable(
                name: "Pictures");

            migrationBuilder.DropTable(
                name: "Status");
        }
    }
}
