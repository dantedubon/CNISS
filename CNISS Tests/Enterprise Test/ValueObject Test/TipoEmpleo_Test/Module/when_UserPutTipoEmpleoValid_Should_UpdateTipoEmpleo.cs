using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Application;
using CNISS.CommonDomain.Ports.Input.REST.Modules.TipoEmpleoModule.Commands;
using CNISS.CommonDomain.Ports.Input.REST.Request.AuditoriaRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.EmpleoRequest;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using Machine.Specifications;
using Moq;
using Nancy;
using Nancy.Testing;
using Should;
using It = Machine.Specifications.It;

namespace CNISS_Tests.Enterprise_Test.ValueObject_Test.TipoEmpleo_Test.Module
{
    [Subject(typeof(TipoEmpleoModuleUpdate))]
    public class when_UserPutTipoEmpleoValid_Should_UpdateTipoEmpleo
    {
        private static Browser _browser;
        private static BrowserResponse _response;
        private static TipoEmpleoRequest tipoEmpleoRequest;
        private static TipoEmpleo _expectedTipoEmpleo;
        private static ICommandUpdateIdentity<TipoEmpleo> _command;

        private Establish context = () =>
        {
            tipoEmpleoRequest = new TipoEmpleoRequest()
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

            _command = Mock.Of<ICommandUpdateIdentity<TipoEmpleo>>();

            _expectedTipoEmpleo = getTipoEmpleo(tipoEmpleoRequest);

            Mock.Get(_command).Setup(x => x.isExecutable(Moq.It.Is<TipoEmpleo>(z => z.Id==_expectedTipoEmpleo.Id))).Returns(true);

            _browser = new Browser(
                x =>
                {
                    x.Module<TipoEmpleoModuleUpdate>();
                    x.Dependencies(_command);
                }

                );

        };

        private Because of = () =>
        {
            _response = _browser.PutSecureJson("/enterprise/tipoEmpleo", tipoEmpleoRequest);
        };

        It should_update_tipoEmpleo =
            () =>
                Mock.Get(_command)
                    .Verify(
                        x =>
                            x.execute(
                                Moq.It.Is<TipoEmpleo>(
                                    z =>
                                        z.Id == _expectedTipoEmpleo.Id &&
                                        z.Descripcion == _expectedTipoEmpleo.Descripcion)));

        private static TipoEmpleo getTipoEmpleo(TipoEmpleoRequest tipoEmpleoRequest)
        {
            var tipoEmpleo = new TipoEmpleo(tipoEmpleoRequest.descripcion)
            {
                Auditoria = new CNISS.CommonDomain.Domain.Auditoria(
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