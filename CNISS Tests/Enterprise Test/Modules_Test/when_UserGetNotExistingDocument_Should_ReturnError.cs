using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Ports.Input.REST.Modules.EnterpriseServiceModule;
using CNISS.EnterpriseDomain.Domain.Repositories;
using Machine.Specifications;
using Moq;
using Nancy;
using Nancy.Testing;
using Should;
using It = Machine.Specifications.It;

namespace CNISS_Tests.Enterprise_Test.Modules_Test
{
    [Subject(typeof(DocumentsQueryModule))]
    public class when_UserGetNotExistingDocument_Should_ReturnError
    {
        private static Browser _browser;
        private static BrowserResponse _response;
        private static Guid idDocument;
        private Establish context = () =>
        {

            idDocument = Guid.NewGuid();
            var repository = Mock.Of<IContentFileRepositoryReadOnly>();
           // Mock.Get(repository).Setup(x => x.get(idDocument)).Returns(null);

            _browser = new Browser(
                x =>
                {
                    x.Module<DocumentsQueryModule>();
                    x.Dependencies(repository);
                }
                
                );


        };

        private Because of = () =>
        {
            _response = _browser.Get("/enterprise/Documents/" + idDocument, (with) => with.HttpRequest());
        };

        It should_return_error = () => _response.StatusCode.ShouldEqual(HttpStatusCode.NotFound);
    }
}