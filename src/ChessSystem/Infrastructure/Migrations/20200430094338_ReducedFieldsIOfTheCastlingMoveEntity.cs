namespace Infrastructure.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class ReducedFieldsIOfTheCastlingMoveEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CastlingMoves_ChessBoardPositions_KingTargetPositionnId",
                table: "CastlingMoves");

            migrationBuilder.DropForeignKey(
                name: "FK_CastlingMoves_ChessBoardPositions_RookTargetPositionnId",
                table: "CastlingMoves");

            migrationBuilder.DropIndex(
                name: "IX_CastlingMoves_KingTargetPositionnId",
                table: "CastlingMoves");

            migrationBuilder.DropIndex(
                name: "IX_CastlingMoves_RookTargetPositionnId",
                table: "CastlingMoves");

            migrationBuilder.DropColumn(
                name: "KingTargetPositionnId",
                table: "CastlingMoves");

            migrationBuilder.DropColumn(
                name: "RookTargetPositionnId",
                table: "CastlingMoves");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "KingTargetPositionnId",
                table: "CastlingMoves",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RookTargetPositionnId",
                table: "CastlingMoves",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CastlingMoves_KingTargetPositionnId",
                table: "CastlingMoves",
                column: "KingTargetPositionnId",
                unique: true,
                filter: "[KingTargetPositionnId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CastlingMoves_RookTargetPositionnId",
                table: "CastlingMoves",
                column: "RookTargetPositionnId",
                unique: true,
                filter: "[RookTargetPositionnId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_CastlingMoves_ChessBoardPositions_KingTargetPositionnId",
                table: "CastlingMoves",
                column: "KingTargetPositionnId",
                principalTable: "ChessBoardPositions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CastlingMoves_ChessBoardPositions_RookTargetPositionnId",
                table: "CastlingMoves",
                column: "RookTargetPositionnId",
                principalTable: "ChessBoardPositions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
