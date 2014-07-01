using CNISS.CommonDomain.Application;
using CNISS.CommonDomain.Ports.Input.REST;
using CNISS.CommonDomain.Ports.Input.REST.Modules.EmpresaModule.Commands;
using CNISS.CommonDomain.Ports.Input.REST.Request.EmpleoRequest;
using CNISS.EnterpriseDomain.Domain.Entities;
using Machine.Specifications;
using Moq;
using Nancy;
using Nancy.Testing;
using Should;
using It = Machine.Specifications.It;

namespace CNISS_Tests.Enterprise_Test.Entities_Test.Empresas_Test.Module
{
    [Subject(typeof(EmpresaModuleUpdate))]
    public class when_UserPutEmpresaWithInvalidData_Should_Return_Error
    {

        static Browser _browser;
        static BrowserResponse _response;
        static ICommandUpdateIdentity<Empresa> _commandInsert;
        static EmpleoRequest _request;
        private Establish context = () =>
        {
            _request = new EmpleoRequest();
            _commandInsert = Mock.Of<ICommandUpdateIdentity<Empresa>>();
            var fileGetter = Mock.Of<IFileGetter>();

            _browser = new Browser(
                x =>
                {
                    x.Module<EmpresaModuleUpdate>();
                    x.Dependencies(_commandInsert, fileGetter);
                }
                );

        };

        private Because of = () => { _response = _browser.PutSecureJson("enterprise/", _request); };

        It should_return_error = () => _response.StatusCode.ShouldEqual(HttpStatusCode.BadRequest);
    }
}