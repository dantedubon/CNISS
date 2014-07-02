using System;
using System.Collections.Generic;
using System.Linq;
using CNISS.AutenticationDomain.Domain.Entities;
using CNISS.AutenticationDomain.Domain.Repositories;
using CNISS.AutenticationDomain.Domain.ValueObjects;
using CNISS.CommonDomain.Domain;
using CNISS.CommonDomain.Ports.Input.REST.Modules.UserModule.UserQuery;
using CNISS.CommonDomain.Ports.Input.REST.Request.AuditoriaRequest;
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
    public class when_UserGetAllUsers_Should_ReturnAllUsers
    {
        static Browser _browser;
        static IEnumerable<User> _usersCollection;
       
        static BrowserResponse _response;
        static IEnumerable<UserRequest> _usersResponse;
        static IEnumerable<UserRequest> _usersExpected;

         Establish context = () =>
         {
             var _usersRol = Builder<Rol>.CreateNew().Build();
             _usersCollection = Builder<User>.CreateListOfSize(10).All().With(x => x.userRol = _usersRol)
                 .With(x => x.auditoria = new Auditoria("",DateTime.Now.Date,"",DateTime.Now.Date))
                 .Build();
             

           
             var _userRepositoryReadOnly = Mock.Of<IUserRepositoryReadOnly>();
             Mock.Get(_userRepositoryReadOnly).Setup(x => x.getAll()).Returns(_usersCollection);

             _usersExpected = convertToRequest(_usersCollection);
             _browser = new Browser(
                x =>
                {
                    x.Module<UserModuleQuery>();
                    x.Dependency(_userRepositoryReadOnly);

                }
            );


         };

         Because of = () =>
         {
             _usersResponse = _browser.GetSecureJson("/user").Body.DeserializeJson<IEnumerable<UserRequest>>();
         };

        It should_return_all_users = () => _usersResponse.ShouldAllBeEquivalentTo(_usersExpected);

        private static IEnumerable<UserRequest> convertToRequest(IEnumerable<User> users)
        {
          return   users.Select(x => new UserRequest
            {
                firstName = x.firstName,
                secondName = x.secondName,
                Id = x.Id,
                mail = x.mail,
                password = "",
                
                 userRol = new RolRequest
                {
                    description = x.userRol.description,
                    name = x.userRol.name,
                    idGuid = x.userRol.Id
                },
                auditoriaRequest = new AuditoriaRequest()
                {
                    fechaCreo = x.auditoria.fechaCreo,
                    fechaModifico = x.auditoria.fechaModifico,
                    usuarioCreo = x.auditoria.usuarioCreo,
                    usuarioModifico = x.auditoria.usuarioModifico
                }

            }).ToList();
        }
    }
}