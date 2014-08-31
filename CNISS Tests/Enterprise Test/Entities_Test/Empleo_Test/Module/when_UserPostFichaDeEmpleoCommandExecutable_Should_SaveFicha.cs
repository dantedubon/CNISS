using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.AutenticationDomain.Domain.Entities;
using CNISS.AutenticationDomain.Domain.Services;
using CNISS.AutenticationDomain.Domain.ValueObjects;
using CNISS.CommonDomain.Domain;
using CNISS.CommonDomain.Ports;
using CNISS.CommonDomain.Ports.Input.REST;
using CNISS.CommonDomain.Ports.Input.REST.Modules.BeneficiarioModule.Commands;
using CNISS.CommonDomain.Ports.Input.REST.Modules.EmpleoModule.Commands;
using CNISS.CommonDomain.Ports.Input.REST.Request;
using CNISS.CommonDomain.Ports.Input.REST.Request.AuditoriaRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.BeneficiarioRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.EmpleoRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.EmpresaRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.GremioRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.UserRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.VisitaRequest;
using CNISS.EnterpriseDomain.Application;
using CNISS.EnterpriseDomain.Domain.Entities;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using Machine.Specifications;
using Moq;
using Nancy;
using Nancy.Authentication.Token;
using Nancy.Testing;
using Should;
using It = Machine.Specifications.It;

namespace CNISS_Tests.Enterprise_Test.Entities_Test.Empleo_Test.Module
{
    [Subject(typeof(FichaEmpleoSupervisionModuleInsert))]
    public class when_UserPostFichaDeEmpleoCommandExecutable_Should_SaveFicha
    {
        private static Browser _browser;
        private static BrowserResponse _browserResponse;
        private static FichaSupervisionEmpleoRequest _fichaSupervisionEmpleoRequest;
        private static FichaSupervisionEmpleo _ficha;
        private static Beneficiario _beneficiario;
        private static Guid _idEmpleo;
        private static ICommandInsertFichaDeSupervision _command;
        private static MovilRequest _dummyMovilRequest;


        private Establish context = () =>
        {
            var userIdentityMovil = new DummyUserIdentityMovil("DRCD");
            _fichaSupervisionEmpleoRequest = getFichaSupervisionEmpleoRequest();

            _dummyMovilRequest = new MovilRequest()
                                {
                                    token = "token",
                                    data = "data"
                                };
            var dataImage = new byte[] { 1, 1 };
            _command = Mock.Of<ICommandInsertFichaDeSupervision>();
            var fileGetter = Mock.Of<IFileGetter>();
            var tokenizer = Mock.Of<ITokenizer>();
            Mock.Get(tokenizer)
                .Setup(x => x.Detokenize(Moq.It.IsAny<string>(), Moq.It.IsAny<NancyContext>()))
                .Returns(userIdentityMovil);
            var serializerJson = Mock.Of<ISerializeJsonRequest>();
            var encryptRequestProvider = getEncrypter();


            Mock.Get(fileGetter)
                .Setup(
                    x =>
                        x.existsFile(@"/ImagenesMoviles",
                            _fichaSupervisionEmpleoRequest.fotografiaBeneficiario.ToString(), ".jpeg")).Returns(true);

            Mock.Get(fileGetter)
                .Setup(
                    x =>
                        x.getFile(@"/ImagenesMoviles",
                            _fichaSupervisionEmpleoRequest.fotografiaBeneficiario.ToString(), ".jpeg")
                ).Returns(dataImage);

            var contentFile = new ContentFile(dataImage);

            _ficha = getFichaSupervisionEmpleo(_fichaSupervisionEmpleoRequest, contentFile);

            Mock.Get(serializerJson)
                .Setup(x => x.fromJson<FichaSupervisionEmpleoRequest>(Moq.It.IsAny<string>()))
                .Returns(_fichaSupervisionEmpleoRequest);

            _beneficiario = new BeneficiarioMap().getBeneficiario(_fichaSupervisionEmpleoRequest.beneficiarioRequest);

            _idEmpleo = _fichaSupervisionEmpleoRequest.empleoId;

            Mock.Get(_command)
                .Setup(x => x.isExecutable(Moq.It.Is<FichaSupervisionEmpleo>(z => z.Firma.Id == _ficha.Firma.Id), Moq.It.Is<Beneficiario>(z => z.Id.identidad == _beneficiario.Id.identidad), _idEmpleo))
                .Returns(true);




            _browser = new Browser(x =>
            {
                x.Module<FichaEmpleoSupervisionModuleInsert>();
                x.MappedDependencies(new[]
                                     {
                                         new Tuple<Type, object>(typeof(ISerializeJsonRequest),serializerJson),
                                         new Tuple<Type, object>(typeof(Func<string,IEncrytRequestProvider>),encryptRequestProvider),
                                        new Tuple<Type, object>(typeof(ITokenizer),tokenizer),
                                        new Tuple<Type, object>(typeof(ICommandInsertFichaDeSupervision),_command),
                                        new Tuple<Type, object>(typeof(IFileGetter),fileGetter)
                                       
                                     });


            });


        };

        private static Func<string, IEncrytRequestProvider> getEncrypter()
        {
            var x = new Func<string, IEncrytRequestProvider>(z => new DummyEncrytRequestProvider());
            return x;
        }

        private Because of = () =>
        {
            _browserResponse = _browser.PostSecureJson("/movil/fichaSupervision/", _dummyMovilRequest);
        };

         It should_save_ficha = () => Mock.Get(_command).Verify(x => x.execute(Moq.It.Is<FichaSupervisionEmpleo>(z => z.Firma.Id == _ficha.Firma.Id)
            
             , Moq.It.Is<Beneficiario>(z => z.Id.identidad == _beneficiario.Id.identidad), _idEmpleo));


        private static FichaSupervisionEmpleo getFichaSupervisionEmpleo(
            FichaSupervisionEmpleoRequest fichaSupervisionEmpleoRequest, ContentFile imagen)
        {
            var firma = getFirmaAutorizada(fichaSupervisionEmpleoRequest.firma);
            var supervisor = getSupervisor(fichaSupervisionEmpleoRequest.supervisor);


            var ficha = new FichaSupervisionEmpleo(supervisor, firma, fichaSupervisionEmpleoRequest.posicionGPS, fichaSupervisionEmpleoRequest.cargo,
                fichaSupervisionEmpleoRequest.funciones, fichaSupervisionEmpleoRequest.telefonoFijo, fichaSupervisionEmpleoRequest.telefonoCelular,
                fichaSupervisionEmpleoRequest.desempeñoEmpleado, imagen);

            var auditoriaRequest = fichaSupervisionEmpleoRequest.auditoriaRequest;
            ficha.Auditoria = new Auditoria(auditoriaRequest.usuarioCreo, auditoriaRequest.fechaCreo,
                auditoriaRequest.usuarioModifico, auditoriaRequest.fechaModifico);
            return ficha;
        }

        private static FirmaAutorizada getFirmaAutorizada(FirmaAutorizadaRequest firmaAutorizadaRequest)
        {
            var userRequest = firmaAutorizadaRequest.userRequest;
            var user = new User(userRequest.Id, "", "", userRequest.password, "", new RolNull());
            var firma = new FirmaAutorizada(user, firmaAutorizadaRequest.fechaCreacion);
            firma.Id = firmaAutorizadaRequest.IdGuid;

            return firma;
        }

        private static Supervisor getSupervisor(SupervisorRequest supervisorRequest)
        {
            var userRequest = supervisorRequest.userRequest;
            var user = new User(userRequest.Id, "", "", userRequest.password, "", new RolNull());
            var supervisor = new Supervisor(user);
            supervisor.Id = supervisorRequest.guid;
            return supervisor;
        }

        private static FichaSupervisionEmpleoRequest getFichaSupervisionEmpleoRequest()
        {
            return new FichaSupervisionEmpleoRequest()
            {
                cargo = "cargo",
                desempeñoEmpleado = 1,
                empleoId = Guid.NewGuid(),
                firma = new FirmaAutorizadaRequest()
                {
                    IdGuid = Guid.NewGuid(),
                    userRequest = new UserRequest()
                    {
                        Id = "DRCD",
                        password = "123456"
                    }

                },
                fotografiaBeneficiario = Guid.NewGuid(),
                funciones = "funciones",
                posicionGPS = "posicionGPS",
                supervisor = new SupervisorRequest()
                {
                    guid = Guid.NewGuid(),
                    userRequest = new UserRequest()
                    {
                        Id = "DRCD",
                        password = "XXX"
                    }

                },
                auditoriaRequest = new AuditoriaRequest()
                {
                    fechaCreo = DateTime.Now.Date,
                    fechaModifico = DateTime.Now.Date,
                    usuarioCreo = "usuarioCreo",
                    usuarioModifico = "usuarioModifico"
                },
                telefonoCelular = "31804433",
                telefonoFijo = "31804433",
                beneficiarioRequest = new BeneficiarioRequest()
                {
                    dependienteRequests = getDependienteRequest(),
                    fechaNacimiento = DateTime.Now.Date,
                    identidadRequest = getIdentidadRequest(),
                    nombreRequest = getNombreRequest(),
                    auditoriaRequest = new AuditoriaRequest()
                    {
                        fechaCreo = DateTime.Now.Date,
                        fechaModifico = DateTime.Now.Date,
                        usuarioCreo = "usuarioCreo",
                        usuarioModifico = "usuarioModifico"
                    },
                    direccionRequest = new DireccionRequest()
                    {
                        departamentoRequest = new DepartamentoRequest() { idDepartamento = "01", nombre = "FM" },
                        municipioRequest = new MunicipioRequest()
                        {
                            idDepartamento = "01",
                            idMunicipio = "01",
                            nombre = "DC"

                        },
                        descripcion = "Barrio Abajo"
                    },
                    fotografiaBeneficiario = Guid.NewGuid().ToString(),
                    telefonoCelular = "31804433",
                    telefonoFijo = "31804433"

                }
            };
        }

        private static IEnumerable<DependienteRequest> getDependienteRequest()
        {
            return new List<DependienteRequest>()
            {
                new DependienteRequest()
                {
                    identidadRequest = getIdentidadRequest(),
                    nombreRequest = getNombreRequest(),
                    parentescoRequest = getParentescoRequest(),
                    auditoriaRequest = new AuditoriaRequest()
                    {
                        fechaCreo = DateTime.Now.Date,
                        fechaModifico = DateTime.Now.Date,
                        usuarioCreo = "usuarioCreo",
                        usuarioModifico = "usuarioModifico"
                    }

                }
            };
        }

        private static NombreRequest getNombreRequest()
        {
            return new NombreRequest()
            {
                nombres = "Dante Ruben",
                primerApellido = "Castillo",
                segundoApellido = "Dubon"
            };
        }

        private static IdentidadRequest getIdentidadRequest()
        {
            return new IdentidadRequest()
            {
                identidad = "0801198512396"
            };
        }

        private static ParentescoRequest getParentescoRequest()
        {
            return new ParentescoRequest()
            {
                descripcion = "Esposo"
            };
        }


    }
}