using CNISS.EnterpriseDomain.Domain.Repositories;
using Nancy;

namespace CNISS.CommonDomain.Ports.Input.REST.Modules.EnterpriseServiceModule
{
    public class DepartamentoModuleQuery:NancyModule
    {
        public DepartamentoModuleQuery(IDepartamentRepositoryReadOnly repository)
        {
            Get["enterprise/departaments"] = parameters =>
            {
                var result = repository.getAll();
                return Response.AsJson(result)
                    .WithStatusCode(HttpStatusCode.OK);

            };
        }
    }
}