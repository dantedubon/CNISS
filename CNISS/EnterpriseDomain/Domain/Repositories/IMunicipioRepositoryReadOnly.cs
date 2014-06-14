using CNISS.CommonDomain.Domain;
using CNISS.EnterpriseDomain.Domain.ValueObjects;

namespace CNISS.EnterpriseDomain.Domain.Repositories
{
    public interface IMunicipioRepositoryReadOnly:IRepositoryReadOnlyCompoundKey<Municipio,string,string>
    {
    }
}