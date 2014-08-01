using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.AutenticationDomain.Domain.Services;
using CNISS.CommonDomain.Ports;
using CNISS.CommonDomain.Ports.Input.REST.Modules.EmpleoModule.Query;
using CNISS.EnterpriseDomain.Domain.Repositories;
using FluentAssertions;
using Machine.Specifications;
using Moq;
using Nancy;
using Nancy.Authentication.Token;
using Nancy.Testing;
using Should;
using It = Machine.Specifications.It;

namespace CNISS_Tests.Enterprise_Test.Entities_Test.Empleo_Test.Module
{
    [Subject(typeof (EmpleoModuleQuery))]
    public class when_UserMovilGetEmpleosByBeneficiarioBadID_Should_ReturnError
    {
        private static Browser _browser;
        private static BrowserResponse _browserResponse;
        private static string _badId;
        private static string _RTN;
        private static Guid _sucursalGuid;
        private Establish context = () =>
        {
            var userIdentityMovil = new DummyUserIdentityMovil("DRCD");
            _badId = "08011955";
            _RTN = "08011985123960";
            _sucursalGuid = Guid.NewGuid();

            var tokenizer = Mock.Of<ITokenizer>();
            Mock.Get(tokenizer)
                .Setup(x => x.Detokenize(Moq.It.IsAny<string>(), Moq.It.IsAny<NancyContext>()))
                .Returns(userIdentityMovil);

            var encryptRequestProvider = getEncrypter();

            var repository = Mock.Of<IEmpleoRepositoryReadOnly>();

            _browser = new Browser(x =>
            {
                x.Module<EmpleoModuleQueryMovil>();
                x.MappedDependencies(new[]
                                    {
                                        new Tuple<Type, object>(typeof (ISerializeJsonRequest), new SerializerRequest()),
                                        new Tuple<Type, object>(typeof (Func<string, IEncrytRequestProvider>),
                                            encryptRequestProvider),
                                        new Tuple<Type, object>(typeof (ITokenizer), tokenizer),
                                        new Tuple<Type, object>(typeof(IEmpleoRepositoryReadOnly),repository)
                                    });
            });
        };

        private Because of = () =>
        {
            _browserResponse =
                _browser.GetSecureJsonWithQueryString("/movil/empleo/id=" + _badId + "/rtn=" + _RTN + "/sucursal=" +
                                       _sucursalGuid, null, "token", "123");
        };

        It should_return_error = () => _browserResponse.StatusCode.ShouldEqual(HttpStatusCode.BadRequest);

        private static Func<string, IEncrytRequestProvider> getEncrypter()
        {
            var x = new Func<string, IEncrytRequestProvider>(z => new DummyEncrytRequestProvider());
            return x;
        }
    }
}