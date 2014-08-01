using System;
using System.Linq;
using CNISS.CommonDomain.Ports.Input.REST.Request;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Security;

namespace CNISS.CommonDomain.Ports.Input.REST.Modules.EnterpriseServiceModule
{
    public class FilesPostModule : NancyModule
    {
        public FilesPostModule(IFilePersister filePersister)
        {
            Post["/Empleo/ComprobantesPago"] = parameters =>
            {
                var file = Request.Files.FirstOrDefault();

                return FileProcessor(filePersister, file, @"/EmpleoComprobantesPago",".pdf");
            };

            Post["/Empresa/Contrato"] = parameters =>
            {

                var file = Request.Files.FirstOrDefault();

                return FileProcessor(filePersister, file, @"/EmpresasContratos", ".pdf");

                
            };

            Post["/Empleo/Contrato"] = parameters =>
            {

                var file = Request.Files.FirstOrDefault();
                return FileProcessor(filePersister, file, @"/EmpleoContratos", ".pdf");

            };
            
        }

        private dynamic FileProcessor(IFilePersister filePersister, HttpFile file, string directory, string extension)
        {
            if (file != null)
            {
                Guid fileID = Guid.NewGuid();
                

                filePersister.saveFile(directory, file, extension, fileID.ToString());
                return Response.AsJson(fileID).WithStatusCode(HttpStatusCode.OK);
            }

            return HttpStatusCode.NotAcceptable;
        }
    }
}