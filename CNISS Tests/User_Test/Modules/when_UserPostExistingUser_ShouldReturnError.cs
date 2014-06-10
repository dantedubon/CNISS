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
    [Subject(typeof(UserModuleCommandInsert))]
    public class when_UserPostExistingUser_ShouldReturnError
    {

        static Browser _browser;
        static BrowserResponse _response;
        static ICommandInsertIdentity<User> _commandInsert;
        static UserRequest _userRequest;
        static IUserRepositoryReadOnly _repositoryRead;
        static RolRequest _userRol;


        Establish context = () =>
        {

            _userRequest = Builder<UserRequest>.CreateNew().Build();
            _userRol = Builder<RolRequest>.CreateNew().Build();
            _userRequest.userRol = _userRol;

            _commandInsert = Mock.Of<ICommandInsertIdentity<User>>();
            _repositoryRead = Mock.Of<IUserRepositoryReadOnly>();
            Mock.Get(_repositoryRead).Setup(x => x.exists(Moq.It.IsAny<string>())).Returns(true);

            _browser = new Browser(
                x =>
                {
                    x.Module<UserModuleCommandInsert>();
                    x.Dependencies(_repositoryRead, _commandInsert);

                }
                );

        };

        Because of = () => _response =_browser.PostSecureJson("/user", _userRequest);

        It should_return_error = () => _response.StatusCode.ShouldEqual(HttpStatusCode.NotAcceptable);
    }
}