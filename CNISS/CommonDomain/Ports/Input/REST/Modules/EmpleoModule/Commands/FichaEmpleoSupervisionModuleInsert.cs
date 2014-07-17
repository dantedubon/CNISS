using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using CNISS.AutenticationDomain.Domain.Entities;
using CNISS.AutenticationDomain.Domain.ValueObjects;
using CNISS.CommonDomain.Application;
using CNISS.CommonDomain.Domain;
using CNISS.CommonDomain.Ports.Input.REST.Modules.BeneficiarioModule.Commands;
using CNISS.CommonDomain.Ports.Input.REST.Request.EmpleoRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.EmpresaRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.VisitaRequest;
using CNISS.EnterpriseDomain.Application;
using CNISS.EnterpriseDomain.Domain.Entities;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Security;

namespace CNISS.CommonDomain.Ports.Input.REST.Modules.EmpleoModule.Commands
{
    public class FichaEmpleoSupervisionModuleInsert:NancyModule
    {
        private const string directorioImagenes = @"/ImagenesMoviles";
        private const string extensionImagenes = ".jpeg";

        public FichaEmpleoSupervisionModuleInsert(ICommandInsertFichaDeSupervision command, IFileGetter fileGetter)
        {
            Post["/movil/fichaSupervision/"] = parameters =>
            {
                this.RequiresClaims(new[] { "movil" });
                var fichaRequest = this.Bind<FichaSupervisionEmpleoRequest>();
                if (fichaRequest.isValidPost())
                {
                    var archivoImagen = fichaRequest.fotografiaBeneficiario.ToString();
                    if (fileGetter.existsFile(directorioImagenes, archivoImagen, extensionImagenes))
                    {
                        var dataImage = fileGetter.getFile(directorioImagenes, archivoImagen, extensionImagenes);
                        var imageFile = new ContentFile(dataImage);
                        var beneficiario = new BeneficiarioMap().getBeneficiario(fichaRequest.beneficiarioRequest);
                        var ficha = getFichaSupervisionEmpleo(fichaRequest, imageFile);
                        if (command.isExecutable(ficha, beneficiario, fichaRequest.empleoId))
                        {
                            beneficiario.fotografiaBeneficiario = imageFile;
                            command.execute(ficha,beneficiario,fichaRequest.empleoId);
                            return new Response().WithStatusCode(HttpStatusCode.OK);

                        }
                        
                    }

                   
                }
                return new Response().WithStatusCode(HttpStatusCode.BadRequest);
                
            };
        }

        private  FichaSupervisionEmpleo getFichaSupervisionEmpleo(
            FichaSupervisionEmpleoRequest fichaSupervisionEmpleoRequest, ContentFile imagen)
        {
            var firma = getFirmaAutorizada(fichaSupervisionEmpleoRequest.firma);
            var supervisor = getSupervisor(fichaSupervisionEmpleoRequest.supervisor);


            var ficha = new FichaSupervisionEmpleo(supervisor, firma, fichaSupervisionEmpleoRequest.posicionGPS, fichaSupervisionEmpleoRequest.cargo,
                fichaSupervisionEmpleoRequest.funciones, fichaSupervisionEmpleoRequest.telefonoFijo, fichaSupervisionEmpleoRequest.telefonoCelular,
                fichaSupervisionEmpleoRequest.desempeñoEmpleado, imagen);

            var auditoriaRequest = fichaSupervisionEmpleoRequest.auditoriaRequest;
            ficha.auditoria = new Auditoria(auditoriaRequest.usuarioCreo, auditoriaRequest.fechaCreo,
                auditoriaRequest.usuarioModifico, auditoriaRequest.fechaModifico);
            return ficha;
        }

        private  FirmaAutorizada getFirmaAutorizada(FirmaAutorizadaRequest firmaAutorizadaRequest)
        {
            var userRequest = firmaAutorizadaRequest.userRequest;
            var user = new User(userRequest.Id, "", "", userRequest.password, "", new RolNull());
            var firma = new FirmaAutorizada(user, firmaAutorizadaRequest.fechaCreacion);
            firma.Id = firmaAutorizadaRequest.IdGuid;

            return firma;
        }

        private  Supervisor getSupervisor(SupervisorRequest supervisorRequest)
        {
            var userRequest = supervisorRequest.userRequest;
            var user = new User(userRequest.Id, "", "", userRequest.password, "", new RolNull());
            var supervisor = new Supervisor(user);
            supervisor.Id = supervisorRequest.guid;
            return supervisor;
        }
    }
}