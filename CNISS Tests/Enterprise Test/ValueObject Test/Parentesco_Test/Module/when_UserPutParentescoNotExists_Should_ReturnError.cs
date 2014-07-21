using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Application;
using CNISS.CommonDomain.Ports.Input.REST.Modules.ActividadEconomicaModule.Commands;
using CNISS.CommonDomain.Ports.Input.REST.Modules.ParentescoModule.Commands;
using CNISS.CommonDomain.Ports.Input.REST.Modules.TipoEmpleoModule.Commands;
using CNISS.CommonDomain.Ports.Input.REST.Request.AuditoriaRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.BeneficiarioRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.EmpleoRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.EmpresaRequest;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using Machine.Specifications;
using Moq;
using Nancy;
using Nancy.Testing;
using Should;
using It = Machine.Specifications.It;

namespace CNISS_Tests.Enterprise_Test.ValueObject_Test.TipoEmpleo_Test.Module
{
    [Subject(typeof(ParentescoModuleUpdate))]
    public class when_UserPutParentescoNotExists_Should_ReturnError
    {
        private static Browser _browser;
        private static BrowserResponse _response;
        private static ParentescoRequest parentescoRequest;
        private static Parentesco _expectedParentesco;
        private static ICommandUpdateIdentity<Parentesco> _command;

        private Establish context = () =>
        {
            parentescoRequest = new ParentescoRequest()
            {
               
                guid = Guid.NewGuid(),
                descripcion = "tipo",
                auditoriaRequest = new AuditoriaRequest()
                {
                    usuarioCreo = "usuarioCreo",
                    fechaCreo = new DateTime(2014, 8, 1),
                    fechaModifico = new DateTime(2014, 8, 1),
                    usuarioModifico = "usuarioModifico"
                }
            };

            _command = Mock.Of<ICommandUpdateIdentity<Parentesco>>();

            _expectedParentesco = getParentesco(parentescoRequest);

            Mock.Get(_command).Setup(x => x.isExecutable(_expectedParentesco)).Returns(false);

            _browser = new Browser(
                x =>
                {
                    x.Module<ParentescoModuleUpdate>();
                    x.Dependencies(_command);
                }

                );

        };

        private Because of = () =>
        {
            _response = _browser.PutSecureJson("/enterprise/beneficiarios/parentescos", parentescoRequest);
        };

        It should_return_error = () => _response.StatusCode.ShouldEqual(HttpStatusCode.BadRequest);

        private static Parentesco getParentesco(ParentescoRequest parentescoRequest)
        {
            var parentesco = new Parentesco(parentescoRequest.descripcion)
            {
                auditoria = new CNISS.CommonDomain.Domain.Auditoria(
                    parentescoRequest.auditoriaRequest.usuarioCreo,
                    parentescoRequest.auditoriaRequest.fechaCreo,
                    parentescoRequest.auditoriaRequest.usuarioModifico,
                    parentescoRequest.auditoriaRequest.fechaModifico

                    )
            };
            parentesco.Id = parentescoRequest.guid;
            return parentesco;
        }
    }
}