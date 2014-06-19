using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Application;
using CNISS.CommonDomain.Ports.Input.REST.Request.GremioRequest;
using CNISS.EnterpriseDomain.Application;
using CNISS.EnterpriseDomain.Domain.Entities;
using Nancy;
using Nancy.ModelBinding;

namespace CNISS.CommonDomain.Ports.Input.REST.Modules.GremioModule.GremioCommand
{
    public class GremioModuleUpdateRepresentante:NancyModule
    {
        public GremioModuleUpdateRepresentante(ICommandUpdateGremioRepresentante commandUpdate)
        {
            var  _gremioMap = new GremioMap();
            Put["enterprise/gremio/representante"] = parameters =>
            {
                var request = this.Bind<GremioRequest>();
                if (request.isValidPutRepresentante())
                {
                    var gremio = _gremioMap.getGremioForPost(request);

                    if (commandUpdate.isExecutable(gremio))
                    {
                        commandUpdate.execute(gremio);
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