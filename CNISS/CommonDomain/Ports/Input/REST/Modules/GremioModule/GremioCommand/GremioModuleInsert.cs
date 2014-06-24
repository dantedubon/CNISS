using System;
using CNISS.CommonDomain.Application;
using CNISS.CommonDomain.Ports.Input.REST.Request.GremioRequest;
using CNISS.EnterpriseDomain.Domain.Entities;
using Nancy;
using Nancy.ModelBinding;

namespace CNISS.CommonDomain.Ports.Input.REST.Modules.GremioModule.GremioCommand
{
    public class GremioModuleInsert:NancyModule
    {
        private readonly GremioMap _gremioMap;

        public GremioModuleInsert(ICommandInsertIdentity<Gremio> commandInsert )
        {
            _gremioMap = new GremioMap();
            Post["enterprise/gremio"] = paramaters =>
            {

                var request = this.Bind<GremioRequest>();
                try
                {
                    if (request.isValidPost())
                    {
                        var gremio = _gremioMap.getGremioForPost(request);
                        if (commandInsert.isExecutable(gremio))
                        {
                            commandInsert.execute(gremio);
                            return new Response()
                                .WithStatusCode(HttpStatusCode.OK);
                        }
                       
                    }
                }
                catch (ArgumentException e)
                {
                    return new Response()
                   .WithStatusCode(HttpStatusCode.BadRequest);
                }

                return new Response()
                    .WithStatusCode(HttpStatusCode.BadRequest);
            };
        }
    }
}