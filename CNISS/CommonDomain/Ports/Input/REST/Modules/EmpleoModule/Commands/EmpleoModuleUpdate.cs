using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Application;
using CNISS.CommonDomain.Ports.Input.REST.Request.EmpleoRequest;
using CNISS.EnterpriseDomain.Domain.Entities;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using Nancy;
using Nancy.ModelBinding;

namespace CNISS.CommonDomain.Ports.Input.REST.Modules.EmpleoModule.Commands
{
    public class EmpleoModuleUpdate:NancyModule
    {
        private readonly IFileGetter _fileGetter;
        private readonly EmpleoMapping _empleoMapping;
 

        public EmpleoModuleUpdate(ICommandUpdateIdentity<Empleo> commandUpdate,IFileGetter fileGetter )
        {
            _fileGetter = fileGetter;
            _empleoMapping = new EmpleoMapping();
            Put["/enterprise/empleos"] = parameters =>
            {
                var request = this.Bind<EmpleoRequest>();
                if (request.isValidPut())
                {

          
                /*    if (request.updateContrato)
                    {
                        var contrato = request.contrato;
                        if (!_fileGetter.existsFile(DirectorioArchivosContratos, contrato, Extension))
                        {
                            return new Response()
                                    .WithStatusCode(HttpStatusCode.BadRequest);
                        }
                        request.contentFile = fileGetter.getFile(DirectorioArchivosContratos, contrato, Extension);
                        
                    }
                    else
                    {
                        var contrato = request.contrato;
                        Guid idContrato;
                        if (!Guid.TryParse(contrato, out idContrato)&&!string.IsNullOrEmpty(contrato))
                        {
                            return new Response()
                               .WithStatusCode(HttpStatusCode.BadRequest);
                        }

                    }*/
                    var empleo = _empleoMapping.getEmpleoForPut(request);
                  
                    if (commandUpdate.isExecutable(empleo))
                    {
                        commandUpdate.execute(empleo);
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