using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.EnterpriseDomain.Application;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using Nancy;

namespace CNISS.CommonDomain.Ports.Input.REST.Modules.EmpleoModule.Commands
{
    public class EmpleoModuleUpdateImagenComprobantePago:NancyModule
    {
        private const string DirectorioArchivosVouchers = @"/EmpleoComprobantesPago";
       
        private const string Extension = ".pdf";

        public EmpleoModuleUpdateImagenComprobantePago(ICommandUpdateEmpleoImagenComprobantePago command, IFileGetter fileGetter)
        {
            Put["/enterprise/empleos/{id:guid}/Comprobante/{comprobante:guid}/Imagen/{imagen:guid}"] = parameters =>
            {
                var empleoId = parameters.id;
                var comprobanteId = parameters.comprobante;
                var imagenId = parameters.imagen;

                if (fileGetter.existsFile(DirectorioArchivosVouchers, imagenId.ToString(), Extension))
                {
                    if (command.isExecutable(empleoId, comprobanteId))
                    {
                        var data = fileGetter.getFile(DirectorioArchivosVouchers, imagenId.ToString(), Extension);

                        var contentFile = new ContentFile(data);
                        command.execute(empleoId,comprobanteId,contentFile);
                        fileGetter.deleteFile(DirectorioArchivosVouchers, imagenId.ToString(), Extension);
                        return Response.AsJson(contentFile.Id)
                    .WithStatusCode(HttpStatusCode.OK);
                    }
                    
                }
                return new Response()
                    .WithStatusCode(HttpStatusCode.BadRequest);
                
            };
        }
       
    }
}