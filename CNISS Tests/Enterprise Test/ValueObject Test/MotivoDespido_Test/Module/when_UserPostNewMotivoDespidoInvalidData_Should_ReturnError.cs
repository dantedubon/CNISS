using CNISS.CommonDomain.Application;
using CNISS.CommonDomain.Ports.Input.REST.Modules.MotivoDespidoModule.Commands;
using CNISS.CommonDomain.Ports.Input.REST.Modules.TipoEmpleoModule.Commands;
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
    [Subject(typeof (MotivoDespidoModuleInsert))]
    public class when_UserPostNewMotivoDespidoInvalidData_Should_ReturnError
    {
        private static Browser _browser;
        private static BrowserResponse _response;
        private static MotivoDespidoRequest _motivoDespidoRequest;

        private Establish context = () =>
        {
            _motivoDespidoRequest = new MotivoDespidoRequest();

            var command = Mock.Of<ICommandInsertIdentity<MotivoDespido>>();

            _browser = new Browser(
                x =>
                {
                    x.Module<MotivoDespidoModuleInsert>();
                    x.Dependencies(command);
                }
                
                );
        };

        private Because of = () => { _response = _browser.PostSecureJson("/enterprise/motivoDespido", _motivoDespidoRequest); };

        It should_return_error = () => _response.StatusCode.ShouldEqual(HttpStatusCode.BadRequest);
    }
}