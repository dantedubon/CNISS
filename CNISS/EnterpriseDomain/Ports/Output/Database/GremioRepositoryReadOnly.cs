using System.Linq;
using CNISS.CommonDomain.Ports.Output.Database;
using CNISS.EnterpriseDomain.Domain;
using CNISS.EnterpriseDomain.Domain.Entities;
using CNISS.EnterpriseDomain.Domain.Repositories;
using NHibernate;
using NHibernate.Hql.Ast.ANTLR;
using NHibernate.Linq;

namespace CNISS.EnterpriseDomain.Ports.Output.Database
{
    public class GremioRepositoryReadOnly:NHibernateReadOnlyRepository<Gremio,RTN>,IGremioRepositoryReadOnly
    {
        public GremioRepositoryReadOnly(ISession session) : base(session)
        {
        }

        public bool exists(RTN id)
        {
            return (from gremio in Session.Query<Gremio>() 
                    where gremio.Id == id
                    select gremio.Id).Any();
                
            

        }
    }

    public class EmpresaRespositoryReadOnly : NHibernateReadOnlyRepository<Empresa, RTN>, IEmpresaRepositoryReadOnly
    {
        public EmpresaRespositoryReadOnly(ISession session) : base(session)
        {
        }

        public bool exists(RTN id)
        {
            return (Session.Query<Empresa>().Where(empresa => empresa.Id == id).Select(empresa => empresa.Id)).Any();
        }
    }
}