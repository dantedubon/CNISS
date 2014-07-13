using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.AutenticationDomain.Domain.Services;
using CNISS.CommonDomain.Ports.Input.REST.Modules;
using CNISS.CommonDomain.Ports.Input.REST.Request.UserRequest;
using Machine.Specifications;
using Moq;
using Nancy;
using Nancy.Authentication.Token;
using Nancy.Testing;
using Should;
using It = Machine.Specifications.It;

namespace CNISS_Tests.Autentication_Test.User_Test.Modules
{
    [Subject(typeof (AuthModule))]
    public class when_UserAuthenticateWithBadUserInfo_Should_ReturnError
    {
        private static Browser _browser;
        private static BrowserResponse _browserResponse;
        private static UserRequest _userRequest;

        private Establish context = () =>
        {
            _userRequest = new UserRequest();

            var tokenizer = Mock.Of<ITokenizer>();
            var authenticator = Mock.Of<IAuthenticateUser>();

            _browser = new Browser(x =>
            {
                x.Module<AuthModule>();
                x.Dependencies(tokenizer, authenticator);
            });
        };

        private Because of = () => { _browserResponse = _browser.PostSecureJson("/auth/movil/", _userRequest); };

        It should_return_error = () => _browserResponse.StatusCode.ShouldEqual(HttpStatusCode.Unauthorized);
    }
}