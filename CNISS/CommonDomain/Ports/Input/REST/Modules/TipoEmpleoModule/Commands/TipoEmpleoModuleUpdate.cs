using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Application;
using CNISS.CommonDomain.Ports.Input.REST.Request.TipoEmpleoRequest;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using Nancy;
using Nancy.ModelBinding;
using NHibernate.Linq;

namespace CNISS.CommonDomain.Ports.Input.REST.Modules.TipoEmpleoModule.Commands
{
    public class TipoEmpleoModuleUpdate:NancyModule
    {
        public TipoEmpleoModuleUpdate(ICommandUpdateIdentity<TipoEmpleo> command )
        {
            Put["/enterprise/tipoEmpleo"] = parameters =>
            {
                var request = this.Bind<TipoEmpleoRequest>();

                if (request.isValidPut())
                {
                    var mapTipoEmpleo = new TipoEmpleoMapping();
                    var tipoEmpleo = mapTipoEmpleo.getTipoEmpleoForPut(request);
                    if (command.isExecutable(tipoEmpleo))
                    { 
                        command.execute(tipoEmpleo);
                      
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