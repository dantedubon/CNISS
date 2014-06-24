using CNISS.CommonDomain.Domain;
using CNISS.EnterpriseDomain.Domain.Entities;

namespace CNISS.EnterpriseDomain.Domain.Repositories
{
    public interface IGremioRepositoryCommands:IRepositoryCommands<Gremio>
    {
        void updateDireccion(Gremio gremio);
        void updateRepresentante(Gremio gremio);
    }
}