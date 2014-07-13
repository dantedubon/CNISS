using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.AutenticationDomain.Domain.Entities;
using CNISS.AutenticationDomain.Domain.Services;
using CNISS.AutenticationDomain.Domain.ValueObjects;
using CNISS.CommonDomain.Ports.Input.REST.Modules;
using CNISS.CommonDomain.Ports.Input.REST.Request.UserRequest;
using Machine.Specifications;
using Machine.Specifications.Factories;
using Moq;
using Nancy;
using Nancy.Authentication.Token;
using Nancy.Security;
using Nancy.Testing;
using Should;
using It = Machine.Specifications.It;

namespace CNISS_Tests.Autentication_Test.User_Test.Modules
{
    [Subject(typeof(AuthModule))]
    public class when_UserAuthenticateUserValid_Should_ReturnToken
    {
        private static Browser _browser;
        private static BrowserResponse _browserResponse;
        private static UserRequest _userRequest;
        private static string _expectedToken;
        private static string _returnToken;
        

        private Establish context = () =>
        {
            _userRequest = new UserRequest()
            {
                Id = "Usuario",
                password = "Password"
            };
            _expectedToken = "Token";
            var tokenizer = Mock.Of<ITokenizer>();
           
            var authenticator = Mock.Of<IAuthenticateUser>();
            var user = new User(_userRequest.Id, _userRequest.firstName, _userRequest.secondName, _userRequest.password,
                _userRequest.mail, new RolNull());
           
            Mock.Get(authenticator).Setup(x => x.isValidUser(user, 1)).Returns(true);
            Mock.Get(tokenizer)
                .Setup(x => x.Tokenize(Moq.It.IsAny<IUserIdentity>(),Moq.It.IsAny<NancyContext>() ))
                .Returns(_expectedToken);
            _browser = new Browser(x =>
            {
                x.Module<AuthModule>();
                x.Dependencies(tokenizer, authenticator);
            });
        };

        private Because of = () =>
        {
          _returnToken= _browser.PostSecureJson("/auth/movil/", _userRequest).Body.DeserializeJson<TokenMock>().token;
       
        };

        private It should_return_Token = () => _returnToken.ShouldEqual(_expectedToken);

        public class TokenMock 
        {
            public string token { get; set; } 
        }
    }
}