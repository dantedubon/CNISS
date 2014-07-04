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
    [Subject(typeof(EmpleoModuleUpdateImagenComprobantePago))]
    public class when_UserPutEmpleoComprobanteImageContractInvalidImageFile_Should_ReturnError
    {
        private static Guid _empleoId;
        private static Guid _imageId;
        private static Guid _comprobanteId;
        private static Browser _browser;
        private static BrowserResponse _response;

        private Establish context = () =>
        {
            _empleoId = Guid.NewGuid();
            _comprobanteId = Guid.NewGuid();
            _imageId = Guid.NewGuid();

            var command = Mock.Of<ICommandUpdateEmpleoImagenComprobantePago>();
            var fileGetter = Mock.Of<IFileGetter>();

            Mock.Get(fileGetter).Setup(x => x.existsFile(
                Moq.It.IsAny<string>(), _imageId.ToString(), Moq.It.IsAny<string>()

                )).Returns(false);

          

            _browser = new Browser(
               x =>
               {
                   x.Module<EmpleoModuleUpdateImagenComprobantePago>();
                   x.Dependencies(command, fileGetter);
               }


               );

        };

        private Because of = () =>
        {
            _response =
                _browser.PutSecureJson(
                    "/enterprise/empleos/" + _empleoId + "/Comprobante/" + _comprobanteId + "/Imagen/" + _imageId, "");
        };

        It should_return_error = () => _response.StatusCode.ShouldEqual(HttpStatusCode.BadRequest);
    }
}