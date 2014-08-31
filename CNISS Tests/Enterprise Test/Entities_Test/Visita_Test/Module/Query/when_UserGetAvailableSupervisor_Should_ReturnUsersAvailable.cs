using System;
using System.Collections.Generic;
using System.Linq;
using CNISS.AutenticationDomain.Domain.Entities;
using CNISS.AutenticationDomain.Domain.ValueObjects;
using CNISS.CommonDomain.Domain;
using CNISS.CommonDomain.Ports.Input.REST.Modules.VisitaModule.Query;
using CNISS.CommonDomain.Ports.Input.REST.Request.AuditoriaRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.RolModule;
using CNISS.CommonDomain.Ports.Input.REST.Request.UserRequest;
using CNISS.EnterpriseDomain.Domain.Repositories;
using FizzWare.NBuilder;
using FluentAssertions;
using Machine.Specifications;
using Moq;
using Nancy.Testing;
using It = Machine.Specifications.It;

namespace CNISS_Tests.Entities_Test.Visita_Test.Module.Query
{
    [Subject(typeof (SupervisorAvailableModuleQuery))]
    public class when_UserGetAvailableSupervisor_Should_ReturnUsersAvailable
    {
        static Browser _browser;
        static IEnumerable<UserRequest> _expectedUserResponse;
         static IEnumerable<UserRequest> _userResponse;
        static IVisitaRepositoryReadOnly _repositoryReadOnly;
        static BrowserResponse _response;
        private static DateTime _fechaInicial;
        private static DateTime _fechaFinal;

        private Establish context = () =>
        {
            _fechaInicial = new DateTime(2014,5,1);
            _fechaFinal = new DateTime(2014,5,30);
             var _usersRol = Builder<Rol>.CreateNew().With(x => x.Auditoria = Builder<Auditoria>.CreateNew().Build()).Build();
             var usersCollection = Builder<User>.CreateListOfSize(10).All().With(x => x.UserRol = _usersRol)
                 .With(x => x.Auditoria = new Auditoria("",DateTime.Now.Date,"",DateTime.Now.Date))
                 .Build();

            _repositoryReadOnly = Mock.Of<IVisitaRepositoryReadOnly>();
            Mock.Get(_repositoryReadOnly)
                .Setup(x => x.usuariosSinVisitaAgendada(_fechaInicial, _fechaFinal))
                .Returns(usersCollection);

            _expectedUserResponse = convertToRequest(usersCollection);

            _browser = new Browser(x =>
            {
                x.Module<SupervisorAvailableModuleQuery>();
                x.Dependencies(_repositoryReadOnly);

            });




        };

        private Because of = () =>
        {
            _userResponse =
                _browser.GetSecureJson("/visita/supervisores/available/" + _fechaInicial.ToString("yyyy-MM-dd") + "/" + _fechaFinal.ToString("yyyy-MM-dd"))
                    .Body.DeserializeJson<IEnumerable<UserRequest>>();
        };

        It should_return_usersAvailable = () => _userResponse.ShouldBeEquivalentTo(_expectedUserResponse);


        private static IEnumerable<UserRequest> convertToRequest(IEnumerable<User> users)
        {
            return users.Select(x => new UserRequest
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