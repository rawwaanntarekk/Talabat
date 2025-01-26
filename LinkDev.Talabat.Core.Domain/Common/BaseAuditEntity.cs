namespace LinkDev.Talabat.Core.Domain.Common
{
    public abstract class BaseAuditEntity<TKey> : BaseEntity<TKey> 
                          where TKey : IEquatable<TKey>
    {
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public string LastModifiedBy { get; set; } = null!;
        public DateTime LastModifiedOn { get; set; } = DateTime.UtcNow;

    }
}   
