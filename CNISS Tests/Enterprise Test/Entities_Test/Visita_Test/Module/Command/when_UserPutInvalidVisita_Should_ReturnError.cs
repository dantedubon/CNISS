using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Application;
using CNISS.CommonDomain.Ports.Input.REST.Modules.VisitaModule.Command;
using CNISS.CommonDomain.Ports.Input.REST.Request.VisitaRequest;
using CNISS.EnterpriseDomain.Domain.Entities;
using FluentAssertions;
using Machine.Specifications;
using Moq;
using Nancy;
using Nancy.Testing;
using It = Machine.Specifications.It;

namespace CNISS_Tests.Enterprise_Test.Entities_Test.Visita_Test.Module.Command
{
    [Subject(typeof (VisitaModuleUpdate))]
    public class when_UserPutInvalidVisita_Should_ReturnError
    {
        private static Browser _browser;
        private static BrowserResponse _response;
        private static VisitaRequest _request;

        private Establish context = () =>
        {
            _request = new VisitaRequest();

            var command = Mock.Of<ICommandUpdateIdentity<Visita>>();
            _browser = new Browser(x =>
            {
                x.Module<VisitaModuleUpdate>();
                x.Dependencies(command);
            });

        };

        private Because of = () => { _response = _browser.PutSecureJson("/visita", _request); };

        It should_return_error = () => _response.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.BadRequest);
    }
}