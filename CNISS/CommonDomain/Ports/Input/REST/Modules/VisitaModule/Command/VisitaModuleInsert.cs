using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.AutenticationDomain.Domain.Entities;
using CNISS.AutenticationDomain.Domain.ValueObjects;
using CNISS.CommonDomain.Application;
using CNISS.CommonDomain.Domain;
using CNISS.CommonDomain.Ports.Input.REST.Request.EmpresaRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.VisitaRequest;
using CNISS.EnterpriseDomain.Domain;
using CNISS.EnterpriseDomain.Domain.Entities;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using Nancy;
using Nancy.ModelBinding;

namespace CNISS.CommonDomain.Ports.Input.REST.Modules.VisitaModule.Command
{
    public class VisitaModuleInsert:NancyModule
    {
        private readonly VisitaMapping _visitaMapping;

        public VisitaModuleInsert(ICommandInsertIdentity<Visita> command)
        {
            _visitaMapping = new VisitaMapping();
            Post["/visita"] = parameters =>
            {
                var visitaRequest = this.Bind<VisitaRequest>();
                if (visitaRequest.isValidPost())
                {
                    var visita = _visitaMapping.getVisita(visitaRequest);
                    command.execute(visita);
                    return new Response()
                   .WithStatusCode(HttpStatusCode.OK);
                }
                return new Response()
                    .WithStatusCode(HttpStatusCode.BadRequest);
            };
        }
    }
}