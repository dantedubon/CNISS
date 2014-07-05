using CNISS.EnterpriseDomain.Domain.Repositories;
using Nancy;

namespace CNISS.CommonDomain.Ports.Input.REST.Modules.ActividadEconomicaModule.Query
{
    public class ActividadesEconomicasModuleQuery:NancyModule
    {
        public ActividadesEconomicasModuleQuery(IActividadEconomicaRepositoryReadOnly repository)
        {
            Get["/empresa/actividades"] = parameters => Response.AsJson(repository.getAll());
        }
    }
}