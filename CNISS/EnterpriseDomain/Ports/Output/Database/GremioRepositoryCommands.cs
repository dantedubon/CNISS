using CNISS.CommonDomain.Ports.Output.Database;
using CNISS.EnterpriseDomain.Domain;
using CNISS.EnterpriseDomain.Domain.Entities;
using CNISS.EnterpriseDomain.Domain.Repositories;
using NHibernate;

namespace CNISS.EnterpriseDomain.Ports.Output
{
    public class GremioRepositoryCommands : NHibernateCommandRepository<Gremio, RTN>, IGremioRespositoryCommands
    {
        private readonly IRepresentanteLegalRepositoryReadOnly _representanteLegalRepositoryRead;
        public GremioRepositoryCommands(ISession session, IRepresentanteLegalRepositoryReadOnly representanteLegalRepositoryRead) : base(session)
        {
            _representanteLegalRepositoryRead = representanteLegalRepositoryRead;
        }

        public void save(Gremio entity)
        {
            var direccion = entity.direccion;

            var representante = entity.representanteLegal;
            var existRepresentante = _representanteLegalRepositoryRead.exists(representante.Id);
            if(!existRepresentante)
                _session.Save(representante);
            _session.Save(direccion);
            _session.Save(entity);
        }
    }
}