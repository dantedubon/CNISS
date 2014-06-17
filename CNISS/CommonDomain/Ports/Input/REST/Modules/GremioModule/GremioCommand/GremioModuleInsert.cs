using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Application;
using CNISS.CommonDomain.Ports.Input.REST.Request.GremioRequest;
using CNISS.EnterpriseDomain.Domain;
using CNISS.EnterpriseDomain.Domain.Entities;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
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
                        var gremio = _gremioMap.getGremio(request);
                        commandInsert.execute(gremio);
                        return new Response()
                            .WithStatusCode(HttpStatusCode.OK);
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