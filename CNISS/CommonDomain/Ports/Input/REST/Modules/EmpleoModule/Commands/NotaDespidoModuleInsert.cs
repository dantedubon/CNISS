using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.AutenticationDomain.Domain.Entities;
using CNISS.AutenticationDomain.Domain.Services;
using CNISS.AutenticationDomain.Domain.ValueObjects;
using CNISS.CommonDomain.Domain;
using CNISS.CommonDomain.Ports.Input.REST.Request;
using CNISS.CommonDomain.Ports.Input.REST.Request.EmpleoRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.EmpresaRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.MotivoDespidoRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.VisitaRequest;
using CNISS.EnterpriseDomain.Application;
using CNISS.EnterpriseDomain.Domain.Entities;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using Nancy;
using Nancy.Authentication.Token;
using Nancy.ModelBinding;
using Nancy.Security;

namespace CNISS.CommonDomain.Ports.Input.REST.Modules.EmpleoModule.Commands
{
    public class NotaDespidoModuleInsert:NancyModule
    {
        private const string directorioImagenes = @"/ImagenesMoviles";
        private const string extensionImagenes = ".jpeg";

        public NotaDespidoModuleInsert(ISerializeJsonRequest serializerJson,Func<string,IEncrytRequestProvider> encryptRequestProvider, 
            ITokenizer tokenizer,ICommandInsertNotaDespido command, IFileGetter fileGetter)
        {
            Post["/movil/notaDespido"] = parameters =>
            {
               

                var movilRequest = this.Bind<MovilRequest>();
                var userId = tokenizer.Detokenize(movilRequest.token, Context);
                if (userId == null)
                {
                    return new Response().WithStatusCode(HttpStatusCode.Unauthorized);
                }

                string notaDespidoString;
                NotaDespidoRequest notaDespidoRequest;
                try
                {
                    var desencrypter = encryptRequestProvider(movilRequest.token);
                    notaDespidoString = desencrypter.decryptString(movilRequest.data);
                    notaDespidoRequest = serializerJson.fromJson<NotaDespidoRequest>(notaDespidoString);
                }
                catch (Exception e)
                {
                    return new Response().WithStatusCode(HttpStatusCode.BadRequest);
                }



                
                if (notaDespidoRequest.isValidPost())
                {
                    var archivoNotaDespido = notaDespidoRequest.imagenNotaDespido.ToString();
                    if (fileGetter.existsFile(directorioImagenes, archivoNotaDespido, extensionImagenes))
                    {
                        var notaDespido = getNotaDespido(notaDespidoRequest);
                        if (command.isExecutable(notaDespidoRequest.empleoId, notaDespido))
                        {
                            var dataImage = fileGetter.getFile(directorioImagenes, archivoNotaDespido, extensionImagenes);
                            var imageFile = new ContentFile(dataImage);
                            notaDespido.DocumentoDespido = imageFile;
                            command.execute(notaDespidoRequest.empleoId,notaDespido);
                            fileGetter.deleteFile(directorioImagenes, archivoNotaDespido, extensionImagenes);
                            return new Response()
     .WithStatusCode(HttpStatusCode.OK);
                        }
               
                    }

                    
                }
                return new Response()
                    .WithStatusCode(HttpStatusCode.BadRequest);
            };
        }

        private NotaDespido getNotaDespido(NotaDespidoRequest notaDespidoRequest)
        {
            var motivoDespido = getMotivoDespido(notaDespidoRequest.motivoDespidoRequest);
            var supervisor = getSupervisor(notaDespidoRequest.supervisorRequest);
            var firma = getFirmaAutorizada(notaDespidoRequest.firmaAutorizadaRequest);

            var notaDespido = new NotaDespido(motivoDespido, notaDespidoRequest.fechaDespido,
                notaDespidoRequest.posicionGPS, supervisor, firma);

            var auditoriaRequest = notaDespidoRequest.auditoriaRequest;
            notaDespido.Auditoria = new Auditoria(auditoriaRequest.usuarioCreo, auditoriaRequest.fechaCreo,
                auditoriaRequest.usuarioModifico, auditoriaRequest.fechaModifico); ;
            return notaDespido;
        }

        private MotivoDespido getMotivoDespido(MotivoDespidoRequest motivoDespidoRequest)
        {
            return new MotivoDespido(motivoDespidoRequest.descripcion){Id = motivoDespidoRequest.IdGuid};
        }

        private FirmaAutorizada getFirmaAutorizada(FirmaAutorizadaRequest firmaAutorizadaRequest)
        {
            var userRequest = firmaAutorizadaRequest.userRequest;
            var user = new User(userRequest.Id, "", "", userRequest.password, "", new RolNull());
            var firma = new FirmaAutorizada(user, firmaAutorizadaRequest.fechaCreacion);
            firma.Id = firmaAutorizadaRequest.IdGuid;

            return firma;
        }

        private Supervisor getSupervisor(SupervisorRequest supervisorRequest)
        {
            var userRequest = supervisorRequest.userRequest;
            var user = new User(userRequest.Id, "", "", userRequest.password, "", new RolNull());
            var supervisor = new Supervisor(user);
            supervisor.Id = supervisorRequest.guid;
            return supervisor;
        }
    }
}