using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Ports.Input.REST;
using CNISS.CommonDomain.Ports.Input.REST.Modules.EmpleoModule.Commands;
using CNISS.EnterpriseDomain.Application;
using Machine.Specifications;
using Moq;
using Nancy;
using Nancy.Testing;
using Should;
using It = Machine.Specifications.It;

namespace CNISS_Tests.Enterprise_Test.Entities_Test.Empleo_Test.Module
{
    [Subject(typeof (EmpleoModuleUpdateContrato))]
    public class when_UserPutInvalidContrato_Should_ReturnError
    {
        private static Guid _empleoId;
        private static Guid _contratoId;
        private static Browser _browser;
        private static BrowserResponse _response;

        private Establish context = () =>
        {
            _empleoId = Guid.NewGuid();
            _contratoId = Guid.NewGuid();

            var command = Mock.Of<ICommandUpdateEmpleoContrato>();
            var fileGetter = Mock.Of<IFileGetter>();

            Mock.Get(fileGetter).Setup(x => x.existsFile(
                Moq.It.IsAny<string>(), Moq.It.IsAny<string>(), Moq.It.IsAny<string>()

                )).Returns(false);

            _browser = new Browser(
               x =>
               {
                   x.Module<EmpleoModuleUpdateContrato>();
                   x.Dependencies(command, fileGetter);
               }


               );

        };

        private Because of = () =>
        {
            _response =
                _browser.PutSecureJson(
                    "/enterprise/empleos/" + _empleoId.ToString() + "/contract/" + _contratoId.ToString(), "");
        };

        It should_return_error = () => _response.StatusCode.ShouldEqual(HttpStatusCode.BadRequest);
    }
}