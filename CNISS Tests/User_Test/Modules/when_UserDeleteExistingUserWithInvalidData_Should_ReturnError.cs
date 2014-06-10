using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.AutenticationDomain.Domain.Entities;
using CNISS.AutenticationDomain.Domain.Repositories;
using CNISS.CommonDomain.Application;
using CNISS.CommonDomain.Ports.Input.REST.Modules.UserModule.UserCommands;
using CNISS.CommonDomain.Ports.Input.REST.Request.RolModule;
using CNISS.CommonDomain.Ports.Input.REST.Request.UserRequest;
using FizzWare.NBuilder;
using Machine.Specifications;
using Moq;
using Nancy;
using Nancy.Testing;
using Should;
using It = Machine.Specifications.It;

namespace CNISS_Tests.User_Test.Modules
{
    [Subject(typeof (UserModuleCommandDelete))]
    public class when_UserDeleteExistingUserWithInvalidData_Should_ReturnError
    {
        static Browser _browser;
        static BrowserResponse _response;
        static ICommandDeleteIdentity<User> _commandDelete;
        static UserRequest _userRequest;
        static IUserRepositoryReadOnly _repositoryRead;
        static RolRequest _userRol;


        Establish context = () =>
        {

            _userRequest = Builder<UserRequest>.CreateNew().Build();
            _userRol = Builder<RolRequest>.CreateNew().Build();
            _userRequest.userRol = _userRol;

            _userRequest.Id = "";

            _commandDelete = Mock.Of<ICommandDeleteIdentity<User>>();
            _repositoryRead = Mock.Of<IUserRepositoryReadOnly>();
            Mock.Get(_repositoryRead).Setup(x => x.exists(Moq.It.IsAny<string>())).Returns(true);

            _browser = new Browser(
                x =>
                {
                    x.Module<UserModuleCommandDelete>();
                    x.Dependencies(_repositoryRead, _commandDelete);

                }
                );

        };

        Because of = () => _response = _browser.DeleteSecureJson("/user", _userRequest);

        It should_return_error = () => _response.StatusCode.ShouldEqual(HttpStatusCode.NotAcceptable);
    }
}