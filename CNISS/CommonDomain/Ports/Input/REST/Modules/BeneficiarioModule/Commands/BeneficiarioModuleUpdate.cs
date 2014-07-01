using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Application;
using CNISS.CommonDomain.Ports.Input.REST.Request.BeneficiarioRequest;
using CNISS.EnterpriseDomain.Domain.Entities;
using Nancy;
using Nancy.ModelBinding;

namespace CNISS.CommonDomain.Ports.Input.REST.Modules.BeneficiarioModule.Commands
{
    public class BeneficiarioModuleUpdate:NancyModule
    {
        public BeneficiarioModuleUpdate(ICommandUpdateIdentity<Beneficiario> commandUpdate )
        {
            Put["/enterprise/beneficiarios"] = parameters =>
            {
                 var request = this.Bind<BeneficiarioRequest>();

                if (request.isValidPost())
                {
                      var beneficiarioMap = new BeneficiarioMap();
                    var beneficiario = beneficiarioMap.getBeneficiario(request);
                    if (commandUpdate.isExecutable(beneficiario))
                    {
                        commandUpdate.execute(beneficiario);
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