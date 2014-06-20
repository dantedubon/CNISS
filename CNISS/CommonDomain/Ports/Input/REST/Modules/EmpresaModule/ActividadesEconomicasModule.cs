using CNISS.EnterpriseDomain.Domain.Repositories;
using Nancy;

namespace CNISS.CommonDomain.Ports.Input.REST.Modules.EmpresaModule
{
    public class ActividadesEconomicasModule:NancyModule
    {
        public ActividadesEconomicasModule(IActividadEconomicaRepositoryReadOnly repository)
        {
            Get["/empresa/actividades"] = parameters => Response.AsJson(repository.getAll());
        }
    }
}