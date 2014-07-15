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
    [Subject(typeof(MotivoDespidoModuleInsert))]
    public class when_UserPostNewMotivoDespidoValidData_Should_SaveTipoEmpleo
    {
        private static Browser _browser;
        private static BrowserResponse _response;
        private static MotivoDespidoRequest _motivoDespidoRequest;
        private static MotivoDespido _motivoDespidoExpected;
        private static ICommandInsertIdentity<MotivoDespido> _command; 


        private Establish context = () =>
        {
            _motivoDespidoRequest = new MotivoDespidoRequest()
            {
                descripcion = "tipoEmpleo",
                auditoriaRequest = new AuditoriaRequest()
                {
                    usuarioCreo = "usuarioCreo",
                    fechaCreo = new DateTime(2014, 8, 1),
                    fechaModifico = new DateTime(2014, 8, 1),
                    usuarioModifico = "usuarioModifico"
                }
            };

             _command = Mock.Of<ICommandInsertIdentity<MotivoDespido>>();
            _motivoDespidoExpected = getTipoEmpleo(_motivoDespidoRequest);


            _browser = new Browser(
                x =>
                {
                    x.Module<MotivoDespidoModuleInsert>();
                    x.Dependencies(_command);
                }

                );
        };

        private Because of = () => { _response = _browser.PostSecureJson("/enterprise/motivoDespido", _motivoDespidoRequest); };



         It should_save_tipoEmpleo = () => Mock.Get(_command)
            .Verify(x => x.execute(Moq.It.Is<MotivoDespido>(z => z.descripcion == _motivoDespidoExpected.descripcion)));

        private static MotivoDespido getTipoEmpleo(MotivoDespidoRequest tipoEmpleoRequest)
        {
            var motivoDespido = new MotivoDespido(tipoEmpleoRequest.descripcion)
            {
                auditoria = new CNISS.CommonDomain.Domain.Auditoria(
                    tipoEmpleoRequest.auditoriaRequest.usuarioCreo,
                    tipoEmpleoRequest.auditoriaRequest.fechaCreo,
                    tipoEmpleoRequest.auditoriaRequest.usuarioModifico,
                    tipoEmpleoRequest.auditoriaRequest.fechaModifico

                    )
            };
            return motivoDespido;
        }
    }
}