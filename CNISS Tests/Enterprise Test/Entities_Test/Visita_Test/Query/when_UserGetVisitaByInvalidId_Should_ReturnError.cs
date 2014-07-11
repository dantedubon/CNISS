using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Ports.Input.REST.Modules.VisitaModule.Query;
using CNISS.EnterpriseDomain.Domain.Entities;
using CNISS.EnterpriseDomain.Domain.Repositories;
using FluentAssertions;
using Machine.Specifications;
using Moq;
using Nancy;
using Nancy.Testing;
using It = Machine.Specifications.It;

namespace CNISS_Tests.Enterprise_Test.Entities_Test.Visita_Test.Query
{
    [Subject(typeof (VisitaModuleQuery))]
    public class when_UserGetVisitaByInvalidId_Should_ReturnError
    {
        static Browser _browser;
        static BrowserResponse _response;
        private static Guid _invalidId;
        private Establish context = () =>
        {
            _invalidId = Guid.NewGuid();
            var repository = Mock.Of<IVisitaRepositoryReadOnly>();


            _browser = new Browser(
                x =>
                {

                    x.Module<VisitaModuleQuery>();
                    x.Dependencies(repository);
                }
            );
        };

        private Because of = () => { _response = _browser.GetSecureJson("/visita/" + _invalidId); };

        It should_return_error = () => _response.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.NotFound);
    }
}