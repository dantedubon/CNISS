using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.AutenticationDomain.Domain.Entities;
using CNISS.AutenticationDomain.Domain.Services;
using CNISS.AutenticationDomain.Domain.ValueObjects;
using CNISS.CommonDomain.Application;
using CNISS.CommonDomain.Ports;
using CNISS.CommonDomain.Ports.Input.REST;
using CNISS.CommonDomain.Ports.Input.REST.Modules.EmpleoModule.Commands;
using CNISS.CommonDomain.Ports.Input.REST.Request;
using CNISS.CommonDomain.Ports.Input.REST.Request.EmpleoRequest;
using CNISS.EnterpriseDomain.Application;
using CNISS.EnterpriseDomain.Domain.Entities;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using Machine.Specifications;
using Moq;
using Nancy;
using Nancy.Authentication.Token;
using Nancy.Testing;
using Should;
using It = Machine.Specifications.It;

namespace CNISS_Tests.Enterprise_Test.Entities_Test.Empleo_Test.Module
{
    [Subject(typeof (FichaEmpleoSupervisionModuleInsert))]
    public class when_UserPostFichaDeEmpleoDataInvalid_Should_ReturnError
    {
        private static Browser _browser;
        private static BrowserResponse _browserResponse;
        private static FichaSupervisionEmpleoRequest _fichaSupervisionEmpleoRequest;
        private static MovilRequest _dummyMovilRequest;

        private Establish context = () =>
        {
            var userIdentityMovil = new DummyUserIdentityMovil("DRCD");
            _fichaSupervisionEmpleoRequest = new FichaSupervisionEmpleoRequest();

            _dummyMovilRequest = new MovilRequest()
            {
                token = "token",
                data = "data"
            };
            var tokenizer = Mock.Of<ITokenizer>();
            Mock.Get(tokenizer)
                .Setup(x => x.Detokenize(Moq.It.IsAny<string>(), Moq.It.IsAny<NancyContext>()))
                .Returns(userIdentityMovil);
            var serializerJson = Mock.Of<ISerializeJsonRequest>();
            var encryptRequestProvider = getEncrypter();

            Mock.Get(serializerJson)
               .Setup(x => x.fromJson<FichaSupervisionEmpleoRequest>(Moq.It.IsAny<string>()))
               .Returns(_fichaSupervisionEmpleoRequest);

            var command = Mock.Of<ICommandInsertFichaDeSupervision>();
            var fileGetter = Mock.Of<IFileGetter>();
            _browser = new Browser(x =>
            {
                x.Module<FichaEmpleoSupervisionModuleInsert>();
                x.MappedDependencies(new[]
                                     {
                                         new Tuple<Type, object>(typeof(ISerializeJsonRequest),serializerJson),
                                         new Tuple<Type, object>(typeof(Func<string,IEncrytRequestProvider>),encryptRequestProvider),
                                        new Tuple<Type, object>(typeof(ITokenizer),tokenizer),
                                        new Tuple<Type, object>(typeof(ICommandInsertFichaDeSupervision),command),
                                        new Tuple<Type, object>(typeof(IFileGetter),fileGetter)
                                       
                                     });
                
            });


        };

        private Because of = () =>
        {
            _browserResponse = _browser.PostSecureJson("/movil/fichaSupervision/", _fichaSupervisionEmpleoRequest);
        };

        It should_return_error = () => _browserResponse.StatusCode.ShouldEqual(HttpStatusCode.BadRequest);

        private static Func<string, IEncrytRequestProvider> getEncrypter()
        {
            var x = new Func<string, IEncrytRequestProvider>(z => new DummyEncrytRequestProvider());
            return x;
        }
       
    }
}