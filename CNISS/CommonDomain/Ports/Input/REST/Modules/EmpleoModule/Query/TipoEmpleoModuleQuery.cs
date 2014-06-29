using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.EnterpriseDomain.Domain.Repositories;
using Nancy;

namespace CNISS.CommonDomain.Ports.Input.REST.Modules.EmpleoModule.Query
{
    public class TipoEmpleoModuleQuery:NancyModule
    {
        public TipoEmpleoModuleQuery(ITipoDeEmpleoReadOnlyRepository readOnlyRepository)
        {
            Get["/enterprise/empleos/tipos"] = parameters => Response.AsJson(readOnlyRepository.getAll());
        }
    }
}