using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.EnterpriseDomain.Application;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using Nancy;

namespace CNISS.CommonDomain.Ports.Input.REST.Modules.EmpleoModule.Commands
{
    public class EmpleoModuleUpdateContrato:NancyModule
    {
        private const string DirectorioArchivosContratos = @"/EmpleoContratos";
        private const string Extension = ".pdf";

        public EmpleoModuleUpdateContrato(ICommandUpdateEmpleoContrato command, IFileGetter fileGetter)
        {
            Put["/enterprise/empleos/{id:guid}/contract/{contract:guid}"] = parameters =>
            {
                var empleoId = parameters.id;
                var contract = parameters.contract;

                if (fileGetter.existsFile(DirectorioArchivosContratos, contract.ToString(), Extension))
                {
                    if (command.isExecutable(empleoId))
                    {
                        var data = fileGetter.getFile(DirectorioArchivosContratos, contract.ToString(), Extension);
                        var contractFile = new ContentFile(data);
                        command.execute(empleoId,contractFile);
                        return Response.AsJson(contractFile.Id)
                  .WithStatusCode(HttpStatusCode.OK);
                    }
                   
                }
                return new Response()
                   .WithStatusCode(HttpStatusCode.BadRequest);
               
            };
        }
    }
}