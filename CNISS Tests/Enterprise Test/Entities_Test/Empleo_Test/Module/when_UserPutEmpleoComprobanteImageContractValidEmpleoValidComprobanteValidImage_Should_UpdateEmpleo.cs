using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Ports.Input.REST;
using CNISS.CommonDomain.Ports.Input.REST.Modules.EmpleoModule.Commands;
using CNISS.EnterpriseDomain.Application;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using Machine.Specifications;
using Moq;
using Nancy;
using Nancy.Testing;
using Should;
using It = Machine.Specifications.It;

namespace CNISS_Tests.Enterprise_Test.Entities_Test.Empleo_Test.Module
{
    [Subject(typeof(EmpleoModuleUpdateImagenComprobantePago))]
    public class when_UserPutEmpleoComprobanteImageContractValidEmpleoValidComprobanteValidImage_Should_UpdateEmpleo
    {
        private static Guid _empleoId;
        private static Guid _imageId;
        private static Guid _comprobanteId;
        private static Browser _browser;
        private static BrowserResponse _response;
        private static ICommandUpdateEmpleoImagenComprobantePago _command;
        private static ContentFile _contentFile;

        private Establish context = () =>
        {
            var data = new byte[] {0, 1};
            _empleoId = Guid.NewGuid();
            _comprobanteId = Guid.NewGuid();
            _imageId = Guid.NewGuid();
            _contentFile = new ContentFile(data);

            _command = Mock.Of<ICommandUpdateEmpleoImagenComprobantePago>();
            var fileGetter = Mock.Of<IFileGetter>();

            Mock.Get(fileGetter).Setup(x => x.existsFile(
                Moq.It.IsAny<string>(), _imageId.ToString(), Moq.It.IsAny<string>()

                )).Returns(true);

               Mock.Get(fileGetter).Setup(x => x.getFile(
                Moq.It.IsAny<string>(), _imageId.ToString(), Moq.It.IsAny<string>()

                )).Returns(data);

            Mock.Get(_command).Setup(x => x.isExecutable(_empleoId, _comprobanteId)).Returns(true);


            _browser = new Browser(
               x =>
               {
                   x.Module<EmpleoModuleUpdateImagenComprobantePago>();
                   x.Dependencies(_command, fileGetter);
               }


               );

        };

        private Because of = () =>
        {
            _response =
                _browser.PutSecureJson(
                    "/enterprise/empleos/" + _empleoId + "/Comprobante/" + _comprobanteId + "/Imagen/" + _imageId, "");
        };

         It should_update_empleo = () => Mock.Get(_command)
            .Verify(
                x =>
                    x.execute(_empleoId, _comprobanteId,
                        Moq.It.Is<ContentFile>(z => z.dataFile == _contentFile.dataFile)));
    }
}