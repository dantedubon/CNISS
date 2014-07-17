using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.AutenticationDomain.Domain.Entities;
using CNISS.AutenticationDomain.Domain.ValueObjects;
using CNISS.CommonDomain.Application;
using CNISS.CommonDomain.Ports.Input.REST;
using CNISS.CommonDomain.Ports.Input.REST.Modules.EmpleoModule.Commands;
using CNISS.CommonDomain.Ports.Input.REST.Request.EmpleoRequest;
using CNISS.EnterpriseDomain.Application;
using CNISS.EnterpriseDomain.Domain.Entities;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using Machine.Specifications;
using Moq;
using Nancy;
using Nancy.Testing;
using Should;
using It = Machine.Specifications.It;

namespace CNISS_Tests.Enterprise_Test.Entities_Test.Empleo_Test.Module
{
    [Subject(typeof (FichaEmpleoSupervisionModuleInsert))]
    public class when_UserPostFichaDeEmpleoDataInvalid_Should_ReturnError
    {
        private static Browser _browser;
        private static BrowserResponse _browserResponse;
        private static FichaSupervisionEmpleoRequest _fichaSupervisionEmpleoRequest;


        private Establish context = () =>
        {
            var userIdentityMovil = new DummyUserIdentityMovil("DRCD");
            _fichaSupervisionEmpleoRequest = new FichaSupervisionEmpleoRequest();

            var command = Mock.Of<ICommandInsertFichaDeSupervision>();
            var fileGetter = Mock.Of<IFileGetter>();
            _browser = new Browser(x =>
            {
                x.Module<FichaEmpleoSupervisionModuleInsert>();
                x.Dependencies(command, fileGetter);
                x.RequestStartup((container, pipelines, context) =>
                {
                    context.CurrentUser = userIdentityMovil;
                });
            });


        };

        private Because of = () =>
        {
            _browserResponse = _browser.PostSecureJson("/movil/fichaSupervision/", _fichaSupervisionEmpleoRequest);
        };

        It should_return_error = () => _browserResponse.StatusCode.ShouldEqual(HttpStatusCode.BadRequest);

       
    }
}