using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Ports.Input.REST.Request.GremioRequest;
using Nancy;
using Nancy.ModelBinding;

namespace CNISS.CommonDomain.Ports.Input.REST.Modules.GremioModule.GremioCommand
{
    public class GremioModuleInsert:NancyModule
    {
        public GremioModuleInsert()
        {
            Post["enterprise/gremio"] = paramaters =>
            {
                var request = this.Bind<GremioRequest>();
                return new Response()
                    .WithStatusCode(HttpStatusCode.OK);
            };

        }
    }
}