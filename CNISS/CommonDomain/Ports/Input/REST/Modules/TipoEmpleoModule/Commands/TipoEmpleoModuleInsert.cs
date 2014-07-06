using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Application;
using CNISS.CommonDomain.Ports.Input.REST.Request.TipoEmpleoRequest;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using Nancy;
using Nancy.ModelBinding;

namespace CNISS.CommonDomain.Ports.Input.REST.Modules.TipoEmpleoModule.Commands
{
    public class TipoEmpleoModuleInsert:NancyModule
    {
        private readonly TipoEmpleoMapping _tipoEmpleoMapping;

        public TipoEmpleoModuleInsert(ICommandInsertIdentity<TipoEmpleo> command )
        {
            _tipoEmpleoMapping = new TipoEmpleoMapping();
            Post["/enterprise/tipoEmpleo"] = parameters =>
            {
                var request = this.Bind<TipoEmpleoRequest>();
                if (request.isValidPost())
                {
                    var tipoEmpleo = _tipoEmpleoMapping.getTipoEmpleoForPost(request);
                    command.execute(tipoEmpleo);

                    return new Response()
                   .WithStatusCode(HttpStatusCode.OK);
                }
                return new Response()
                    .WithStatusCode(HttpStatusCode.BadRequest);
            };
        }
    }
}