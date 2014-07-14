using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.AutenticationDomain.Domain.Entities;
using CNISS.AutenticationDomain.Domain.ValueObjects;
using CNISS.CommonDomain.Ports.Input.REST.Modules.VisitaModule.Query;
using CNISS.CommonDomain.Ports.Input.REST.Request.VisitaRequest;
using CNISS.EnterpriseDomain.Domain.Repositories;
using FluentAssertions;
using Machine.Specifications;
using Moq;
using Nancy;
using Nancy.Testing;
using It = Machine.Specifications.It;

namespace CNISS_Tests.Autentication_Test.User_Test.Modules
{
    [Subject(typeof (SupervisorLugaresVisitaModuleQuery))]
    public class when_MovilUserGetLugaresVisitasUserNotHaveAgenda_Should_ReturnError
    {
        private static Browser _browser;
        private static BrowserResponse _response;

        private Establish context = () =>
        {

            var userIdentityMovil = new DummyUserIdentityMovil("DRCD");

            var repository = Mock.Of<IVisitaRepositoryReadOnly>();

            var user = new User("DRCD", "Dante", "Ruben", "XXXX", "XXXX", new RolNull());



            _browser = new Browser(x =>
            {
                x.Module<SupervisorLugaresVisitaModuleQuery>();

                x.Dependencies(repository);
                x.RequestStartup((container, pipelines, context) =>
                {
                    context.CurrentUser = userIdentityMovil;
                });


            });
        };

        private Because of = () =>
        {
            _response = _browser.GetSecureJson("/movil/supervisor/lugaresVisita");


        };

         It should_return_error = () => _response.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.NotFound);
    }
}