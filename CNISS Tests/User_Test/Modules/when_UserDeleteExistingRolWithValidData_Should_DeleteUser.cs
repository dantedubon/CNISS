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
using Nancy.Testing;
using It = Machine.Specifications.It;

namespace CNISS_Tests.User_Test.Modules
{
    [Subject(typeof (UserModuleCommandDelete))]
    public class when_UserDeleteExistingRolWithValidData_Should_DeleteUser
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

        Because of = () => _browser.DeleteSecureJson("/user", _userRequest);

        It should_delete_user = () => Mock.Get(_commandDelete).Verify(x => x.execute(Moq.It.Is<User>(z => z.Id == _userRequest.Id)));
    }
}