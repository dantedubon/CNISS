namespace CNISS.CommonDomain.Domain
{
    public abstract class Entity<TKey> : IDomainObjectIdentified<TKey>
    {
        public virtual TKey Id { get;  set; }
    }
}