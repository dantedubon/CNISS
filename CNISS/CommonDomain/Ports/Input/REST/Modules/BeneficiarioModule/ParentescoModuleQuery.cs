using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.EnterpriseDomain.Domain.Repositories;
using Nancy;

namespace CNISS.CommonDomain.Ports.Input.REST.Modules.BeneficiarioModule
{
    public class ParentescoModuleQuery:NancyModule
    {
        public ParentescoModuleQuery(IParentescoReadOnlyRepository repository)
        {

            Get["/enterprise/beneficiarios/parentescos"] = parameters => Response.AsJson(repository.getAll());
        }
    }
}