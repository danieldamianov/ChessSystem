namespace ChessGameLogic.Validations
{
    internal static class PositionValidator
    {
        internal static bool ValidatePosition(char horizontal, int vertical)
        {
            if (horizontal < 'a' || horizontal > 'h')
            {
                return false;
            }

            if (vertical < 1 || vertical > 8)
            {
                return false;
            }

            return true;
        }
    }
}
