using CNISS.CommonDomain.Application;
using CNISS.CommonDomain.Ports.Input.REST.Modules.MotivoDespidoModule.Commands;
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
    public class when_UserPutMotivoDespidoBadData_Should_ReturnError
    {
        private static Browser _browser;
        private static BrowserResponse _response;
        private static MotivoDespidoRequest motivoDespidoRequest;

        private Establish context = () =>
        {
            motivoDespidoRequest = new MotivoDespidoRequest();
            var command = Mock.Of<ICommandUpdateIdentity<MotivoDespido>>();



            _browser = new Browser(
                x =>
                {
                    x.Module<MotivoDespidoModuleUpdate>();
                    x.Dependencies(command);
                }

                );


        };

        private Because of = () =>
        {
            _response = _browser.PutSecureJson("/enterprise/motivoDespido", motivoDespidoRequest);
        };

        It should_return_error = () => _response.StatusCode.ShouldEqual(HttpStatusCode.BadRequest);
    }
}