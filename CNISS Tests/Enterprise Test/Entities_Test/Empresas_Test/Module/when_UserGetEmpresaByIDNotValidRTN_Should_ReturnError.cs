using CNISS.CommonDomain.Ports.Input.REST.Modules.EmpresaModule.Query;
using CNISS.CommonDomain.Ports.Input.REST.Request.GremioRequest;
using CNISS.EnterpriseDomain.Domain.Repositories;
using Machine.Specifications;
using Moq;
using Nancy;
using Nancy.Testing;
using Should;
using It = Machine.Specifications.It;

namespace CNISS_Tests.Enterprise_Test.Entities_Test.Empresas_Test.Module
{
    [Subject(typeof(EmpresaModuleQuery))]
    public class when_UserGetEmpresaByIDNotValidRTN_Should_ReturnError
    {

        static Browser _browser;
        static BrowserResponse _response;
        private static IEmpresaRepositoryReadOnly _repositoryRead;
        private static RTNRequest request;




        private Establish context = () =>
        {   
            request = new RTNRequest() { RTN = "08011985123961" };
            _repositoryRead = Mock.Of<IEmpresaRepositoryReadOnly>();
            var _repositorioGremio = Mock.Of<IGremioRepositoryReadOnly>();
            _browser = new Browser(
                x =>
                {
                    x.Module<EmpresaModuleQuery>();
                    x.Dependencies(_repositoryRead, _repositorioGremio);
                }

                );

        };

        private Because of = () => { _response = _browser.GetSecureJson("/enterprise/id=", request); };

        It should_return_Error = () => _response.StatusCode.ShouldEqual(HttpStatusCode.BadRequest);
    }
}