﻿using System.Collections.Generic;
using System.Linq;
using CNISS.CommonDomain.Ports.Output.Database;
using CNISS.EnterpriseDomain.Domain.Repositories;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using NHibernate;
using NHibernate.Linq;

namespace CNISS.EnterpriseDomain.Ports.Output.Database
{
    public class DepartamentRepositoryReadOnly:NHibernateReadOnlyRepository<Departamento,string>, IDepartamentRepositoryReadOnly
    {
        public DepartamentRepositoryReadOnly(ISession session) : base(session)
        {
           
        }



       /* public override IEnumerable<Departamento> getAll()
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
        }*/


        private bool isMunicipioFromDepartamento(string idMunicipio, string departamentoId)
        {
            var departamento = Session.Get<Departamento>(departamentoId);
            return departamento != null && departamento.municipios.Any(x => x.Id == idMunicipio && x.departamentoId == departamentoId);
        }

        public bool isMunicipioFromDepartamento(Municipio _municipio)
        {
            return isMunicipioFromDepartamento(_municipio.Id, _municipio.departamentoId);
        }
    }
}