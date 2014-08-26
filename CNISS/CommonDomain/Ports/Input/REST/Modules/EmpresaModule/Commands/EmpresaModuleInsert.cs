using CNISS.CommonDomain.Application;
using CNISS.CommonDomain.Ports.Input.REST.Request.EmpresaRequest;
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
                   
                       
                    var empresaMap = new EmpresaMap();
                    var file = request.contentFile;
    

                    var empresa = empresaMap.getEmpresa(request);
                    if (_commandInsert.isExecutable(empresa))
                    {
                        if (!string.IsNullOrEmpty(file))
                        {
                            if (!fileGetter.existsFile(@"/EmpresasContratos", file, ".pdf"))
                            {
                                return new Response()
                                        .WithStatusCode(HttpStatusCode.BadRequest);
                            }
                            var fileContrato = fileGetter.getFile(@"/EmpresasContratos", file, ".pdf");
                            empresa.contrato = new ContentFile(fileContrato);
                        }


                        _commandInsert.execute(empresa);


                        fileGetter.deleteFile(@"/EmpresasContratos", file, ".pdf");
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