namespace ChessSystem.Domain.BaseEntities
{
    using System;

    public abstract class BaseDeletableEntity<TKey> : BaseEntitiy<TKey>, IDeletableEntity
    {
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
