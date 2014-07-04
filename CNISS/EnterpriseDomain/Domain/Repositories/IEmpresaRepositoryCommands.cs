using CNISS.CommonDomain.Domain;
using CNISS.EnterpriseDomain.Domain.Entities;
using CNISS.EnterpriseDomain.Domain.ValueObjects;

namespace CNISS.EnterpriseDomain.Domain.Repositories
{
    public  interface IEmpresaRepositoryCommands:IRepositoryCommands<Empresa>
    {
        void updateContrato(RTN id, ContentFile nuevoContrato);
    }
}