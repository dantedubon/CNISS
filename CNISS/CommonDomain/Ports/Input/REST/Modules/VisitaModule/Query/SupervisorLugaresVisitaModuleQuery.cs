using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.EnterpriseDomain.Domain.Repositories;
using Nancy;
using Nancy.Security;

namespace CNISS.CommonDomain.Ports.Input.REST.Modules.VisitaModule.Query
{
    public class SupervisorLugaresVisitaModuleQuery:NancyModule
    {
        public SupervisorLugaresVisitaModuleQuery(IVisitaRepositoryReadOnly repository)
        {
            Get["/movil/supervisor/lugaresVisita"] = _ =>
            {
               this.RequiresClaims(new[] { "movil" });
               return Response.AsJson("Autenticado");
               
            };
        }
    }
}