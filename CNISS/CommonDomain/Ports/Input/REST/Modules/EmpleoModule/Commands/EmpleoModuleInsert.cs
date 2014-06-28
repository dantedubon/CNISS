using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Application;
using CNISS.CommonDomain.Ports.Input.REST.Request.BeneficiarioRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.EmpleoRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.EmpresaRequest;
using CNISS.EnterpriseDomain.Domain;
using CNISS.EnterpriseDomain.Domain.Entities;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using Nancy;
using Nancy.ModelBinding;
using NHibernate.Linq;
using NHibernate.Param;

namespace CNISS.CommonDomain.Ports.Input.REST.Modules.EmpleoModule.Commands
{
    public class EmpleoModuleInsert:NancyModule
    {
        private readonly EmpleoMapping _empleoMapping;

        public EmpleoModuleInsert(ICommandInsertIdentity<Empleo> command )
        {
            _empleoMapping = new EmpleoMapping();
            Post["/enterprise/empleos"] = parameters =>
            {
                var request = this.Bind<EmpleoRequest>();
                if (request.isValidPost())
                {
                    var empleo = _empleoMapping.getEmpleo(request);
                    if (command.isExecutable(empleo))
                    {
                        command.execute(empleo);
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