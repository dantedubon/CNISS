using CNISS.CommonDomain.Ports.Input.REST.Modules.EmpleoModule.Query;
using CNISS.CommonDomain.Ports.Input.REST.Request.GremioRequest;
using CNISS.EnterpriseDomain.Domain.Repositories;
using Machine.Specifications;
using Moq;
using Nancy;
using Nancy.Testing;
using Should;
using It = Machine.Specifications.It;

namespace CNISS_Tests.Enterprise_Test.Entities_Test.Empleo_Test.Module
{
    [Subject(typeof(EmpleoModuleQuery))]
    public class when_UserGetEmpleoByEmpresaBadRTN_returnError
    {
        static Browser _browser;
        static BrowserResponse _response;
        private static IEmpleoRepositoryReadOnly _repositoryRead;
        private static RTNRequest _rtnRequest;

        private Establish context = () =>
        {
            _rtnRequest = new RTNRequest(){RTN = ""};
            _repositoryRead = Mock.Of<IEmpleoRepositoryReadOnly>();


            _browser = new Browser(
                x =>
                {
                    x.Module<EmpleoModuleQuery>();
                    x.Dependencies(_repositoryRead);
                }

                );


        };


        private Because of = () =>
        {
            _response = _browser.GetSecureJson("/enterprise/empleos/empresa/id=" + _rtnRequest.RTN);
        };

        It should_return_empleo = () => { _response.StatusCode.ShouldEqual(HttpStatusCode.BadRequest); };
    }
}