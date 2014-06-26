using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;

namespace CNISS.CommonDomain.Ports.Input.REST.Modules.EmpleoModule
{
    public class EmpleoComprobantesPagoModule : NancyModule
    {
        public EmpleoComprobantesPagoModule(IFilePersister filePersister)
        {
            Post["/Empleo/ComprobantesPago"] = parameters =>
            {

                var file = Request.Files.FirstOrDefault();

                if (file != null)
                {
                    Guid fileID = Guid.NewGuid();

                    filePersister.saveFile(@"/EmpleoComprobantesPago", file, ".pdf", fileID.ToString());
                    return Response.AsJson(fileID).WithStatusCode(HttpStatusCode.OK);
                }

                return HttpStatusCode.NotAcceptable;
            };
        }
    }
}