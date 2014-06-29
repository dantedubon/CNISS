using System;
using CNISS.CommonDomain.Ports.Input.REST.Modules.EmpleoModule.Query;
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
    public class when_UserGetEmpleoByIdBadEmpty_returnError
    {
        static Browser _browser;
        static BrowserResponse _response;
        private static IEmpleoRepositoryReadOnly _repositoryRead;
        private static Guid _idRequest;

        private Establish context = () =>
        {

            _repositoryRead = Mock.Of<IEmpleoRepositoryReadOnly>();
            _idRequest = Guid.Empty;
            _browser = new Browser(
                x =>
                {
                    x.Module<EmpleoModuleQuery>();
                    x.Dependencies(_repositoryRead);
                }

                );


        };

        private Because of = () => { _response = _browser.GetSecureJson("/enterprise/empleos/id=" + _idRequest); };

        It should_return_empleo = () => { _response.StatusCode.ShouldEqual(HttpStatusCode.BadRequest); };
    }
}