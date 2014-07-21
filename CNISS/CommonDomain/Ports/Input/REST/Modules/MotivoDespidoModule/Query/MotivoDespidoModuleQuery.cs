using CNISS.EnterpriseDomain.Domain.Repositories;
using Nancy;
using Nancy.Security;

namespace CNISS.CommonDomain.Ports.Input.REST.Modules.MotivoDespidoModule.Query
{
    public class MotivoDespidoModuleQuery:NancyModule
    {
        public MotivoDespidoModuleQuery(IMotivoDespidoRepositoryReadOnly readOnlyRepository)
        {
            Get["/enterprise/motivoDespido"] = parameters => Response.AsJson(readOnlyRepository.getAll());

            Get["/movil/motivosDespido"] = parameters =>
            {
                this.RequiresClaims(new[] {"movil"});
                return Response.AsJson(readOnlyRepository.getAll());
            };
        }
    }
}