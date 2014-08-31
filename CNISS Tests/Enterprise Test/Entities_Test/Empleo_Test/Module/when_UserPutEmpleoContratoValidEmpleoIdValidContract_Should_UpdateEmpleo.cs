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
    [Subject(typeof(EmpleoModuleUpdateContrato))]
    public class when_UserPutEmpleoContratoValidEmpleoIdValidContract_Should_UpdateEmpleo
    {
        private static Guid _empleoId;
        private static Guid _contratoId;
        private static Browser _browser;
        private static BrowserResponse _response;
        private static ICommandUpdateEmpleoContrato _command;
        private static ContentFile _contentFile;

        private Establish context = () =>
        {
            var _data = new byte[] {0, 1};
            _empleoId = Guid.NewGuid();
            _contratoId = Guid.NewGuid();
            _contentFile = new ContentFile(_data);

            _command = Mock.Of<ICommandUpdateEmpleoContrato>();
            var fileGetter = Mock.Of<IFileGetter>();

            Mock.Get(fileGetter).Setup(x => x.existsFile(
                Moq.It.IsAny<string>(), _contratoId.ToString(), Moq.It.IsAny<string>()

                )).Returns(true);

             Mock.Get(fileGetter).Setup(x => x.getFile(
                Moq.It.IsAny<string>(), _contratoId.ToString(), Moq.It.IsAny<string>()

                )).Returns(_data);
            
            Mock.Get(_command).Setup(x => x.isExecutable(_empleoId)).Returns(true);

            _browser = new Browser(
               x =>
               {
                   x.Module<EmpleoModuleUpdateContrato>();
                   x.Dependencies(_command, fileGetter);
               }


               );

        };

        private Because of = () =>
        {
            _response =
                _browser.PutSecureJson(
                    "/enterprise/empleos/" + _empleoId + "/contract/" + _contratoId, "");
        };

        private It should_return_error = () => Mock.Get(_command)
            .Verify(x => x.execute(_empleoId, Moq.It.Is<ContentFile>(z => z.DataFile.Equals(_contentFile.DataFile))));
    }
}