using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.AutenticationDomain.Domain.Entities;
using CNISS.AutenticationDomain.Domain.Repositories;
using CNISS.AutenticationDomain.Domain.ValueObjects;
using CNISS.AutenticationDomain.Ports.Output.Database;
using CNISS.CommonDomain.Ports.Input.REST.Modules.UserModule.UserQuery;
using CNISS.CommonDomain.Ports.Input.REST.Request.RolModule;
using CNISS.CommonDomain.Ports.Input.REST.Request.UserRequest;
using FizzWare.NBuilder;
using FluentAssertions;
using Machine.Specifications;
using Moq;
using Nancy.Testing;
using It = Machine.Specifications.It;

namespace CNISS_Tests.User_Test.Modules
{
    [Subject(typeof (UserModuleQuery))]
    public class when_GetUserByID_Should_ReturnUser
    {
        static Browser _browser;
        static UserRequest _expectedUserResponse;
        static IUserRepositoryReadOnly _repositoryReadOnly;
        static BrowserResponse _response;
         static UserRequest _userResponse;

         Establish context = () =>
         {
             var _userRol = Builder<Rol>.CreateNew().Build();
             var _expectedUser = Builder<User>.CreateNew().With(x => x.userRol = _userRol).Build();

             var userRepository = new DummyRepository(_expectedUser);

             _expectedUserResponse = new UserRequest
             {
                 Id = _expectedUser.Id,
                 firstName = _expectedUser.firstName,
                 mail = _expectedUser.mail,
                 password = "",
                 secondName = _expectedUser.secondName,
                 userRol = new RolRequest
                 {
                     description = _expectedUser.userRol.description,
                     idGuid = _expectedUser.userRol.Id,
                     name = _expectedUser.userRol.name

                 }
             };

             _browser = new Browser(
                x =>
                {
                    x.Module<UserModuleQuery>();
                    x.Dependency(userRepository);

                }
            );

         };

        Because of = () =>
        {
            _userResponse = _browser.GetSecureJson("/user/id=" + _expectedUserResponse.Id).Body.DeserializeJson<UserRequest>();
        };

        It should_return_user = () => _userResponse.ShouldBeEquivalentTo(_expectedUserResponse);
    }

    internal class DummyRepository : IUserRepositoryReadOnly
    {
        private readonly User _userToReturn;

        public DummyRepository(User user)
        {
            _userToReturn = user;
        }
        public User get(string id)
        {
            return _userToReturn;
        }

        public IEnumerable<User> getAll()
        {
            throw new NotImplementedException();
        }

        public bool exists(string id)
        {
            throw new NotImplementedException();
        }
    }
}