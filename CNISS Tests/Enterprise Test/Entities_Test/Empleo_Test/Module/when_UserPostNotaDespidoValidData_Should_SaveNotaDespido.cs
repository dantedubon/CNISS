﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.AutenticationDomain.Domain.Entities;
using CNISS.AutenticationDomain.Domain.Services;
using CNISS.AutenticationDomain.Domain.ValueObjects;
using CNISS.CommonDomain.Ports;
using CNISS.CommonDomain.Ports.Input.REST;
using CNISS.CommonDomain.Ports.Input.REST.Modules.EmpleoModule.Commands;
using CNISS.CommonDomain.Ports.Input.REST.Request;
using CNISS.CommonDomain.Ports.Input.REST.Request.AuditoriaRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.EmpleoRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.EmpresaRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.MotivoDespidoRequest;
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
    [Subject(typeof(NotaDespidoModuleInsert))]
    public class when_UserPostNotaDespidoValidData_Should_SaveNotaDespido
    {
        private static Browser _browser;
        private static BrowserResponse _response;
        private static NotaDespidoRequest _notaDespidoRequest;
        private static ICommandInsertNotaDespido _command;
        private static NotaDespido _notaDespido;
        private static MovilRequest _dummyMovilRequest;

        private Establish context = () =>
        {
            _notaDespidoRequest = new NotaDespidoRequest()
            {
                auditoriaRequest = getAuditoriaRequest(),
                empleoId = Guid.NewGuid(),
                fechaDespido = DateTime.Now.Date,
                firmaAutorizadaRequest = getFirmaAutorizadaRequest(),
                imagenNotaDespido = Guid.NewGuid(),
                motivoDespidoRequest = getMotivoDespidoRequest(),
                posicionGPS = "posicionGPS",
                supervisorRequest = getSupervisorRequest()
            };
            var userIdentityMovil = new DummyUserIdentityMovil("DRCD");
            _notaDespido = getNotaDespido(_notaDespidoRequest);


            _dummyMovilRequest = new MovilRequest()
            {
                token = "token",
                data = "data"
            };

            var tokenizer = Mock.Of<ITokenizer>();
            Mock.Get(tokenizer)
                .Setup(x => x.Detokenize(Moq.It.IsAny<string>(), Moq.It.IsAny<NancyContext>()))
                .Returns(userIdentityMovil);
            var serializerJson = Mock.Of<ISerializeJsonRequest>();
            var encryptRequestProvider = getEncrypter();

            _command = Mock.Of<ICommandInsertNotaDespido>();
            var fileGetter = Mock.Of<IFileGetter>();
            Mock.Get(fileGetter)
                .Setup(
                    x =>
                        x.existsFile(@"/ImagenesMoviles",
                            _notaDespidoRequest.imagenNotaDespido.ToString(), ".jpeg")).Returns(true);

            Mock.Get(_command)
                .Setup(x => x.isExecutable(_notaDespidoRequest.empleoId, Moq.It.IsAny<NotaDespido>()))
                .Returns(true);

            Mock.Get(serializerJson)
                .Setup(x => x.fromJson<NotaDespidoRequest>(Moq.It.IsAny<string>()))
                .Returns(_notaDespidoRequest);

            _browser = new Browser(x =>
            {
                x.Module<NotaDespidoModuleInsert>();
                x.MappedDependencies(new[]
                                     {
                                         new Tuple<Type, object>(typeof(ISerializeJsonRequest),serializerJson),
                                         new Tuple<Type, object>(typeof(Func<string,IEncrytRequestProvider>),encryptRequestProvider),
                                        new Tuple<Type, object>(typeof(ITokenizer),tokenizer),
                                        new Tuple<Type, object>(typeof(ICommandInsertNotaDespido),_command),
                                        new Tuple<Type, object>(typeof(IFileGetter),fileGetter)
                                       
                                     });
               
            });



        };

        private Because of = () => { _response = _browser.PostSecureJson("/movil/notaDespido", _dummyMovilRequest); };

        It should_save_notaDespido = () => Mock.Get(_command).Verify(x =>
            x.execute(_notaDespidoRequest.empleoId,Moq.It.Is<NotaDespido>(
            z => z.FirmaAutorizada.Id == _notaDespido.FirmaAutorizada.Id 
            && z.FechaDespido == _notaDespido.FechaDespido 
            && z.MotivoDespido.Id == _notaDespido.MotivoDespido.Id
            && z.PosicionGps == _notaDespido.PosicionGps
            && z.Supervisor.Id == _notaDespido.Supervisor.Id)));

        private static Func<string, IEncrytRequestProvider> getEncrypter()
        {
            var x = new Func<string, IEncrytRequestProvider>(z => new DummyEncrytRequestProvider());
            return x;
        }


          private static NotaDespido getNotaDespido(NotaDespidoRequest notaDespidoRequest)
        {
            var motivoDespido = getMotivoDespido(notaDespidoRequest.motivoDespidoRequest);
            var supervisor = getSupervisor(notaDespidoRequest.supervisorRequest);
            var firma = getFirmaAutorizada(notaDespidoRequest.firmaAutorizadaRequest);

            var notaDespido = new NotaDespido(motivoDespido, notaDespidoRequest.fechaDespido,
                notaDespidoRequest.posicionGPS, supervisor, firma);
            return notaDespido;
        }

        private static MotivoDespido getMotivoDespido(MotivoDespidoRequest motivoDespidoRequest)
        {
            return new MotivoDespido(motivoDespidoRequest.descripcion){Id = motivoDespidoRequest.IdGuid};
        }

        private static FirmaAutorizada getFirmaAutorizada(FirmaAutorizadaRequest firmaAutorizadaRequest)
        {
            var userRequest = firmaAutorizadaRequest.userRequest;
            var user = new User(userRequest.Id, "", "", userRequest.password, "", new RolNull());
            var firma = new FirmaAutorizada(user, firmaAutorizadaRequest.fechaCreacion);
            firma.Id = firmaAutorizadaRequest.IdGuid;

            return firma;
        }

        private  static  Supervisor getSupervisor(SupervisorRequest supervisorRequest)
        {
            var userRequest = supervisorRequest.userRequest;
            var user = new User(userRequest.Id, "", "", userRequest.password, "", new RolNull());
            var supervisor = new Supervisor(user);
            supervisor.Id = supervisorRequest.guid;
            return supervisor;
        }

        private static MotivoDespidoRequest getMotivoDespidoRequest()
        {
            return new MotivoDespidoRequest()
            {
                IdGuid = Guid.NewGuid()
            };
        }

        private static FirmaAutorizadaRequest getFirmaAutorizadaRequest()
        {
            return new FirmaAutorizadaRequest()
            {
                IdGuid = Guid.NewGuid(),
                userRequest = getUserRequest()

            };
        }


        private static AuditoriaRequest getAuditoriaRequest()
        {
            return new AuditoriaRequest()
            {
                fechaCreo = new DateTime(2014, 8, 2),
                usuarioCreo = "usuarioCreo",
                fechaModifico = new DateTime(2014, 8, 2),
                usuarioModifico = "usuarioModifico"
            };
        }
        private static SupervisorRequest getSupervisorRequest()
        {
            return new SupervisorRequest()
            {
                guid = Guid.NewGuid(),
                userRequest = new UserRequest()
                {
                    Id = "User",

                    password = "xxxx",

                }
            };

        }



        private static UserRequest getUserRequest()
        {
            return new UserRequest()
            {
                Id = "User",
                password = "xxxx",

            };
        }

    }
}