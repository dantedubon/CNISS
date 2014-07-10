using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.AutenticationDomain.Domain.Entities;
using CNISS.AutenticationDomain.Domain.ValueObjects;
using CNISS.CommonDomain.Application;
using CNISS.CommonDomain.Domain;
using CNISS.CommonDomain.Ports.Input.REST.Modules.VisitaModule.Command;
using CNISS.CommonDomain.Ports.Input.REST.Request.AuditoriaRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.EmpresaRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.GremioRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.RolModule;
using CNISS.CommonDomain.Ports.Input.REST.Request.UserRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.VisitaRequest;
using CNISS.EnterpriseDomain.Domain;
using CNISS.EnterpriseDomain.Domain.Entities;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using FluentAssertions;
using Machine.Specifications;
using Moq;
using Nancy;
using Nancy.Testing;
using It = Machine.Specifications.It;

namespace CNISS_Tests.Enterprise_Test.Entities_Test.Visita_Test.Command
{
    [Subject(typeof(VisitaModuleInsert))]
    public class when_UserPostVisitaValidData_Should_SaveVisita
    {
        private static Browser _browser;
        private static BrowserResponse _response;
        private static VisitaRequest _request;
        private static Visita _expectedVisita;
        private static ICommandInsertIdentity<Visita> _command;


        private Establish context = () =>
        {
            _request = new VisitaRequest()
            {
                auditoriaRequest = getAuditoriaRequest(),
                fechaInicial = new DateTime(2014,8,1),
                fechaFinal = new DateTime(2014,8,30),
                nombre = "Gira de Prueba",
                supervisoresRequests = getSupervisorRequests()
            };

            _command = Mock.Of<ICommandInsertIdentity<Visita>>();

            _expectedVisita = getVisitaRequest(_request);


            _browser = new Browser(x =>
            {
                x.Module<VisitaModuleInsert>();
                x.Dependencies(_command);
            });



        };

        private Because of = () => { _response = _browser.PostSecureJson("/visita", _request); };

        It should_save_visita= () => Mock.Get(_command).Verify(x => x.execute(Moq.It.Is<Visita>(z => z.nombre == _expectedVisita.nombre 
                                                                                                       && z.fechaInicial == _expectedVisita.fechaInicial 
                                                                                                       && z.fechaFinal == _expectedVisita.fechaFinal
                                                                                                       && z.supervisores.FirstOrDefault().usuario.Id == _expectedVisita.supervisores.FirstOrDefault().usuario.Id)));


        private static Visita getVisitaRequest(VisitaRequest visitaRequest)
        {
            var auditoria = new Auditoria(visitaRequest.auditoriaRequest.usuarioCreo,
                visitaRequest.auditoriaRequest.fechaCreo, visitaRequest.auditoriaRequest.usuarioModifico,
                visitaRequest.auditoriaRequest.fechaModifico);

            var supervisoresRequest = visitaRequest.supervisoresRequests;


            return new Visita(visitaRequest.nombre,visitaRequest.fechaInicial,visitaRequest.fechaFinal)
            {
                auditoria = auditoria,
                supervisores = getSupervisores(visitaRequest.supervisoresRequests)
                
            };
        }

        private static IList<Supervisor> getSupervisores(IEnumerable<SupervisorRequest> supervisorRequests)
        {

            var supervisores = supervisorRequests.Select(
                x =>
                    new Supervisor(new User(x.userRequest.Id, x.userRequest.firstName, x.userRequest.secondName,
                        x.userRequest.password, x.userRequest.mail,
                        new Rol(x.userRequest.userRol.name, x.userRequest.userRol.description)))
                    {
                        auditoria =
                            new Auditoria(x.auditoriaRequest.usuarioCreo, x.auditoriaRequest.fechaCreo,
                                x.auditoriaRequest.usuarioModifico, x.auditoriaRequest.fechaModifico),
                        lugaresVisitas = getLugaresVisitas(x.lugarVisitaRequests)
                        

                    }).ToList();

            return supervisores;
        }

        private static IList<LugarVisita> getLugaresVisitas(IEnumerable<LugarVisitaRequest> lugarVisitaRequests)
        {
            return
                lugarVisitaRequests.Select(
                    x => new LugarVisita(getEmpresa(x.empresaRequest), getSucursal(x.sucursalRequest))).ToList();
        }

        private static Empresa getEmpresa(EmpresaRequest empresaRequest)
        {
            var empresa = new Empresa(new RTN(empresaRequest.rtnRequest.RTN), empresaRequest.nombre,
                empresaRequest.fechaIngreso, new GremioNull());
            return empresa;
        }

        private static Sucursal getSucursal(SucursalRequest sucursalRequest)
        {
            var sucursal = new Sucursal(sucursalRequest.nombre, new DireccionNull(), new FirmaAutorizadaNull());
            sucursal.Id = sucursalRequest.guid;

            return sucursal;
        }

        private static IList<SupervisorRequest> getSupervisorRequests()
        {
            return new List<SupervisorRequest>()
            {
                new SupervisorRequest()
                {
                    auditoriaRequest = getAuditoriaRequest(),
                    lugarVisitaRequests = new List<LugarVisitaRequest>(){getLugarVisitaRequest()},
                    userRequest = getUserRequest()
                }
            };
        }

        private static AuditoriaRequest getAuditoriaRequest()
        {
            return new AuditoriaRequest()
            {
                fechaCreo = new DateTime(2014, 8, 2),
                usuarioCreo = "usuarioCreo",
                fechaModifico = new DateTime(2014, 8, 2),
                usuarioModifico = "usuarioModifico"
            };
        }

        private static UserRequest getUserRequest()
        {
            var rol = new RolRequest() { idGuid = Guid.NewGuid() };
            var user = new UserRequest { firstName = "Dante", Id = "DRCD", userRol = rol, mail = "xx", password = "dd", secondName = "Castillo" };
            return user;
        }

        private static LugarVisitaRequest getLugarVisitaRequest()
        {
            return new LugarVisitaRequest()
            {
                empresaRequest = getEmpresaRequest(),
                sucursalRequest = getSucursalRequest(),
                auditoriaRequest = getAuditoriaRequest()
            };
        }

        private static EmpresaRequest getEmpresaRequest()
        {
            return new EmpresaRequest()
            {
                rtnRequest = new RTNRequest() { RTN = "08011985123960" },
                nombre = "Empresa"

            };
        }

        private static SucursalRequest getSucursalRequest()
        {
            return new SucursalRequest()
            {
                guid = Guid.NewGuid(),
                nombre = "Sucursal"
            };
        }
    }
}