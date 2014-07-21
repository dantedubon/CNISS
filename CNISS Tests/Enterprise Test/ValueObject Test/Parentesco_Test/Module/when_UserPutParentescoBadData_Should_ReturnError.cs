using CNISS.CommonDomain.Application;
using CNISS.CommonDomain.Ports.Input.REST.Modules.ParentescoModule.Commands;
using CNISS.CommonDomain.Ports.Input.REST.Modules.TipoEmpleoModule.Commands;
using CNISS.CommonDomain.Ports.Input.REST.Request.BeneficiarioRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.EmpleoRequest;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using Machine.Specifications;
using Moq;
using Nancy;
using Nancy.Testing;
using Should;
using It = Machine.Specifications.It;

namespace CNISS_Tests.Parentesco_Test.Module
{
    [Subject(typeof(ParentescoModuleUpdate))]
    public class when_UserPutParentescoBadData_Should_ReturnError
    {
        private static Browser _browser;
        private static BrowserResponse _response;
        private static ParentescoRequest parentescoRequest;

        private Establish context = () =>
        {
            parentescoRequest = new ParentescoRequest();
            var command = Mock.Of<ICommandUpdateIdentity<Parentesco>>();



            _browser = new Browser(
                x =>
                {
                    x.Module<ParentescoModuleUpdate>();
                    x.Dependencies(command);
                }

                );


        };

        private Because of = () =>
        {
            _response = _browser.PutSecureJson("/enterprise/beneficiarios/parentescos", parentescoRequest);
        };

        It should_return_error = () => _response.StatusCode.ShouldEqual(HttpStatusCode.BadRequest);
    }
}