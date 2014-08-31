using System;
using CNISS.CommonDomain.Application;
using CNISS.CommonDomain.Ports.Input.REST.Modules.MotivoDespidoModule.Commands;
using CNISS.CommonDomain.Ports.Input.REST.Modules.TipoEmpleoModule.Commands;
using CNISS.CommonDomain.Ports.Input.REST.Request.AuditoriaRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.EmpleoRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.MotivoDespidoRequest;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using Machine.Specifications;
using Moq;
using Nancy;
using Nancy.Testing;
using Should;
using It = Machine.Specifications.It;

namespace CNISS_Tests.MotivoDespido_Test.Module
{
    [Subject(typeof(MotivoDespidoModuleUpdate))]
    public class when_UserPutMotivoDespidoNotExists_Should_ReturnError
    {
        private static Browser _browser;
        private static BrowserResponse _response;
        private static MotivoDespidoRequest motivoDespidoRequest;
        private static MotivoDespido _expectedMotivoDespido;
        private static ICommandUpdateIdentity<MotivoDespido> _command;

        private Establish context = () =>
        {
            motivoDespidoRequest = new MotivoDespidoRequest()
            {
               
                IdGuid = Guid.NewGuid(),
                descripcion = "tipo",
                auditoriaRequest = new AuditoriaRequest()
                {
                    usuarioCreo = "usuarioCreo",
                    fechaCreo = new DateTime(2014, 8, 1),
                    fechaModifico = new DateTime(2014, 8, 1),
                    usuarioModifico = "usuarioModifico"
                }
            };

            _command = Mock.Of<ICommandUpdateIdentity<MotivoDespido>>();

            _expectedMotivoDespido = getTipoEmpleo(motivoDespidoRequest);

            Mock.Get(_command).Setup(x => x.isExecutable(_expectedMotivoDespido)).Returns(false);

            _browser = new Browser(
                x =>
                {
                    x.Module<MotivoDespidoModuleUpdate>();
                    x.Dependencies(_command);
                }

                );

        };

        private Because of = () =>
        {
            _response = _browser.PutSecureJson("/enterprise/motivoDespido", motivoDespidoRequest);
        };

        It should_return_error = () => _response.StatusCode.ShouldEqual(HttpStatusCode.BadRequest);

        private static MotivoDespido getTipoEmpleo(MotivoDespidoRequest motivoDespidoRequest)
        {
            var motivoDespido = new MotivoDespido(motivoDespidoRequest.descripcion)
            {
                Auditoria = new CNISS.CommonDomain.Domain.Auditoria(
                    motivoDespidoRequest.auditoriaRequest.usuarioCreo,
                    motivoDespidoRequest.auditoriaRequest.fechaCreo,
                    motivoDespidoRequest.auditoriaRequest.usuarioModifico,
                    motivoDespidoRequest.auditoriaRequest.fechaModifico

                    )
            };
            motivoDespido.Id = motivoDespidoRequest.IdGuid;
            return motivoDespido;
        }
    }
}