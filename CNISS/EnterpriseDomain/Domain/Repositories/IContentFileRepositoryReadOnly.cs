using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Domain;
using CNISS.EnterpriseDomain.Domain.ValueObjects;

namespace CNISS.EnterpriseDomain.Domain.Repositories
{
    public interface IContentFileRepositoryReadOnly:IRepositoryReadOnly<ContentFile,Guid>
    {
    }
}