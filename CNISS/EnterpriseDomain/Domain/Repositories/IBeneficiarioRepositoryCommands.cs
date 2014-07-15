using CNISS.CommonDomain.Domain;
using CNISS.EnterpriseDomain.Domain.Entities;

namespace CNISS.EnterpriseDomain.Domain.Repositories
{
    public interface IBeneficiarioRepositoryCommands:IRepositoryCommands<Beneficiario>
    {
        void updateInformationFromMovil(Beneficiario beneficiario);
    }
}