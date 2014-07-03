using CNISS.AutenticationDomain.Domain.Entities;
using CNISS.AutenticationDomain.Domain.Repositories;
using CNISS.CommonDomain.Application;
using CNISS.CommonDomain.Ports.Input.REST.Modules.UserModule.UserCommands;
using CNISS.CommonDomain.Ports.Input.REST.Request.AuditoriaRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.RolModule;
using CNISS.CommonDomain.Ports.Input.REST.Request.UserRequest;
using FizzWare.NBuilder;
using Machine.Specifications;
using Moq;
using Nancy.Testing;
using It = Machine.Specifications.It;

namespace CNISS_Tests.User_Test.Modules
{
    [Subject(typeof (UserModuleCommandUpdate))]
    public class when_UserPutExistingUserWithValidData_Should_UpdateUser
    {
        static Browser _browser;
        static BrowserResponse _response;
        static ICommandUpdateIdentity<User> _commandUpdate;
        static UserRequest _userRequest;
        static IUserRepositoryReadOnly _repositoryRead;
        static RolRequest _userRol;
        Establish context = () =>
        {

            _userRequest = Builder<UserRequest>.CreateNew().With(x => x.auditoriaRequest=Builder<AuditoriaRequest>.CreateNew().Build()).Build();
            _userRol = Builder<RolRequest>.CreateNew().With(x => x.auditoriaRequest = Builder<AuditoriaRequest>.CreateNew().Build()).Build();
            _userRequest.userRol = _userRol;

            _commandUpdate = Mock.Of<ICommandUpdateIdentity<User>>();
            _repositoryRead = Mock.Of<IUserRepositoryReadOnly>();
            Mock.Get(_repositoryRead).Setup(x => x.exists(Moq.It.IsAny<string>())).Returns(true);

            _browser = new Browser(
                x =>
                {
                    x.Module<UserModuleCommandUpdate>();
                    x.Dependencies(_repositoryRead, _commandUpdate);

                }
                );

        };

        Because of = () => _browser.PutSecureJson("/user", _userRequest);
        It should_update_user = () => Mock.Get(_commandUpdate).Verify(x => x.execute(Moq.It.Is<User>(z => z.Id == _userRequest.Id)));
    }
}