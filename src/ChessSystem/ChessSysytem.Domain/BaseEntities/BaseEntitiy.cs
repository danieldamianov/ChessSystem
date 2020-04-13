namespace ChessSystem.Domain.BaseEntities
{
    using ChessSystem.Domain.Exceptions;
    using System;
    using System.ComponentModel.DataAnnotations;

    public abstract class BaseEntitiy<TKey>
    {
        [Key]
        public TKey Id { get; set; }
    }
}
