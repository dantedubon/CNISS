namespace CNISS.CommonDomain.Domain
{
    public interface IDomainObjectIdentified<Tkey>
    {
        Tkey idKey { get; }
    }
}
