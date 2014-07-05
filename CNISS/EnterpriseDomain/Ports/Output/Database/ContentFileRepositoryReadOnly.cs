using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Ports.Output.Database;
using CNISS.EnterpriseDomain.Domain.Repositories;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using NHibernate;
using NHibernate.Linq;

namespace CNISS.EnterpriseDomain.Ports.Output.Database
{
    public class ContentFileRepositoryReadOnly:NHibernateReadOnlyRepository<ContentFile,Guid>,IContentFileRepositoryReadOnly
    {
        public ContentFileRepositoryReadOnly(ISession session) : base(session)
        {
        }

        public  bool exists(Guid id)
        {
            return (Session.Query<ContentFile>().Where(x => x.Id == id).Select(x => x.Id).Any());
        }
    }
}