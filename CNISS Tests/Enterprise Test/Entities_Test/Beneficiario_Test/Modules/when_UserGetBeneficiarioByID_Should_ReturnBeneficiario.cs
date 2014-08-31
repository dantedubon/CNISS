using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Compilation;
using CNISS.CommonDomain.Domain;
using CNISS.CommonDomain.Ports.Input.REST.Modules.BeneficiarioModule.Query;
using CNISS.CommonDomain.Ports.Input.REST.Request.AuditoriaRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.BeneficiarioRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.GremioRequest;
using CNISS.EnterpriseDomain.Domain.Entities;
using CNISS.EnterpriseDomain.Domain.Repositories;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using FizzWare.NBuilder;
using FluentAssertions;
using Machine.Specifications;
using Moq;
using Nancy;
using Nancy.Testing;
using Should;
using It = Machine.Specifications.It;

namespace CNISS_Tests.Enterprise_Test.Entities_Test.Beneficiario_Test.Modules
{
    [Subject(typeof(BeneficiarioModuleQuery))]
    public class when_UserGetBeneficiarioByID_Should_ReturnBeneficiario
    {
        static Browser _browser;
        static BrowserResponse _response;
        private static IdentidadRequest _requestId;
        private static BeneficiarioRequest _expectedBeneficiario;
        private static BeneficiarioRequest _responseBeneficiario;

        private Establish context = () =>
        {
            _requestId = new IdentidadRequest() { identidad = "0801198512396" };

            var beneficiario = Builder<Beneficiario>.CreateNew().WithConstructor(
                () => new Beneficiario(new Identidad("0801198512396"),Builder<Nombre>.CreateNew().Build(),new DateTime(1984,8,2))
                ).With(x => x.Auditoria = Builder<Auditoria>.CreateNew().Build()).Build();

            var dependientes = Builder<Dependiente>.CreateListOfSize(10).All().WithConstructor(
                ()=> new Dependiente(new Identidad("0501198812345"),Builder<Nombre>.CreateNew().Build(),Builder<Parentesco>.CreateNew().Build(),new DateTime(1984,8,2))
                ).With(x => x.auditoria = Builder<Auditoria>.CreateNew().Build()).Build();

            foreach (var dependiente in dependientes)
            {
                beneficiario.addDependiente(dependiente);
            }

            _expectedBeneficiario = getRequest(beneficiario);

            var repository = Mock.Of<IBeneficiarioRepositoryReadOnly>();
            Mock.Get(repository).Setup(x => x.get(Moq.It.IsAny<Identidad>())).Returns(beneficiario);



            _browser = new Browser(
                x =>
                {
                    x.Module<BeneficiarioModuleQuery>();
                    x.Dependencies(repository);
                }

                );



        };

        

        private Because of = () => { _responseBeneficiario = _browser.GetSecureJson("/enterprise/beneficiarios/id=", _requestId)
            .Body
            .DeserializeJson<BeneficiarioRequest>();
        };

        private It should_return_beneficiario =
            () => _responseBeneficiario.ShouldBeEquivalentTo(_expectedBeneficiario);


        private static BeneficiarioRequest getRequest(Beneficiario beneficiario)
        {
            return new BeneficiarioRequest()
            {
                nombreRequest = new NombreRequest()
                {
                    nombres = beneficiario.Nombre.Nombres,
                    primerApellido = beneficiario.Nombre.PrimerApellido,
                    segundoApellido = beneficiario.Nombre.SegundoApellido
                },
                fechaNacimiento = beneficiario.FechaNacimiento,
                identidadRequest = new IdentidadRequest() { identidad = beneficiario.Id.identidad },
                dependienteRequests = beneficiario.Dependientes.Select(x => new DependienteRequest()
                {
                    IdGuid = x.idGuid,
                    fechaNacimiento = x.FechaNacimiento,
                    identidadRequest = new IdentidadRequest() { identidad = x.Id.identidad },
                    nombreRequest = new NombreRequest()
                    {
                        nombres = x.Nombre.Nombres,
                        primerApellido = x.Nombre.PrimerApellido,
                        segundoApellido = x.Nombre.SegundoApellido
                    },
                    parentescoRequest = new ParentescoRequest()
                    {
                        descripcion = x.Parentesco.Descripcion,
                        guid = x.Parentesco.Id
                    },
                    auditoriaRequest = new AuditoriaRequest()
                    {
                        fechaCreo = x.auditoria.FechaCreacion,
                        fechaModifico = x.auditoria.FechaActualizacion,
                        usuarioCreo = x.auditoria.CreadoPor,
                        usuarioModifico = x.auditoria.ActualizadoPor
                    }
                }),
                auditoriaRequest = new AuditoriaRequest()
                {
                    fechaCreo = beneficiario.Auditoria.FechaCreacion,
                    fechaModifico = beneficiario.Auditoria.FechaActualizacion,
                    usuarioCreo = beneficiario.Auditoria.CreadoPor,
                    usuarioModifico = beneficiario.Auditoria.ActualizadoPor
                }

            };
        }
    }
}