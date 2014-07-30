using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.AutenticationDomain.Domain.Services;
using CNISS.CommonDomain.Ports;
using CNISS.CommonDomain.Ports.Input.REST;
using CNISS.CommonDomain.Ports.Input.REST.Modules.EmpleoModule.Commands;
using CNISS.CommonDomain.Ports.Input.REST.Request;
using CNISS.CommonDomain.Ports.Input.REST.Request.AuditoriaRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.EmpleoRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.EmpresaRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.MotivoDespidoRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.UserRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.VisitaRequest;
using CNISS.EnterpriseDomain.Application;
using Machine.Specifications;
using Moq;
using Nancy;
using Nancy.Authentication.Token;
using Nancy.Testing;
using Should;
using It = Machine.Specifications.It;

namespace CNISS_Tests.Enterprise_Test.Entities_Test.Empleo_Test.Module
{
    [Subject(typeof (NotaDespidoModuleInsert))]
    public class when_UserPostNotaDespidoDataInvalid_Should_ReturnError
    {
        private static Browser _browser;
        private static BrowserResponse _response;
        private static NotaDespidoRequest _notaDespidoRequest;
        private static MovilRequest _dummyMovilRequest;

        private Establish context = () =>
        {
            _notaDespidoRequest = new NotaDespidoRequest();
            var userIdentityMovil = new DummyUserIdentityMovil("DRCD");


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

            var command = Mock.Of<ICommandInsertNotaDespido>();
            var fileGetter = Mock.Of<IFileGetter>();

            Mock.Get(serializerJson)
               .Setup(x => x.fromJson<NotaDespidoRequest>(Moq.It.IsAny<string>()))
               .Returns(_notaDespidoRequest);

            _browser = new Browser(x =>
            {
                x.Module<NotaDespidoModuleInsert>();
                x.MappedDependencies(new[]
                                     {
                                         new Tuple<Type, object>(typeof(ISerializeJsonRequest),serializerJson),
                                         new Tuple<Type, object>(typeof(Func<string,IEncrytRequestProvider>),encryptRequestProvider),
                                        new Tuple<Type, object>(typeof(ITokenizer),tokenizer),
                                        new Tuple<Type, object>(typeof(ICommandInsertNotaDespido),command),
                                        new Tuple<Type, object>(typeof(IFileGetter),fileGetter)
                                       
                                     });
            });



        };

        private Because of = () => { _response = _browser.PostSecureJson("/movil/notaDespido", _dummyMovilRequest); };

        It should_return_error = () => _response.StatusCode.ShouldEqual(HttpStatusCode.BadRequest);


        private static Func<string, IEncrytRequestProvider> getEncrypter()
        {
            var x = new Func<string, IEncrytRequestProvider>(z => new DummyEncrytRequestProvider());
            return x;
        }
       
    }
}