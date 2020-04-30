namespace ChessWebApplication.Controllers.Game.InputModels
{
    using System.ComponentModel.DataAnnotations;

    public class PlayInputModel
    {
        [Required]
        [RegularExpression(@"[\dabcdef]{8}-[\dabcdef]{4}-[\dabcdef]{4}-[\dabcdef]{4}-[\dabcdef]{12}")]
        public string WhitePlayerId { get; set; }

        [Required]
        [RegularExpression(@"[\dabcdef]{8}-[\dabcdef]{4}-[\dabcdef]{4}-[\dabcdef]{4}-[\dabcdef]{12}")]
        public string BlackPlayerId { get; set; }

        [Required]
        [RegularExpression(@"(black|white)")]
        public string PlayerColor { get; set; }
    }
}
