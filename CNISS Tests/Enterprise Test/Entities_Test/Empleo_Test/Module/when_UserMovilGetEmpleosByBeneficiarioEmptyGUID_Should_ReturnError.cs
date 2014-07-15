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
    public class when_UserMovilGetEmpleosByBeneficiarioEmptyGUID_Should_ReturnError
    {
        private static Browser _browser;
        private static BrowserResponse _browserResponse;
        private static string _Id;
        private static string _rtn;
        private static Guid _badSucursalGUID;
        private Establish context = () =>
        {
            var userIdentityMovil = new DummyUserIdentityMovil("DRCD");
            _Id = "0801198512396";
            _rtn = "08011985123960";
            _badSucursalGUID = Guid.Empty;

            var repository = Mock.Of<IEmpleoRepositoryReadOnly>();

            _browser = new Browser(x =>
            {
                x.Module<EmpleoModuleQuery>();
                x.Dependencies(repository);
                x.RequestStartup((container, pipelines, context) =>
                {
                    context.CurrentUser = userIdentityMovil;
                });
            });
        };

        private Because of = () =>
        {
            _browserResponse =
                _browser.GetSecureJson("/movil/empleo/id=" + _Id + "/rtn=" + _rtn + "/sucursal=" +
                                       _badSucursalGUID);
        };

        It should_return_error = () => _browserResponse.StatusCode.ShouldEqual(HttpStatusCode.BadRequest);

       
    }
}