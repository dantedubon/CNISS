using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Application;
using CNISS.CommonDomain.Ports.Input.REST.Request.EmpresaRequest;
using CNISS.EnterpriseDomain.Domain.Entities;
using Nancy;
using Nancy.ModelBinding;

namespace CNISS.CommonDomain.Ports.Input.REST.Modules.EmpresaModule.Commands
{
    public class EmpresaModuleUpdate:NancyModule
    {
        public EmpresaModuleUpdate(ICommandUpdateIdentity<Empresa> _commandUpdate, IFileGetter fileGetter)
        {
            Put["enterprise/"] = parameters =>
            {
                  var request = this.Bind<EmpresaRequest>();
                if (request.isValidPost())
                {
                    var empresaMap = new EmpresaMap();
                    var empresa = empresaMap.getEmpresa(request);
                    if (_commandUpdate.isExecutable(empresa))
                    {
                      
                            _commandUpdate.execute(empresa);
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