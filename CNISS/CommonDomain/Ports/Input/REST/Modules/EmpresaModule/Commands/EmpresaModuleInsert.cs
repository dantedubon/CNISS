using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.AutenticationDomain.Domain.Entities;
using CNISS.AutenticationDomain.Domain.ValueObjects;
using CNISS.CommonDomain.Application;
using CNISS.CommonDomain.Ports.Input.REST.Modules.GremioModule.GremioCommand;
using CNISS.CommonDomain.Ports.Input.REST.Request.EmpresaRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.GremioRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.RolModule;
using CNISS.CommonDomain.Ports.Input.REST.Request.UserRequest;
using CNISS.EnterpriseDomain.Domain;
using CNISS.EnterpriseDomain.Domain.Entities;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using Nancy;
using Nancy.ModelBinding;

namespace CNISS.CommonDomain.Ports.Input.REST.Modules.EmpresaModule.Commands
{
    public class EmpresaModuleInsert:NancyModule
    {
     
        public EmpresaModuleInsert(ICommandInsertIdentity<Empresa> _commandInsert, IFileGetter fileGetter)
        {
            Post["enterprise/"] = parameters =>
            {
                var request = this.Bind<EmpresaRequest>();
                if (request.isValidPost())
                {
                    var directory = @"/EmpresasContratos";
                    var extension = ".pdf";
                    var nameFile = request.contentFile;
                    if (fileGetter.existsFile(directory, nameFile, extension))
                    {
                        var dataFile = fileGetter.getFile(directory, nameFile, extension);
                        var empresaMap = new EmpresaMap();
                        var empresa = empresaMap.getEmpresa(request, dataFile);
                        if (_commandInsert.isExecutable(empresa))
                        {
                            _commandInsert.execute(empresa);
                            return new Response()
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