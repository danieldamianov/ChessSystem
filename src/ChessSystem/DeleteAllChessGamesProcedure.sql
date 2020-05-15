CREATE PROCEDURE deleteAllChessGames
 AS 
 DELETE FROM NormalMoves
 DELETE FROM CastlingMoves
 DELETE FROM PawnProductionMoves
 DELETE FROM ChessGames

 EXEC deleteAllChessGames