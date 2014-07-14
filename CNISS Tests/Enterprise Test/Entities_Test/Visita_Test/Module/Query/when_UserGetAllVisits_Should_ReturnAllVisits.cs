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
                auditoria = new Auditoria("UsuarioCreo",new DateTime(2014,7,1),"UsuarioModifico",new DateTime(2014,7,30)),
                supervisores = new List<Supervisor>()
                {
                    new Supervisor(new User("DRCD","Dante","Castillo","XXX","XXX",new Rol("Rol Prueba","Rol Prueba")))
                    {
                        auditoria = new Auditoria("UsuarioCreo",new DateTime(2014,7,1),"UsuarioModifico",new DateTime(2014,7,30)),
                        lugaresVisitas = new List<LugarVisita>()
                        {
                            new LugarVisita(new Empresa(new RTN("08011985123960"), "XYZ",new DateTime(2014,7,15),new GremioNull()),new Sucursal("El Centro",new Direccion(new Departamento(){Id = "01",nombre = "Francisco Morazan"},new Municipio("01","01","Distrito Central"),"Barrio Abajo")
                           , new FirmaAutorizadaNull()) )
                            {
                                auditoria = new Auditoria("UsuarioCreo",new DateTime(2014,7,1),"UsuarioModifico",new DateTime(2014,7,30))
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
                fechaInicial = visita.fechaInicial,
                fechaFinal = visita.fechaFinal,
                nombre = visita.nombre,
                auditoriaRequest = new AuditoriaRequest()
                {
                    fechaCreo = visita.auditoria.fechaCreo,
                    fechaModifico = visita.auditoria.fechaModifico,
                    usuarioCreo = visita.auditoria.usuarioCreo,
                    usuarioModifico = visita.auditoria.usuarioModifico
                },
                supervisoresRequests = getSupervisoresRequests(visita.supervisores)
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
                    fechaCreo = x.auditoria.fechaCreo,
                    fechaModifico = x.auditoria.fechaModifico,
                    usuarioCreo = x.auditoria.usuarioCreo,
                    usuarioModifico = x.auditoria.usuarioModifico
                },

                userRequest = new UserRequest()
                {
                    Id = x.usuario.Id,
                    firstName = x.usuario.firstName,
                    mail = x.usuario.mail,
                    secondName = x.usuario.secondName,
                    password = "XXX",
                    userRol = new RolRequest()
                    {
                        idGuid = x.usuario.userRol.Id
                        
                    }

                },
                lugarVisitaRequests = x.lugaresVisitas.Select(z => new LugarVisitaRequest()
                {
                    guid = z.Id,
                    empresaRequest = new EmpresaRequest()
                    {
                        rtnRequest = new RTNRequest() { RTN = z.empresa.Id.rtn },
                        nombre = z.empresa.nombre
                        
                    },
                   sucursalRequest = new SucursalRequest()
                    {
                        guid = z.sucursal.Id,
                        nombre = z.sucursal.nombre,
                        direccionRequest = new DireccionRequest()
                        {
                            departamentoRequest = new DepartamentoRequest()
                            {
                                idDepartamento = z.sucursal.direccion.departamento.Id,
                                nombre = z.sucursal.direccion.departamento.nombre
                            },
                            municipioRequest = new MunicipioRequest()
                            {
                                idDepartamento = z.sucursal.direccion.municipio.departamentoId,
                                idMunicipio = z.sucursal.direccion.municipio.Id,
                                nombre = z.sucursal.direccion.municipio.nombre
                            },
                            descripcion = z.sucursal.direccion.referenciaDireccion


                        }
                        
                    },
                    auditoriaRequest = new AuditoriaRequest()
                    {
                        fechaCreo = z.auditoria.fechaCreo,
                        fechaModifico = z.auditoria.fechaModifico,
                        usuarioCreo = z.auditoria.usuarioCreo,
                        usuarioModifico = z.auditoria.usuarioModifico
                    },



                }).ToList()
            }).ToList();
        } 
        #endregion
    }
}