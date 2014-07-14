using System;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Ports.Input.REST.Modules.VisitaModule.Query;
using CNISS.EnterpriseDomain.Domain.Repositories;
using Machine.Specifications;
using Moq;
using Nancy;
using Nancy.Responses.Negotiation;
using Nancy.Testing;
using Should;
using It = Machine.Specifications.It;

namespace CNISS_Tests.Autentication_Test.User_Test.Modules
{
    [Subject(typeof (SupervisorLugaresVisitaModuleQuery))]
    public class when_MovilUserGetLugaresVisitas_Should_ReturnLugaresVisita
    {
        private static Browser _browser;
        private static BrowserResponse _response;
        private static string _x;
        
        private Establish context = () =>
        {

            var user = new DummyUserIdentityMovil("Usuario Movil");

            var repository = Mock.Of<IVisitaRepositoryReadOnly>();

           _browser = new Browser(x =>
           {
               x.Module<SupervisorLugaresVisitaModuleQuery>();
               
               x.Dependencies(repository);
               x.RequestStartup((container, pipelines, context) =>
               {
                   context.CurrentUser = user;
               });


           });
        };

        private Because of = () =>
        {
            _x = _browser.GetSecureJson("/movil/supervisor/lugaresVisita").Body.DeserializeJson<string>();


        };

        It should_return_lugares_visita = () => _x.ShouldEqual("Autenticado");
    }
}