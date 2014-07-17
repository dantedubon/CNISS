﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.AutenticationDomain.Domain.Entities;
using CNISS.AutenticationDomain.Domain.ValueObjects;
using CNISS.CommonDomain.Application;
using CNISS.CommonDomain.Ports.Input.REST;
using CNISS.CommonDomain.Ports.Input.REST.Modules.EmpleoModule.Commands;
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
using Nancy.Testing;
using Should;
using It = Machine.Specifications.It;

namespace CNISS_Tests.Enterprise_Test.Entities_Test.Empleo_Test.Module
{
    [Subject(typeof (FichaEmpleoSupervisionModuleInsert))]
    public class when_UserPostFichaDeEmpleoFotografiaBeneficiarioNotExists_Should_ReturnError
    {
        private static Browser _browser;
        private static BrowserResponse _browserResponse;
        private static FichaSupervisionEmpleoRequest _fichaSupervisionEmpleoRequest;


        private Establish context = () =>
        {
            var userIdentityMovil = new DummyUserIdentityMovil("DRCD");
            _fichaSupervisionEmpleoRequest = getFichaSupervisionEmpleoRequest();

            var command = Mock.Of<ICommandInsertFichaDeSupervision>();
            var fileGetter = Mock.Of<IFileGetter>();
            Mock.Get(fileGetter)
                .Setup(
                    x =>
                        x.existsFile(@"/ImagenesMoviles",
                            _fichaSupervisionEmpleoRequest.fotografiaBeneficiario.ToString(), ".jpeg")).Returns(false);

            _browser = new Browser(x =>
            {
                x.Module<FichaEmpleoSupervisionModuleInsert>();
                x.Dependencies(command, fileGetter);
                x.RequestStartup((container, pipelines, context) =>
                {
                    context.CurrentUser = userIdentityMovil;
                });
            });


        };

        private Because of = () =>
        {
            _browserResponse = _browser.PostSecureJson("/movil/fichaSupervision/", _fichaSupervisionEmpleoRequest);
        };

         It should_return_error = () => _browserResponse.StatusCode.ShouldEqual(HttpStatusCode.BadRequest);


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
                    nombreRequest = getNombreRequest()
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
                    parentescoRequest = getParentescoRequest()
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