using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Ports.Input.REST.Modules.VisitaModule.Query;
using CNISS.EnterpriseDomain.Domain.Repositories;
using FluentAssertions;
using Machine.Specifications;
using Moq;
using Nancy;
using Nancy.Testing;
using It = Machine.Specifications.It;

namespace CNISS_Tests.Enterprise_Test.Entities_Test.Visita_Test.Query
{
    [Subject(typeof(SupervisorAvailableModuleQuery))]
    public class when_UserGetAvailableSupervisorBadDate_Should_Return_error
    {
        static Browser _browser;
        private static DateTime _fechaInicial;
        private static DateTime _fechaFinal;
        static BrowserResponse _response;

        private Establish context = () =>
        {
            _fechaInicial = new DateTime(2014, 6, 1);
            _fechaFinal = new DateTime(2014, 5, 30);

            var repositoryReadOnly = Mock.Of<IVisitaRepositoryReadOnly>();


            _browser = new Browser(x =>
            {
                x.Module<SupervisorAvailableModuleQuery>();
                x.Dependencies(repositoryReadOnly);

            });
        };

        private Because of = () =>
        {
            _response =
                _browser.GetSecureJson("/visita/supervisores/available/" + _fechaInicial.ToString("yyyy-MM-dd") + "/" +
                                       _fechaFinal.ToString("yyyy-MM-dd"));

        };

        It should_return_error = () => _response.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.BadRequest);
    }
}