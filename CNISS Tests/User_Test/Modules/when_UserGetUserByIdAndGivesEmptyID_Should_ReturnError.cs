using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.AutenticationDomain.Domain.Entities;
using CNISS.AutenticationDomain.Domain.Repositories;
using CNISS.AutenticationDomain.Domain.ValueObjects;
using CNISS.CommonDomain.Ports.Input.REST.Modules.UserModule.UserQuery;
using CNISS.CommonDomain.Ports.Input.REST.Request.RolModule;
using CNISS.CommonDomain.Ports.Input.REST.Request.UserRequest;
using FizzWare.NBuilder;
using Machine.Specifications;
using Nancy;
using Nancy.Testing;
using Should;

namespace CNISS_Tests.User_Test.Modules
{
    [Subject(typeof (UserModuleQuery))]
    public class when_UserGetUserByIdAndGivesEmptyID_Should_ReturnError
    {
        static Browser _browser;
        static UserRequest _expectedUserResponse;
        static IUserRepositoryReadOnly _repositoryReadOnly;
        static BrowserResponse _response;
        static UserRequest _userResponse;

        Establish context = () =>
        {
           
            

            var userRepository = new DummyRepository(null);

            

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

             _response = _browser.GetSecureJson("/user/id=" + "");
         };

        It should_return_error = () => _response.StatusCode.ShouldEqual(HttpStatusCode.NotAcceptable);
    }
}