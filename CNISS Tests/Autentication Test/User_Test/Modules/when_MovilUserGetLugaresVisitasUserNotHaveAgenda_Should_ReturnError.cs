using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.AutenticationDomain.Domain.Entities;
using CNISS.AutenticationDomain.Domain.Services;
using CNISS.AutenticationDomain.Domain.ValueObjects;
using CNISS.CommonDomain.Ports;
using CNISS.CommonDomain.Ports.Input.REST.Modules.VisitaModule.Query;
using CNISS.CommonDomain.Ports.Input.REST.Request;
using CNISS.CommonDomain.Ports.Input.REST.Request.VisitaRequest;
using CNISS.EnterpriseDomain.Domain.Repositories;
using FluentAssertions;
using Machine.Specifications;
using Moq;
using Nancy;
using Nancy.Authentication.Token;
using Nancy.Testing;
using It = Machine.Specifications.It;

namespace CNISS_Tests.Autentication_Test.User_Test.Modules
{
    [Subject(typeof (SupervisorLugaresVisitaModuleQuery))]
    public class when_MovilUserGetLugaresVisitasUserNotHaveAgenda_Should_ReturnError
    {
        private static Browser _browser;
        private static BrowserResponse _response;
        private static MovilRequest _dummyMovilRequest;

        private Establish context = () =>
        {

            var userIdentityMovil = new DummyUserIdentityMovil("DRCD");

            var repository = Mock.Of<IVisitaRepositoryReadOnly>();

            var user = new User("DRCD", "Dante", "Ruben", "XXXX", "XXXX", new RolNull());

            _dummyMovilRequest = new MovilRequest()
            {
                token = "token",

            };

            var tokenizer = Mock.Of<ITokenizer>();
            Mock.Get(tokenizer)
                .Setup(x => x.Detokenize(Moq.It.IsAny<string>(), Moq.It.IsAny<NancyContext>()))
                .Returns(userIdentityMovil);

            var encryptRequestProvider = getEncrypter();

            _browser = new Browser(x =>
            {
                x.Module<SupervisorLugaresVisitaModuleQuery>();

                x.MappedDependencies(new[]
                                    {
                                        new Tuple<Type, object>(typeof (ISerializeJsonRequest), new SerializerRequest()),
                                        new Tuple<Type, object>(typeof (Func<string, IEncrytRequestProvider>),
                                            encryptRequestProvider),
                                        new Tuple<Type, object>(typeof (ITokenizer), tokenizer),
                                        new Tuple<Type, object>(typeof(IVisitaRepositoryReadOnly),repository)
                                    });


            });
        };

        private Because of = () =>
        {
            _response = _browser.PostSecureJson("/movil/supervisor/lugaresVisita",_dummyMovilRequest);


        };

         It should_return_error = () => _response.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.NotFound);

         private static Func<string, IEncrytRequestProvider> getEncrypter()
         {
             var x = new Func<string, IEncrytRequestProvider>(z => new DummyEncrytRequestProvider());
             return x;
         }
    }
}