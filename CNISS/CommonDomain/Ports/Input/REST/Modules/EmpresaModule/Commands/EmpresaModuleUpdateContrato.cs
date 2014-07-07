using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.EnterpriseDomain.Application;
using CNISS.EnterpriseDomain.Domain;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using Nancy;

namespace CNISS.CommonDomain.Ports.Input.REST.Modules.EmpresaModule.Commands
{
    public class EmpresaModuleUpdateContrato:NancyModule
    {
        private const string DirectorioContratoEmpresas = @"/EmpresasContratos";
        private const string Extension = ".pdf";

        public EmpresaModuleUpdateContrato(ICommandUpdateEmpresaContrato command, IFileGetter fileGetter)
        {
            Put["/enterprise/{rtn}/contract/{contract:guid}"] = parameters =>
            {
                var rtn = parameters.rtn;
                var contractId = parameters.contract;

                if (fileGetter.existsFile(DirectorioContratoEmpresas, contractId.ToString(), Extension))
                {
                    var rtnEmpresa = new RTN(rtn);
                    if (rtnEmpresa.isRTNValid())
                    {
                        if (command.isExecutable(rtnEmpresa))
                        {
                            var data = fileGetter.getFile(DirectorioContratoEmpresas, contractId, Extension);
                            var contentFile = new ContentFile(data);
                            command.execute(rtnEmpresa,contentFile);

                            return  Response.AsJson(contentFile.Id)
                  .WithStatusCode(HttpStatusCode.OK);
                        }
                        
                    }

                   
                }

                return new Response()
                    .WithStatusCode(HttpStatusCode.BadRequest);
            };
        }
    }
}