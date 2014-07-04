using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Ports.Input.REST;
using CNISS.CommonDomain.Ports.Input.REST.Modules.EmpresaModule.Commands;
using CNISS.EnterpriseDomain.Application;
using CNISS.EnterpriseDomain.Domain;
using Machine.Specifications;
using Moq;
using Nancy;
using Nancy.Testing;
using Should;
using It = Machine.Specifications.It;

namespace CNISS_Tests.Enterprise_Test.Entities_Test.Empresas_Test.Module
{
    [Subject(typeof (EmpresaModuleUpdateContrato))]
    public class when_UserPutEmpresaContrato_ContratoNotExists_ReturnError
    {
        private static string _rtnEmpresa;
        private static Guid _contratoId;
        private static Browser _browser;
        private static BrowserResponse _response;

        private Establish context = () =>
        {
            _rtnEmpresa = "08011985123960";
            _contratoId = Guid.NewGuid();

            var command = Mock.Of<ICommandUpdateEmpresaContrato>();
            var fileGetter = Mock.Of<IFileGetter>();

            Mock.Get(fileGetter)
                .Setup(x => x.existsFile(Moq.It.IsAny<string>(), _contratoId.ToString(), Moq.It.IsAny<string>())).Returns(false);

            _browser = new Browser( x =>
            {
                x.Module<EmpresaModuleUpdateContrato>();
                x.Dependencies(command, fileGetter);
            });


        };

        private Because of = () => { _response = _browser.PutSecureJson("/enterprise/"+_rtnEmpresa+"/contract/"+_contratoId, ""); };

        It should_return_error = () => _response.StatusCode.ShouldEqual(HttpStatusCode.BadRequest);
    }
}