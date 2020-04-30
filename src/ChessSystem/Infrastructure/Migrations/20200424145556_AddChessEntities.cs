namespace Infrastructure.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddChessEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PagesOpened",
                table: "LogedInUsers");

            migrationBuilder.CreateTable(
                name: "ChessBoardPositions",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Horizontal = table.Column<string>(nullable: false),
                    Vertical = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChessBoardPositions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChessGames",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    WhitePlayerId = table.Column<string>(nullable: true),
                    BlackPlayerId = table.Column<string>(nullable: true),
                    EndGameInfo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChessGames", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CastlingMoves",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    OrderInTheGame = table.Column<int>(nullable: false),
                    ChessGameId = table.Column<string>(nullable: true),
                    ColorOfTheFigures = table.Column<int>(nullable: false),
                    KingInitialPositionId = table.Column<string>(nullable: true),
                    KingTargetPositionnId = table.Column<string>(nullable: true),
                    RookInitialPositionnId = table.Column<string>(nullable: true),
                    RookTargetPositionnId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CastlingMoves", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CastlingMoves_ChessGames_ChessGameId",
                        column: x => x.ChessGameId,
                        principalTable: "ChessGames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CastlingMoves_ChessBoardPositions_KingInitialPositionId",
                        column: x => x.KingInitialPositionId,
                        principalTable: "ChessBoardPositions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CastlingMoves_ChessBoardPositions_KingTargetPositionnId",
                        column: x => x.KingTargetPositionnId,
                        principalTable: "ChessBoardPositions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CastlingMoves_ChessBoardPositions_RookInitialPositionnId",
                        column: x => x.RookInitialPositionnId,
                        principalTable: "ChessBoardPositions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CastlingMoves_ChessBoardPositions_RookTargetPositionnId",
                        column: x => x.RookTargetPositionnId,
                        principalTable: "ChessBoardPositions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NormalMoves",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    OrderInTheGame = table.Column<int>(nullable: false),
                    ChessGameId = table.Column<string>(nullable: true),
                    InitialPositionId = table.Column<string>(nullable: true),
                    TargetPositionId = table.Column<string>(nullable: true),
                    ChessFigureType = table.Column<int>(nullable: false),
                    ChessFigureColor = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NormalMoves", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NormalMoves_ChessGames_ChessGameId",
                        column: x => x.ChessGameId,
                        principalTable: "ChessGames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NormalMoves_ChessBoardPositions_InitialPositionId",
                        column: x => x.InitialPositionId,
                        principalTable: "ChessBoardPositions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NormalMoves_ChessBoardPositions_TargetPositionId",
                        column: x => x.TargetPositionId,
                        principalTable: "ChessBoardPositions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PawnProductionMoves",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    OrderInTheGame = table.Column<int>(nullable: false),
                    ChessGameId = table.Column<string>(nullable: true),
                    ChessBoardPositionId = table.Column<string>(nullable: true),
                    FigureThatHasBeenProduced = table.Column<int>(nullable: false),
                    ColorOFTheFigures = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PawnProductionMoves", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PawnProductionMoves_ChessBoardPositions_ChessBoardPositionId",
                        column: x => x.ChessBoardPositionId,
                        principalTable: "ChessBoardPositions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PawnProductionMoves_ChessGames_ChessGameId",
                        column: x => x.ChessGameId,
                        principalTable: "ChessGames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CastlingMoves_ChessGameId",
                table: "CastlingMoves",
                column: "ChessGameId");

            migrationBuilder.CreateIndex(
                name: "IX_CastlingMoves_KingInitialPositionId",
                table: "CastlingMoves",
                column: "KingInitialPositionId",
                unique: true,
                filter: "[KingInitialPositionId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CastlingMoves_KingTargetPositionnId",
                table: "CastlingMoves",
                column: "KingTargetPositionnId",
                unique: true,
                filter: "[KingTargetPositionnId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CastlingMoves_RookInitialPositionnId",
                table: "CastlingMoves",
                column: "RookInitialPositionnId",
                unique: true,
                filter: "[RookInitialPositionnId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CastlingMoves_RookTargetPositionnId",
                table: "CastlingMoves",
                column: "RookTargetPositionnId",
                unique: true,
                filter: "[RookTargetPositionnId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_NormalMoves_ChessGameId",
                table: "NormalMoves",
                column: "ChessGameId");

            migrationBuilder.CreateIndex(
                name: "IX_NormalMoves_InitialPositionId",
                table: "NormalMoves",
                column: "InitialPositionId",
                unique: true,
                filter: "[InitialPositionId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_NormalMoves_TargetPositionId",
                table: "NormalMoves",
                column: "TargetPositionId",
                unique: true,
                filter: "[TargetPositionId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PawnProductionMoves_ChessBoardPositionId",
                table: "PawnProductionMoves",
                column: "ChessBoardPositionId",
                unique: true,
                filter: "[ChessBoardPositionId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PawnProductionMoves_ChessGameId",
                table: "PawnProductionMoves",
                column: "ChessGameId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CastlingMoves");

            migrationBuilder.DropTable(
                name: "NormalMoves");

            migrationBuilder.DropTable(
                name: "PawnProductionMoves");

            migrationBuilder.DropTable(
                name: "ChessBoardPositions");

            migrationBuilder.DropTable(
                name: "ChessGames");

            migrationBuilder.AddColumn<int>(
                name: "PagesOpened",
                table: "LogedInUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
