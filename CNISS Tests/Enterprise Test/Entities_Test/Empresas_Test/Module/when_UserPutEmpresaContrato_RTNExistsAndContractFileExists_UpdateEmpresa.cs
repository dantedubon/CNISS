using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Ports.Input.REST;
using CNISS.CommonDomain.Ports.Input.REST.Modules.EmpresaModule.Commands;
using CNISS.EnterpriseDomain.Application;
using CNISS.EnterpriseDomain.Domain;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using Machine.Specifications;
using Moq;
using Nancy;
using Nancy.Testing;
using Should;
using It = Machine.Specifications.It;

namespace CNISS_Tests.Enterprise_Test.Entities_Test.Empresas_Test.Module
{
    [Subject(typeof(EmpresaModuleUpdateContrato))]
    public class when_UserPutEmpresaContrato_RTNExistsAndContractFileExists_UpdateEmpresa
    {
        private static string _rtnEmpresa;
        private static Guid _contratoId;
        private static Browser _browser;
        private static BrowserResponse _response;
        private static ContentFile _contentFile;
        private static ICommandUpdateEmpresaContrato _command;

        private Establish context = () =>
        {
            var data = new byte[] {1, 1, 1};
            _contentFile = new ContentFile(data);
            _rtnEmpresa = "08011985123960";
            _contratoId = Guid.NewGuid();

            _command = Mock.Of<ICommandUpdateEmpresaContrato>();
            var fileGetter = Mock.Of<IFileGetter>();

            Mock.Get(fileGetter)
                .Setup(x => x.existsFile(Moq.It.IsAny<string>(), _contratoId.ToString(), Moq.It.IsAny<string>())).Returns(true);
            Mock.Get(fileGetter)
                .Setup(x => x.getFile(Moq.It.IsAny<string>(), _contratoId.ToString(), Moq.It.IsAny<string>())).Returns(data);


            Mock.Get(_command)
                .Setup(x => x.isExecutable(new RTN(_rtnEmpresa))).Returns(true);

            _browser = new Browser(x =>
            {
                x.Module<EmpresaModuleUpdateContrato>();
                x.Dependencies(_command, fileGetter);
            });




        };

        private Because of = () => { _response = _browser.PutSecureJson("/enterprise/" + _rtnEmpresa + "/contract/" + _contratoId, ""); };

        private It should_update_empresa =
            () =>
                Mock.Get(_command)
                    .Verify(
                        x =>
                            x.execute(new RTN(_rtnEmpresa),
                                Moq.It.Is<ContentFile>(z => z.dataFile == _contentFile.dataFile)));
    }
}