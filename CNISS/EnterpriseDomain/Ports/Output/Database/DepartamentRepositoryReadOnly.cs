using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using CNISS.CommonDomain.Ports.Output.Database;
using CNISS.EnterpriseDomain.Domain.Repositories;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;
using NHibernate.Mapping;
using NHibernate.Transform;

namespace CNISS.EnterpriseDomain.Ports.Output.Database
{
    public class DepartamentRepositoryReadOnly:NHibernateReadOnlyRepository<Departamento,string>, IDepartamentRepositoryReadOnly
    {
        public DepartamentRepositoryReadOnly(ISession session) : base(session)
        {
           
        }

        public override IEnumerable<Departamento> getAll()
        {
            var list = Session.CreateCriteria<Departamento>().List<Departamento>();
            var query = list.Select(x => new Departamento
            {
                Id = x.Id,
                nombre = x.nombre,
                municipios = x.municipios.Select( z=> new Municipio
                {
                   Id = z.Id,
                   departamentoId = z.departamentoId,
                   nombre = z.nombre
                })
            });
            return query.ToList();
        }
    }
}