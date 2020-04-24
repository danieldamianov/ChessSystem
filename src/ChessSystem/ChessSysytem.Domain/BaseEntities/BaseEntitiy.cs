namespace ChessSystem.Domain.BaseEntities
{
    public abstract class BaseEntitiy<TKey>
    {
        public TKey Id { get; set; }
    }
}
