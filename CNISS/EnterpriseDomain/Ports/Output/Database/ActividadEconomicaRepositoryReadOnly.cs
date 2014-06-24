using System;
using System.Collections.Generic;
using System.Linq;
using CNISS.CommonDomain.Ports.Output.Database;
using CNISS.EnterpriseDomain.Domain.Repositories;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using NHibernate;
using NHibernate.Linq;

namespace CNISS.EnterpriseDomain.Ports.Output.Database
{
    public class ActividadEconomicaRepositoryReadOnly :NHibernateReadOnlyRepository<ActividadEconomica,Guid>, IActividadEconomicaRepositoryReadOnly
    {
        public ActividadEconomicaRepositoryReadOnly(ISession session) : base(session)
        {
        }


        public bool existsAll(IEnumerable<ActividadEconomica> actividades)
        {
            var listaExistente = Session.Query<ActividadEconomica>().Select(x=> x.Id).ToList();
            var resultado = actividades.Select(x => x.Id).Except(listaExistente);

            return !resultado.Any();
        }
    }
}