using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.AutenticationDomain.Domain.Entities;
using CNISS.AutenticationDomain.Domain.Services;
using CNISS.AutenticationDomain.Domain.ValueObjects;
using CNISS.CommonDomain.Ports.Input.REST.Modules;
using CNISS.CommonDomain.Ports.Input.REST.Modules.UserModule;
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
    [Subject(typeof(AuthModule))]
    public class when_UserAuthenticateUserInvalid_Should_ReturnError
    {
        private static Browser _browser;
        private static BrowserResponse _browserResponse;
        private static UserRequest _userRequest;

        private Establish context = () =>
        {
            _userRequest = new UserRequest()
            {
                Id = "Usuario",
                password = "Password"
            };

            var tokenizer = Mock.Of<ITokenizer>();
            var authenticator = Mock.Of<IAuthenticateUser>();
            var user = new User(_userRequest.Id, _userRequest.firstName, _userRequest.secondName, _userRequest.password,
                _userRequest.mail, new RolNull());
            Mock.Get(authenticator).Setup(x => x.isValidUser(user, 2)).Returns(false);

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