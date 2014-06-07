namespace CNISS.CommonDomain.Domain
{
    public interface IDomainObjectIdentified<Tkey>
    {
        Tkey Id { get; }
    }
}
