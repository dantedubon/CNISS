using CNISS.CommonDomain.Ports.Output.Database;
using CNISS.EnterpriseDomain.Domain;
using CNISS.EnterpriseDomain.Domain.Entities;
using CNISS.EnterpriseDomain.Domain.Repositories;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using FluentNHibernate.Conventions.AcceptanceCriteria;
using NHibernate;

namespace CNISS.EnterpriseDomain.Ports.Output
{
    public class GremioRepositoryCommands : NHibernateCommandRepository<Gremio, RTN>, IGremioRepositoryCommands
    {
        private readonly IRepresentanteLegalRepositoryReadOnly _representanteLegalRepositoryRead;
        public GremioRepositoryCommands(ISession session, IRepresentanteLegalRepositoryReadOnly representanteLegalRepositoryRead) : base(session)
        {
            _representanteLegalRepositoryRead = representanteLegalRepositoryRead;
        }

        public void update(Gremio entity)
        {
            var representante = entity.representanteLegal;
            if (!isRepresentantExisting(representante.Id))
                _session.Save(representante);
            _session.Update(entity);

        }

        public void save(Gremio entity)
        {
            var direccion = entity.direccion;

            var representante = entity.representanteLegal;
            if (!isRepresentantExisting(representante.Id))
                _session.Save(representante);
            _session.Save(direccion);
            _session.Save(entity);
        }

        private bool isRepresentantExisting(Identidad id)
        {
            return _representanteLegalRepositoryRead.exists(id);
        }


    }
}