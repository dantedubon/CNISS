using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Application;
using CNISS.CommonDomain.Ports.Input.REST.Request.BeneficiarioRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.GremioRequest;
using CNISS.EnterpriseDomain.Domain.Entities;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using Nancy;
using Nancy.ModelBinding;
using NHibernate.Linq;

namespace CNISS.CommonDomain.Ports.Input.REST.Modules.BeneficiarioModule.Commands
{
    public class BeneficiarioModuleInsert:NancyModule
    {
       

        public BeneficiarioModuleInsert(ICommandInsertIdentity<Beneficiario> command )
        {
            
            Post["/enterprise/beneficiarios"] = parameters =>
            {
                var request = this.Bind<BeneficiarioRequest>();

                if (request.isValidPost())
                {
                     var beneficiarioMap = new BeneficiarioMap();
                    var beneficiario = beneficiarioMap.getBeneficiario(request);
                    if (command.isExecutable(beneficiario))
                    {
                        command.execute(beneficiario);
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