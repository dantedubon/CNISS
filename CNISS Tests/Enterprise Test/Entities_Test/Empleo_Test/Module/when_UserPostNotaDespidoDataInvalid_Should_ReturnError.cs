using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Ports.Input.REST;
using CNISS.CommonDomain.Ports.Input.REST.Modules.EmpleoModule.Commands;
using CNISS.CommonDomain.Ports.Input.REST.Request.AuditoriaRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.EmpleoRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.EmpresaRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.MotivoDespidoRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.UserRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.VisitaRequest;
using CNISS.EnterpriseDomain.Application;
using Machine.Specifications;
using Moq;
using Nancy;
using Nancy.Testing;
using Should;
using It = Machine.Specifications.It;

namespace CNISS_Tests.Enterprise_Test.Entities_Test.Empleo_Test.Module
{
    [Subject(typeof (NotaDespidoModuleInsert))]
    public class when_UserPostNotaDespidoDataInvalid_Should_ReturnError
    {
        private static Browser _browser;
        private static BrowserResponse _response;
        private static NotaDespidoRequest _notaDespidoRequest;

        private Establish context = () =>
        {
            _notaDespidoRequest = new NotaDespidoRequest();
            var userIdentityMovil = new DummyUserIdentityMovil("DRCD");

            var command = Mock.Of<ICommandInsertNotaDespido>();
            var fileGetter = Mock.Of<IFileGetter>();

            _browser = new Browser(x =>
            {
                x.Module<NotaDespidoModuleInsert>();
                x.Dependencies(command, fileGetter);
                x.RequestStartup((container, pipelines, context) =>
                {
                    context.CurrentUser = userIdentityMovil;
                });
            });



        };

        private Because of = () => { _response = _browser.PostSecureJson("/movil/notaDespido", _notaDespidoRequest); };

        It should_return_error = () => _response.StatusCode.ShouldEqual(HttpStatusCode.BadRequest);

       
    }
}