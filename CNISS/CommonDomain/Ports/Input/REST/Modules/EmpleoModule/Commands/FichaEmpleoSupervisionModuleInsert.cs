using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using CNISS.AutenticationDomain.Domain.Entities;
using CNISS.AutenticationDomain.Domain.Services;
using CNISS.AutenticationDomain.Domain.ValueObjects;
using CNISS.CommonDomain.Application;
using CNISS.CommonDomain.Domain;
using CNISS.CommonDomain.Ports.Input.REST.Modules.BeneficiarioModule.Commands;
using CNISS.CommonDomain.Ports.Input.REST.Request;
using CNISS.CommonDomain.Ports.Input.REST.Request.EmpleoRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.EmpresaRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.VisitaRequest;
using CNISS.EnterpriseDomain.Application;
using CNISS.EnterpriseDomain.Domain.Entities;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using Nancy;
using Nancy.Authentication.Token;
using Nancy.Json;
using Nancy.ModelBinding;
using Nancy.Security;

namespace CNISS.CommonDomain.Ports.Input.REST.Modules.EmpleoModule.Commands
{
    public class FichaEmpleoSupervisionModuleInsert:NancyModule
    {
        private const string directorioImagenes = @"/ImagenesMoviles";
        private const string extensionImagenes = ".jpeg";

        public FichaEmpleoSupervisionModuleInsert(ISerializeJsonRequest serializerJson,Func<string,IEncrytRequestProvider> encryptRequestProvider, ITokenizer tokenizer,ICommandInsertFichaDeSupervision command, IFileGetter fileGetter)
        {
            Post["/movil/fichaSupervision/"] = parameters =>
            {
                

                var movilRequest = this.Bind<MovilRequest>();
                var userId = tokenizer.Detokenize(movilRequest.token,Context);
                if (userId == null)
                {
                    return new Response().WithStatusCode(HttpStatusCode.Unauthorized);
                }

                var desencrypter = encryptRequestProvider(movilRequest.token);
                var fichaString = desencrypter.decryptString(movilRequest.data);


                var fichaRequest = serializerJson.fromJson<FichaSupervisionEmpleoRequest>(fichaString);

             
               
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

        private string returnJson()
        {
            string cosa = @"{
    ""fotografiaBeneficiario"": ""91324018-fd56-499d-9803-f3d9abe1f9f2"",
    ""posicionGPS"": ""posicion"",
    ""cargo"": ""cargo"",
    ""funciones"": ""funciones"",
    ""telefonoFijo"": ""31804422"",
    ""telefonoCelular"": ""31804422"",
    ""desempeñoEmpleado"": ""9"",
    ""supervisor"": {
        ""guid"": ""61305CBC-52E8-41BD-AB28-1ECFF0071DD8"",
		 ""auditoriaRequest"": null,
		  ""lugarVisitaRequests"": null,
        ""userRequest"": {
            ""Id"": ""DRCDF"",
            ""firstName"": """",
            ""secondName"": """",
            ""mail"": """",
            ""password"": ""12345678"",
            ""userRol"": {
                ""idGuid"": null,
                ""name"": null,
                ""description"": null,
                ""nivel"": 0,
                ""auditoriaRequest"": null
            },
            ""auditoriaRequest"": null
        }
    },
    ""empleoId"": ""B5D09572-E626-40E6-B347-A35C0172169D"",
    ""auditoriaRequest"": {
        ""usuarioCreo"": ""dcastillo"",
        ""fechaCreo"": ""2014-06-23T16: 56: 50"",
        ""usuarioModifico"": ""dcastillo"",
        ""fechaModifico"": ""2014-06-23T16: 56: 50""
    },
    ""firma"": {
        ""IdGuid"": ""722d7afb-3ad2-48ca-86bf-a27c459a8a01"",
        ""userRequest"": {
            ""Id"": ""DRCDFirma"",
            ""firstName"": """",
            ""secondName"": """",
            ""mail"": """",
            ""password"": ""12345678"",
            ""userRol"": {
                ""idGuid"": null,
                ""name"": null,
                ""description"": null,
                ""nivel"": 0,
                ""auditoriaRequest"": null
            },
            ""auditoriaRequest"": null
        }
    },
	""beneficiarioRequest"":{
	 ""telefonoFijo"": ""31804422"",
    ""telefonoCelular"": ""31804422"",
	""direccionRequest"": {
            ""idGuid"": ""00000000-0000-0000-0000-000000000000"",
            ""municipioRequest"": {
                ""idMunicipio"": ""01"",
                ""idDepartamento"": ""01"",
                ""nombre"": ""municipio""
            },
            ""departamentoRequest"": {
                ""idDepartamento"": ""01"",
                ""nombre"": ""departamento""
            },
            ""descripcion"": ""Barrio Abajo""
        },
    ""nombreRequest"": {
        ""nombres"": ""Dante"",
        ""primerApellido"": ""Castillo"",
        ""segundoApellido"": ""Dubón""
    },
    ""identidadRequest"": {
        ""identidad"": ""0801198512395""
    },
    ""dependienteRequests"": [
        {
            ""nombreRequest"": {
                ""nombres"": ""Lavinia"",
                ""primerApellido"": ""Dubón"",
                ""segundoApellido"": ""Fajardo""
            },
            ""identidadRequest"": {
                ""identidad"": ""0801195732456""
            },
            ""auditoriaRequest"": {
        ""usuarioCreo"": ""dcastillo"",
        ""fechaCreo"": ""2014-06-23T16: 56: 50"",
        ""usuarioModifico"": ""dcastillo"",
        ""fechaModifico"": ""2014-06-23T16: 56: 50""
    },
            ""parentescoRequest"": {
                ""guid"": ""97574753-22C8-443F-96CC-40C3CD417A06"",
                ""descripcion"": ""Madre""
            },
             ""fechaNacimiento"": ""1984-08-02T00:00:00""
        },
        {
            ""nombreRequest"": {
                ""nombres"": ""Martha"",
                ""primerApellido"": ""Fajardo"",
                ""segundoApellido"": ""Sabillon""
            },
          ""auditoriaRequest"": {
        ""usuarioCreo"": ""dcastillo"",
        ""fechaCreo"": ""2014-06-23T16: 56: 50"",
        ""usuarioModifico"": ""dcastillo"",
        ""fechaModifico"": ""2014-06-23T16: 56: 50""
    },
            ""identidadRequest"": {
                ""identidad"": ""0801193032456""
            },
            ""parentescoRequest"": {
                ""guid"": ""97574753-22C8-443F-96CC-40C3CD417A06"",
                ""descripcion"": ""Madre""
            },
             ""fechaNacimiento"": ""1984-08-02T00:00:00""
        }
    ],
  ""auditoriaRequest"": {
        ""usuarioCreo"": ""dcastillo"",
        ""fechaCreo"": ""2014-06-23T16: 56: 50"",
        ""usuarioModifico"": ""dcastillo"",
        ""fechaModifico"": ""2014-06-23T16: 56: 50""
    },
    ""fechaNacimiento"": ""1984-08-02T00:00:00""
}
	
	
}";

            return cosa;


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