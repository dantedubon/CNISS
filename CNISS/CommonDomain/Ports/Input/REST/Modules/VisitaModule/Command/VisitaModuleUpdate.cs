using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Application;
using CNISS.CommonDomain.Ports.Input.REST.Request.VisitaRequest;
using CNISS.EnterpriseDomain.Domain.Entities;
using Nancy;
using Nancy.ModelBinding;

namespace CNISS.CommonDomain.Ports.Input.REST.Modules.VisitaModule.Command
{
    public class VisitaModuleUpdate:NancyModule
    {
        public VisitaModuleUpdate(ICommandUpdateIdentity<Visita> commandUpdate )
        {
            Put["/visita"] = parameters =>
            {
                var visitaRequest = this.Bind<VisitaRequest>();
                if (visitaRequest.isValidPut())
                {
                    var visitaMapping = new VisitaMapping();
                    var visita = visitaMapping.getVisitaForPut(visitaRequest);
                    if (commandUpdate.isExecutable(visita))
                    {
                        commandUpdate.execute(visita);
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