using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.AutenticationDomain.Domain.Entities;
using CNISS.AutenticationDomain.Domain.ValueObjects;
using CNISS.CommonDomain.Ports.Input.REST.Modules.VisitaModule.Query;
using CNISS.CommonDomain.Ports.Input.REST.Request.EmpresaRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.GremioRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.UserRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.VisitaRequest;
using CNISS.EnterpriseDomain.Domain;
using CNISS.EnterpriseDomain.Domain.Entities;
using CNISS.EnterpriseDomain.Domain.Repositories;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using FluentAssertions;
using Machine.Specifications;
using Moq;
using Nancy;
using Nancy.Responses.Negotiation;
using Nancy.Testing;
using Should;
using It = Machine.Specifications.It;

namespace CNISS_Tests.Autentication_Test.User_Test.Modules
{
    [Subject(typeof (SupervisorLugaresVisitaModuleQuery))]
    public class when_MovilUserGetLugaresVisitas_Should_ReturnLugaresVisita
    {
        private static Browser _browser;
        private static BrowserResponse _response;
        private static SupervisorRequest _expectedSupervisor;
        private static SupervisorRequest _responseSupervisor;
        
        
        private Establish context = () =>
        {

            var userIdentityMovil = new DummyUserIdentityMovil("DRCD");

            var repository = Mock.Of<IVisitaRepositoryReadOnly>();

              var user = new User("DRCD", "Dante", "Ruben", "XXXX", "XXXX", new RolNull());
            var supervisor = getSupervisor(user);
            _expectedSupervisor = getSupervisorRequest(supervisor);
            Mock.Get(repository).Setup(x => x.getAgendaSupervisor(Moq.It.Is<User>(z => z.Id == user.Id ))).Returns(supervisor);

           _browser = new Browser(x =>
           {
               x.Module<SupervisorLugaresVisitaModuleQuery>();
               
               x.Dependencies(repository);
               x.RequestStartup((container, pipelines, context) =>
               {
                   context.CurrentUser = userIdentityMovil;
               });


           });
        };

        private Because of = () =>
        {
            _responseSupervisor = _browser.GetSecureJson("/movil/supervisor/lugaresVisita").Body.DeserializeJson<SupervisorRequest>();


        };

         It should_return_lugares_visita = () => _responseSupervisor.ShouldBeEquivalentTo(_expectedSupervisor);


        private static SupervisorRequest getSupervisorRequest(Supervisor supervisor)
        {
            return new SupervisorRequest()
            {
                guid = supervisor.Id,
                userRequest = new UserRequest()
                {
                    Id = supervisor.usuario.Id
                },
                lugarVisitaRequests = supervisor.lugaresVisitas.Select(x => new LugarVisitaRequest()
                {
                    guid = x.Id,
                    empresaRequest = new EmpresaRequest()
                    {
                        nombre = x.empresa.nombre,
                        rtnRequest = new RTNRequest() { RTN = x.empresa.Id.rtn}
                    },
                    sucursalRequest = new SucursalRequest()
                    {
                        guid = x.sucursal.Id,
                        nombre = x.sucursal.nombre,
                        direccionRequest = new DireccionRequest()
                        {
                            departamentoRequest = new DepartamentoRequest()
                            {
                                idDepartamento = x.sucursal.direccion.departamento.Id,
                                nombre = x.sucursal.direccion.departamento.nombre
                            },
                            municipioRequest = new MunicipioRequest()
                            {
                                idDepartamento = x.sucursal.direccion.municipio.departamentoId,
                                idMunicipio = x.sucursal.direccion.municipio.Id,
                                nombre = x.sucursal.direccion.municipio.nombre
                            },
                            descripcion = x.sucursal.direccion.referenciaDireccion
                        },
                        userFirmaRequest = new UserRequest()
                        {
                            Id = x.sucursal.firma.user.Id
                        }
                    }
                    

                }).ToList()
            };
        }

        private static Supervisor getSupervisor(User user)
        {
          
            return new Supervisor(user)
            {
                Id = Guid.NewGuid(),
                lugaresVisitas = new List<LugarVisita>()
                {
                    new LugarVisita(getEmpresa(),getSucursal() )
                }
            };
        }

        private static Empresa getEmpresa()
        {
            var empresa =  new Empresa(new RTN("08011985123960"),"Empresa XYZ",new DateTime().Date,getGremio() );
            empresa.AddSucursal(getSucursal());
            return empresa;
        }

        private static Sucursal getSucursal()
        {
            var user = new User("DRCD", "", "", "XXXX", "XXX", new RolNull());
            return new Sucursal("Barrio El Centro",getDireccion(),new FirmaAutorizada(user,new DateTime().Date));
        }

        private static Direccion getDireccion()
        {
            return new Direccion(new Departamento() {Id = "01", nombre = "Francisco Morazan"},
                new Municipio("01", "01", "Distrito Central"), "Barrio Abajo");
        }
        private static Gremio getGremio()
        {
            return new Gremio(new RTN("08011985123960"),new RepresentanteLegal(new Identidad("0801198512396"),"Dante Castillo"),getDireccion(),"Camara San Pedro"   );
        }
    }
}