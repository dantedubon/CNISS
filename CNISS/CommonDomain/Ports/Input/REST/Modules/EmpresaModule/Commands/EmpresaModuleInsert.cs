using CNISS.CommonDomain.Application;
using CNISS.CommonDomain.Ports.Input.REST.Request.EmpresaRequest;
using CNISS.EnterpriseDomain.Domain.Entities;
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
                   
                       
                    var empresaMap = new EmpresaMap();
                    var file = request.contentFile;
                    var fileContrato = new byte[] {0};

                    if (!string.IsNullOrEmpty(file))
                    {
                        if (!fileGetter.existsFile(@"/EmpresasContratos", file, ".pdf"))
                        {
                            return new Response()
                                    .WithStatusCode(HttpStatusCode.BadRequest);
                        }
                        fileContrato = fileGetter.getFile(@"/EmpresasContratos", file, ".pdf");
                    }


                    var empresa = empresaMap.getEmpresa(request, fileContrato);
                    if (_commandInsert.isExecutable(empresa))
                    {
                        _commandInsert.execute(empresa);
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