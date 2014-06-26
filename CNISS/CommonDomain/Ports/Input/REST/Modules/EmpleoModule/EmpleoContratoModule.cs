using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;

namespace CNISS.CommonDomain.Ports.Input.REST.Modules.EmpleoModule
{
    public class EmpleoContratoModule : NancyModule
    {
        public EmpleoContratoModule(IFilePersister filePersister)
        {
            Post["/Empleo/Contrato"] = parameters =>
            {

                var file = Request.Files.FirstOrDefault();

                if (file != null)
                {
                    Guid fileID = Guid.NewGuid();

                    filePersister.saveFile(@"/EmpleoContratos", file, ".pdf", fileID.ToString());
                    return Response.AsJson(fileID).WithStatusCode(HttpStatusCode.OK);
                }

                return HttpStatusCode.NotAcceptable;
            };
        }
    }
}