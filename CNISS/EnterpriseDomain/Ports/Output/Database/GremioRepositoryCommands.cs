using System;
using CNISS.CommonDomain.Ports.Output.Database;
using CNISS.EnterpriseDomain.Domain;
using CNISS.EnterpriseDomain.Domain.Entities;
using CNISS.EnterpriseDomain.Domain.Repositories;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using NHibernate;

namespace CNISS.EnterpriseDomain.Ports.Output
{
    public class GremioRepositoryCommands : NHibernateCommandRepository<Gremio, RTN>, IGremioRepositoryCommands
    {
        private readonly IRepresentanteLegalRepositoryReadOnly _representanteLegalRepositoryRead;
        private readonly IDireccionRepositoryReadOnly _direccionRepositoryRead;

        public GremioRepositoryCommands(ISession session, IRepresentanteLegalRepositoryReadOnly representanteLegalRepositoryRead, 
            IDireccionRepositoryReadOnly direccionRepositoryRead) : base(session)
        {
            _representanteLegalRepositoryRead = representanteLegalRepositoryRead;
            _direccionRepositoryRead = direccionRepositoryRead;
        }

        public void update(Gremio entity)
        {
            
            _session.Update(entity);

        }

        public void updateRepresentante(Gremio entity)
        {
            var representante = entity.RepresentanteLegal;
         
            _session.SaveOrUpdate(representante);
            update(entity);
        }

        public void updateDireccion(Gremio entity)
        {
            var direccion = entity.Direccion;
            _session.SaveOrUpdate(direccion);

            update(entity);

        }

        public void save(Gremio entity)
        {
            var direccion = entity.Direccion;

            var representante = entity.RepresentanteLegal;
            if (!isRepresentantExisting(representante.Id))
                _session.Save(representante);
            _session.Save(direccion);
            _session.Save(entity);
        }

        private bool isDireccionExisting(Guid id)
        {
            return _direccionRepositoryRead.exists(id);
        }

        private bool isRepresentantExisting(Identidad id)
        {
            return _representanteLegalRepositoryRead.exists(id);
        }


    }
}