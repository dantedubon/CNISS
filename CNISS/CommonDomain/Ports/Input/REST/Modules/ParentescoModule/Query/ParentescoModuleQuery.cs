using CNISS.EnterpriseDomain.Domain.Repositories;
using Nancy;

namespace CNISS.CommonDomain.Ports.Input.REST.Modules.ParentescoModule.Query
{
    public class ParentescoModuleQuery:NancyModule
    {
        public ParentescoModuleQuery(IParentescoReadOnlyRepository repository)
        {

            Get["/enterprise/beneficiarios/parentescos"] = parameters => Response.AsJson(repository.getAll());
        }
    }
}