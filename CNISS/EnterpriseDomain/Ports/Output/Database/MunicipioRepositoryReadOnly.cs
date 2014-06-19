using System;
using System.Collections.Generic;
using CNISS.CommonDomain.Ports.Output.Database;
using CNISS.EnterpriseDomain.Domain.Repositories;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using NHibernate;

namespace CNISS.EnterpriseDomain.Ports.Output.Database
{
    public class MunicipioRepositoryReadOnly:NHibernateReadOnlyRepositoryCompoundKey<Municipio,string,string>,IMunicipioRepositoryReadOnly
    {
        public MunicipioRepositoryReadOnly(ISession session) : base(session)
        {
        }

        public override Municipio get(string departamentoId, string municipioId)
        {
         return   Session.Get<Municipio>(new Municipio() { departamentoId = departamentoId, Id = municipioId });

        }

        public override IEnumerable<Municipio> getAll()
        {
            throw new NotImplementedException();
        }

        public override bool exists(string departamentoId, string municipioId)
        {
            throw new NotImplementedException();
        }
    }
}