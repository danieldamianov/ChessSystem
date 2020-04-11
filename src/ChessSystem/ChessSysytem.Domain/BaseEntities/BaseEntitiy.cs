namespace ChessSystem.Domain.BaseEntities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public abstract class BaseEntitiy<TKey> : IAuditInfo
    {
        [Key]
        public TKey Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
