using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Application;
using CNISS.CommonDomain.Ports.Input.REST.Request.EmpresaRequest;
using CNISS.EnterpriseDomain.Application;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using Nancy;
using Nancy.ModelBinding;

namespace CNISS.CommonDomain.Ports.Input.REST.Modules.ActividadEconomicaModule.Commands
{
    public class ActividadEconomicaModuleUpdate:NancyModule
    {
        public ActividadEconomicaModuleUpdate(ICommandUpdateIdentity<ActividadEconomica> command)
        {

            Put["enterprise/actividades"] = parameters =>
            {
                var request = this.Bind<ActividadEconomicaRequest>();
                if (request.isValidPut())
                {
                    var actividadMap = new ActividadEconomicaMapping();
                    var actividad = actividadMap.getActividadEconomicaForPut(request);
                    if (command.isExecutable(actividad))
                    {
                        command.execute(actividad);
                        return new Response()
                  .WithStatusCode(HttpStatusCode.OK);
                    }
                   
                }
                return new Response()
                   .WithStatusCode(HttpStatusCode.BadRequest);
            };
        }
    }
}