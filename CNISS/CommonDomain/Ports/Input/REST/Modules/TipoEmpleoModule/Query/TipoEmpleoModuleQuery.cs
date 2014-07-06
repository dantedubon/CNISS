using CNISS.EnterpriseDomain.Domain.Repositories;
using Nancy;

namespace CNISS.CommonDomain.Ports.Input.REST.Modules.TipoEmpleoModule
{
    public class TipoEmpleoModuleQuery:NancyModule
    {
        public TipoEmpleoModuleQuery(ITipoDeEmpleoReadOnlyRepository readOnlyRepository)
        {
            Get["/enterprise/empleos/tipos"] = parameters => Response.AsJson(readOnlyRepository.getAll());
        }
    }
}