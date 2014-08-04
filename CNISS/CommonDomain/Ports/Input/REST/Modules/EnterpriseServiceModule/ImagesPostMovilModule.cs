using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Ports.Input.REST.Modules.UserModule;
using CNISS.CommonDomain.Ports.Input.REST.Request;
using Nancy;
using Nancy.Authentication.Token;
using Nancy.ModelBinding;

namespace CNISS.CommonDomain.Ports.Input.REST.Modules.EnterpriseServiceModule
{
    public class ImagesPostMovilModule:NancyModule
    {
        public ImagesPostMovilModule(ITokenizer tokenizer, IFilePersister filePersister)
        {
            Post["/movil/imagenes"] = parameters =>
            {

                var movilRequest = this.Bind<MovilRequest>();
                try
                {
                    var userId = tokenizer.Detokenize(movilRequest.token, Context);
                    if (userId == null)
                    {
                        return new Response().WithStatusCode(HttpStatusCode.Unauthorized);
                    }
                }
                catch (Exception e)
                {
                    return new Response().WithStatusCode(HttpStatusCode.Unauthorized);
                }

                
                var file = Request.Files.FirstOrDefault();
                return FileProcessor(filePersister, file, @"/ImagenesMoviles", ".jpeg");

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