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
using Nancy.Testing;
using It = Machine.Specifications.It;

namespace CNISS_Tests.MotivoDespido_Test.Module
{
    [Subject(typeof(MotivoDespidoModuleUpdate))]
    public class when_UserPutTipoEmpleoValid_Should_UpdateTipoEmpleo
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

            Mock.Get(_command).Setup(x => x.isExecutable(Moq.It.Is<MotivoDespido>(z => z.Id==_expectedMotivoDespido.Id))).Returns(true);

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

        It should_update_tipoEmpleo =
            () =>
                Mock.Get(_command)
                    .Verify(
                        x =>
                            x.execute(
                                Moq.It.Is<MotivoDespido>(
                                    z =>
                                        z.Id == _expectedMotivoDespido.Id &&
                                        z.descripcion == _expectedMotivoDespido.descripcion)));

        private static MotivoDespido getTipoEmpleo(MotivoDespidoRequest tipoEmpleoRequest)
        {
            var tipoEmpleo = new MotivoDespido(tipoEmpleoRequest.descripcion)
            {
                auditoria = new CNISS.CommonDomain.Domain.Auditoria(
                    tipoEmpleoRequest.auditoriaRequest.usuarioCreo,
                    tipoEmpleoRequest.auditoriaRequest.fechaCreo,
                    tipoEmpleoRequest.auditoriaRequest.usuarioModifico,
                    tipoEmpleoRequest.auditoriaRequest.fechaModifico

                    )
            };
            tipoEmpleo.Id = tipoEmpleoRequest.IdGuid;
            return tipoEmpleo;
        }
    }
}