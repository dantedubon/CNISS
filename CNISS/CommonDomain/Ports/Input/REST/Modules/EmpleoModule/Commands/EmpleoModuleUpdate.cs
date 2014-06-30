using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Application;
using CNISS.CommonDomain.Ports.Input.REST.Request.EmpleoRequest;
using CNISS.EnterpriseDomain.Domain.Entities;
using Nancy;
using Nancy.ModelBinding;

namespace CNISS.CommonDomain.Ports.Input.REST.Modules.EmpleoModule.Commands
{
    public class EmpleoModuleUpdate:NancyModule
    {
        private readonly EmpleoMapping _empleoMapping;

        public EmpleoModuleUpdate(ICommandUpdateIdentity<Empleo> commandUpdate )
        {
           _empleoMapping = new EmpleoMapping();
            Put["/enterprise/empleos"] = parameters =>
            {
                var request = this.Bind<EmpleoRequest>();
                if (request.isValidPut())
                {

                    var empleo = _empleoMapping.getEmpleo(request);
                    if (commandUpdate.isExecutable(empleo))
                    {
                        commandUpdate.execute(empleo);
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