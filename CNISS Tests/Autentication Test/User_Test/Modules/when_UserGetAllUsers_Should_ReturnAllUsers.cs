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
             var _usersRol = Builder<Rol>.CreateNew().With(x => x.Auditoria = Builder<Auditoria>.CreateNew().Build()).Build();
             _usersCollection = Builder<User>.CreateListOfSize(10).All().With(x => x.UserRol = _usersRol)
                 .With(x => x.Auditoria = new Auditoria("",DateTime.Now.Date,"",DateTime.Now.Date))
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
                firstName = x.FirstName,
                secondName = x.SecondName,
                Id = x.Id,
                mail = x.Mail,
                password = "",
                
                 userRol = new RolRequest
                {
                    description = x.UserRol.Description,
                    name = x.UserRol.Name,
                    idGuid = x.UserRol.Id,
                    nivel = x.UserRol.Nivel,
                    auditoriaRequest = new AuditoriaRequest()
                    {
                        fechaCreo = x.UserRol.Auditoria.FechaCreacion,
                        fechaModifico = x.UserRol.Auditoria.FechaActualizacion,
                        usuarioCreo = x.UserRol.Auditoria.CreadoPor,
                        usuarioModifico = x.UserRol.Auditoria.ActualizadoPor
                    }

                },
                auditoriaRequest = new AuditoriaRequest()
                {
                    fechaCreo = x.Auditoria.FechaCreacion,
                    fechaModifico = x.Auditoria.FechaActualizacion,
                    usuarioCreo = x.Auditoria.CreadoPor,
                    usuarioModifico = x.Auditoria.ActualizadoPor
                }

            }).ToList();
        }
    }
}