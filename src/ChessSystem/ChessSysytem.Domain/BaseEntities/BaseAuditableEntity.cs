namespace ChessSystem.Domain.BaseEntities
{
    using System;

    using ChessSystem.Domain.Exceptions;

    public abstract class BaseAuditableEntity<TKey> : BaseEntitiy<TKey>, IAuditInfo
    {
        private string createdBy;
        private string modifiedBy;

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public string CreatedBy
        {
            get => this.createdBy;
            set => this.createdBy = value ?? throw new InvalidEntityException("User ID cannot be null.");
        }

        public string ModifiedBy
        {
            get => this.modifiedBy;
            set => this.modifiedBy = value ?? throw new InvalidEntityException("User ID cannot be null.");
        }
    }
}
