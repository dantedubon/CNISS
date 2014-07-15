using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Ports.Input.REST.Modules.EmpleoModule.Query;
using CNISS.EnterpriseDomain.Domain.Repositories;
using FluentAssertions;
using Machine.Specifications;
using Moq;
using Nancy;
using Nancy.Testing;
using Should;
using It = Machine.Specifications.It;

namespace CNISS_Tests.Enterprise_Test.Entities_Test.Empleo_Test.Module
{
    [Subject(typeof (EmpleoModuleQuery))]
    public class when_UserMovilGetEmpleosByBeneficiarioBadID_Should_ReturnError
    {
        private static Browser _browser;
        private static BrowserResponse _browserResponse;
        private static string _badId;
        private static string _RTN;
        private static Guid _sucursalGuid;
        private Establish context = () =>
        {
            var userIdentityMovil = new DummyUserIdentityMovil("DRCD");
            _badId = "08011955";
            _RTN = "08011985123960";
            _sucursalGuid = Guid.NewGuid();

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
                _browser.GetSecureJson("/movil/empleo/id=" + _badId + "/rtn=" + _RTN + "/sucursal=" +
                                       _sucursalGuid);
        };

        It should_return_error = () => _browserResponse.StatusCode.ShouldEqual(HttpStatusCode.BadRequest);
    }
}