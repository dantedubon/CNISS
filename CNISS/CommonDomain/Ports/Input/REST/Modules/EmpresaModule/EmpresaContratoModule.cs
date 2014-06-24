using System;
using System.Linq;
using Nancy;

namespace CNISS.CommonDomain.Ports.Input.REST.Modules
{
    public class EmpresaContratoModule:NancyModule
    {
        public EmpresaContratoModule(IFilePersister filePersister)
        {
            Post["/Empresa/Contrato"] = parameters =>
            {
                
                var file = Request.Files.FirstOrDefault();
                
                if (file != null)
                {
                    Guid fileID = Guid.NewGuid();

                    filePersister.saveFile(@"/EmpresasContratos",file,".pdf",fileID.ToString());
                    return Response.AsJson(fileID).WithStatusCode(HttpStatusCode.OK);
                }

                return HttpStatusCode.NotAcceptable;
            };
        }
    }
}