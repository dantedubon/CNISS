using System;
using System.Collections.Generic;
using System.Linq;
using CNISS.AutenticationDomain.Domain.Entities;
using CNISS.AutenticationDomain.Domain.ValueObjects;
using CNISS.CommonDomain.Domain;
using CNISS.CommonDomain.Ports.Input.REST.Modules.VisitaModule.Query;
using CNISS.CommonDomain.Ports.Input.REST.Request.AuditoriaRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.EmpresaRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.GremioRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.RolModule;
using CNISS.CommonDomain.Ports.Input.REST.Request.UserRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.VisitaRequest;
using CNISS.EnterpriseDomain.Domain;
using CNISS.EnterpriseDomain.Domain.Entities;
using CNISS.EnterpriseDomain.Domain.Repositories;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using FluentAssertions;
using Machine.Specifications;
using Moq;
using Nancy.Testing;
using It = Machine.Specifications.It;

namespace CNISS_Tests.Entities_Test.Visita_Test.Module.Query
{
    [Subject(typeof (VisitaModuleQuery))]
    public class when_UserGetAllVisits_Should_ReturnAllVisits
    {

        static Browser _browser;
        static IEnumerable<VisitaRequest> _expectedVisitaRequests;
        static IEnumerable<VisitaRequest> _visitaResponse;
       
        static BrowserResponse _response;

        private Establish context = () =>
        {
            var visita = new Visita("Gira Prueba", new DateTime(2014, 8, 1), new DateTime(2014, 8, 30))
            {
                Auditoria = new Auditoria("UsuarioCreo",new DateTime(2014,7,1),"UsuarioModifico",new DateTime(2014,7,30)),
                Supervisores = new List<Supervisor>()
                {
                    new Supervisor(new User("DRCD","Dante","Castillo","XXX","XXX",new Rol("Rol Prueba","Rol Prueba")))
                    {
                        Auditoria = new Auditoria("UsuarioCreo",new DateTime(2014,7,1),"UsuarioModifico",new DateTime(2014,7,30)),
                        LugaresVisitas = new List<LugarVisita>()
                        {
                            new LugarVisita(new Empresa(new RTN("08011985123960"), "XYZ",new DateTime(2014,7,15),new GremioNull()),new Sucursal("El Centro",new Direccion(new Departamento(){Id = "01",Nombre = "Francisco Morazan"},new Municipio("01","01","Distrito Central"),"Barrio Abajo")
                           , new FirmaAutorizadaNull()) )
                            {
                                Auditoria = new Auditoria("UsuarioCreo",new DateTime(2014,7,1),"UsuarioModifico",new DateTime(2014,7,30))
                            }
                        }
                    }
                }
            };


            _expectedVisitaRequests = new List<VisitaRequest>() {getVisitaRequest(visita)};
            var repository = Mock.Of<IVisitaRepositoryReadOnly>();
            Mock.Get(repository).Setup(x => x.getAll()).Returns(new List<Visita>() {visita});



            _browser = new Browser(

                x =>
                {
                    x.Module<VisitaModuleQuery>();
                    x.Dependencies(repository);
                }
            );




        };

        private Because of = () =>
        {
            _visitaResponse = _browser.GetSecureJson("/visita").Body.DeserializeJson<IEnumerable<VisitaRequest>>();
        };

        It should_return_allVisits = () => _visitaResponse.ShouldBeEquivalentTo(_expectedVisitaRequests);



       


        #region Metodos de Mapeo
        private static VisitaRequest getVisitaRequest(Visita visita)
        {
            var visitaRequest = new VisitaRequest()
            {
                guid = visita.Id,
                fechaInicial = visita.FechaInicial,
                fechaFinal = visita.FechaFinal,
                nombre = visita.Nombre,
                auditoriaRequest = new AuditoriaRequest()
                {
                    fechaCreo = visita.Auditoria.FechaCreacion,
                    fechaModifico = visita.Auditoria.FechaActualizacion,
                    usuarioCreo = visita.Auditoria.CreadoPor,
                    usuarioModifico = visita.Auditoria.ActualizadoPor
                },
                supervisoresRequests = getSupervisoresRequests(visita.Supervisores)
            };

            return visitaRequest;
        }


        private static IList<SupervisorRequest> getSupervisoresRequests(IEnumerable<Supervisor> supervisores)
        {
            return supervisores.Select(x => new SupervisorRequest()
            {
                guid = x.Id,
                auditoriaRequest = new AuditoriaRequest()
                {
                    fechaCreo = x.Auditoria.FechaCreacion,
                    fechaModifico = x.Auditoria.FechaActualizacion,
                    usuarioCreo = x.Auditoria.CreadoPor,
                    usuarioModifico = x.Auditoria.ActualizadoPor
                },

                userRequest = new UserRequest()
                {
                    Id = x.Usuario.Id,
                    firstName = x.Usuario.FirstName,
                    mail = x.Usuario.Mail,
                    secondName = x.Usuario.SecondName,
                    password = "XXX",
                    userRol = new RolRequest()
                    {
                        idGuid = x.Usuario.UserRol.Id
                        
                    }

                },
                lugarVisitaRequests = x.LugaresVisitas.Select(z => new LugarVisitaRequest()
                {
                    guid = z.Id,
                    empresaRequest = new EmpresaRequest()
                    {
                        rtnRequest = new RTNRequest() { RTN = z.Empresa.Id.Rtn },
                        nombre = z.Empresa.Nombre
                        
                    },
                   sucursalRequest = new SucursalRequest()
                    {
                        guid = z.Sucursal.Id,
                        nombre = z.Sucursal.Nombre,
                        direccionRequest = new DireccionRequest()
                        {
                            departamentoRequest = new DepartamentoRequest()
                            {
                                idDepartamento = z.Sucursal.Direccion.Departamento.Id,
                                nombre = z.Sucursal.Direccion.Departamento.Nombre
                            },
                            municipioRequest = new MunicipioRequest()
                            {
                                idDepartamento = z.Sucursal.Direccion.Municipio.DepartamentoId,
                                idMunicipio = z.Sucursal.Direccion.Municipio.Id,
                                nombre = z.Sucursal.Direccion.Municipio.Nombre
                            },
                            descripcion = z.Sucursal.Direccion.ReferenciaDireccion


                        }
                        
                    },
                    auditoriaRequest = new AuditoriaRequest()
                    {
                        fechaCreo = z.Auditoria.FechaCreacion,
                        fechaModifico = z.Auditoria.FechaActualizacion,
                        usuarioCreo = z.Auditoria.CreadoPor,
                        usuarioModifico = z.Auditoria.ActualizadoPor
                    },



                }).ToList()
            }).ToList();
        } 
        #endregion
    }
}