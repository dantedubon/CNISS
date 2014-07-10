using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Ports.Input.REST.Modules.UserModule.UserQuery;
using CNISS.EnterpriseDomain.Domain.Repositories;
using Nancy;

namespace CNISS.CommonDomain.Ports.Input.REST.Modules.VisitaModule.Query
{
    public class SupervisorAvailableModuleQuery: NancyModule
    {
        public SupervisorAvailableModuleQuery(IVisitaRepositoryReadOnly repositoryRead)
        {
            Get["/visita/supervisores/available/{fechaInicial:datetime(yyyy-MM-dd)}/{fechaFinal:datetime(yyyy-MM-dd)}"] =
                parameters =>

                {
                    DateTime fechaInicial = parameters.fechaInicial;
                    DateTime fechaFinal = parameters.fechaFinal;
                    if (fechaInicial < fechaFinal)
                    {
                        var userMapping = new UserMapping();
                        var usersAvailable = repositoryRead.usuariosSinVisitaAgendada(fechaInicial, fechaFinal);
                        return Response.AsJson(userMapping.convertToRequest(usersAvailable))
                            .WithStatusCode(HttpStatusCode.OK);
                    }
                    return new Response()
                        .WithStatusCode(HttpStatusCode.BadRequest);
                };
        }
    }
}