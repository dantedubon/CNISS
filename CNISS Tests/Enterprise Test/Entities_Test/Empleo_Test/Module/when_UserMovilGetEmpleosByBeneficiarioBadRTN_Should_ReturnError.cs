using System;
using CNISS.CommonDomain.Ports.Input.REST.Modules.EmpleoModule.Query;
using CNISS.EnterpriseDomain.Domain.Repositories;
using Machine.Specifications;
using Moq;
using Nancy;
using Nancy.Testing;
using Should;
using It = Machine.Specifications.It;

namespace CNISS_Tests.Enterprise_Test.Entities_Test.Empleo_Test.Module
{
    [Subject(typeof(EmpleoModuleQuery))]
    public class when_UserMovilGetEmpleosByBeneficiarioBadRTN_Should_ReturnError
    {
        private static Browser _browser;
        private static BrowserResponse _browserResponse;
        private static string _Id;
        private static string _BadRtn;
        private static Guid _sucursalGuid;
        private Establish context = () =>
        {

            _Id = "0801198512396";
            _BadRtn = "0801198512396";
            _sucursalGuid = Guid.NewGuid();

            var repository = Mock.Of<IEmpleoRepositoryReadOnly>();

            _browser = new Browser(x =>
            {
                x.Module<EmpleoModuleQuery>();
                x.Dependencies(repository);
            });
        };

        private Because of = () =>
        {
            _browserResponse =
                _browser.GetSecureJson("/movil/empleo/id=" + _Id + "/rtn=" + _BadRtn + "/sucursal=" +
                                       _sucursalGuid);
        };

        It should_return_error = () => _browserResponse.StatusCode.ShouldEqual(HttpStatusCode.BadRequest);
    }
}