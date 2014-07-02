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
    [Subject(typeof (UserModuleCommandInsert))]
    public class when_UserPostNonExistingUserWithValidData_CreateNewUser
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
            _userRequest.auditoriaRequest = Builder<AuditoriaRequest>.CreateNew().Build();
            _userRequest.userRol = _userRol;
    
            _commandInsert = Mock.Of<ICommandInsertIdentity<User>>();
            _repositoryRead = Mock.Of<IUserRepositoryReadOnly>();
            _browser = new Browser(
                x =>
                {
                    x.Module<UserModuleCommandInsert>();
                    x.Dependencies(_repositoryRead,_commandInsert);

                }
                );

        };

        Because of = () => _browser.PostSecureJson("/user", _userRequest);

        It should_insertNewUser = () => Mock.Get(_commandInsert).Verify(x => x.execute(Moq.It.Is<User>(z => z.Id == _userRequest.Id)));
    }
}