using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Domain;
using CNISS.CommonDomain.Ports.Input.REST.Modules.EmpleoModule.Query;
using CNISS.CommonDomain.Ports.Input.REST.Request.AuditoriaRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.BeneficiarioRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.EmpleoRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.EmpresaRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.GremioRequest;
using CNISS.EnterpriseDomain.Domain;
using CNISS.EnterpriseDomain.Domain.Entities;
using CNISS.EnterpriseDomain.Domain.Repositories;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using FizzWare.NBuilder;
using FluentAssertions;
using FluentAssertions.Events;
using Machine.Specifications;
using Moq;
using Nancy.Testing;
using It = Machine.Specifications.It;

namespace CNISS_Tests.Enterprise_Test.Entities_Test.Empleo_Test.Module
{
    [Subject(typeof (EmpleoModuleQuery))]
    public class when_UserGetsAllEmpleo_Should_ReturnAllEmpleos
    {
        static Browser _browser;
        static BrowserResponse _response;
        private static IEmpleoRepositoryReadOnly _repositoryRead;

        private static IEnumerable<EmpleoRequest> _expectedResponse;

        private static IEnumerable<EmpleoRequest> _responseEmpleos; 

        private Establish context = () =>
        {
            var empleos = Builder<Empleo>.CreateListOfSize(10).All().WithConstructor(
                ()=> new Empleo(Builder<Empresa>.CreateNew().WithConstructor(
                    ()=> new Empresa(new RTN("08011985123960"),"empresa",new DateTime(2014,2,1),new GremioNull() )  
                    ).Build(),Builder<Sucursal>.CreateNew().WithConstructor(()=> new Sucursal("Sucursal",new DireccionNull(), new FirmaAutorizadaNull())).Build(),
                    Builder<Beneficiario>.CreateNew().WithConstructor(()=> new Beneficiario(new Identidad("0801198512396"),Builder<Nombre>.CreateNew().Build(),new DateTime(1984,8,2))).Build(),
                    Builder<HorarioLaboral>.CreateNew().WithConstructor(()=> new HorarioLaboral(Builder<Hora>.CreateNew().Build(),Builder<Hora>.CreateNew().Build(),Builder<DiasLaborables>.CreateNew().Build())).Build(),
                    "Ingeniero",12000m,Builder<TipoEmpleo>.CreateNew().Build(),new DateTime(2014,8,2))

                ).All().With(x => x.auditoria = Builder<Auditoria>.CreateNew().Build()).Build();

            _repositoryRead = Mock.Of<IEmpleoRepositoryReadOnly>();
            Mock.Get(_repositoryRead).Setup(x => x.getAll()).Returns(empleos);

            _expectedResponse = getEmpleoRequests(empleos);

            _browser = new Browser(
                x =>
                {
                    x.Module<EmpleoModuleQuery>();
                    x.Dependencies(_repositoryRead);
                }
                
                );
        };

        private Because of = () =>
        {
            _responseEmpleos = _browser.GetSecureJson("/enterprise/empleos").Body.DeserializeJson<IEnumerable<EmpleoRequest>>();
        };

        It should_return_empleos = () => _responseEmpleos.ShouldBeEquivalentTo(_expectedResponse);

        private static IEnumerable<DependienteRequest> getDependienteRequests(IEnumerable<Dependiente> dependientes)
        {
            var dependientesRequest = new List<DependienteRequest>();
            if (dependientes != null)
            {
                dependientesRequest = dependientes.Select(x => new DependienteRequest()
                {
                    IdGuid = x.idGuid,
                    identidadRequest = new IdentidadRequest() { identidad = x.Id.identidad },
                    fechaNacimiento = x.fechaNacimiento,
                    nombreRequest = new NombreRequest()
                    {
                        nombres = x.nombre.nombres,
                        primerApellido = x.nombre.primerApellido,
                        segundoApellido = x.nombre.segundoApellido
                    },
                    parentescoRequest = new ParentescoRequest()
                    {
                        descripcion = x.parentesco.descripcion,
                        guid = x.parentesco.Id
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

            return dependientesRequest;
        }


        private static IEnumerable<EmpleoRequest> getEmpleoRequests(IEnumerable<Empleo> empleos)
        {
            return empleos.Select(x => new EmpleoRequest()
            {
                beneficiarioRequest = new BeneficiarioRequest()
                {
                    identidadRequest = new IdentidadRequest() { identidad = x.beneficiario.Id.identidad},
                    nombreRequest = new NombreRequest()
                    {
                        nombres = x.beneficiario.nombre.nombres,
                        primerApellido = x.beneficiario.nombre.primerApellido,
                        segundoApellido = x.beneficiario.nombre.segundoApellido
                    },
                    fechaNacimiento = x.beneficiario.fechaNacimiento,
                    dependienteRequests = getDependienteRequests(x.beneficiario.dependientes)

                },
                cargo = x.cargo,
                comprobantes = x.comprobantesPago.Select(z=> new ComprobantePagoRequest()
                {
                    deducciones = z.deducciones,
                    fechaPago = z.fechaPago,
                    guid = z.Id,
                    sueldoNeto = z.sueldoNeto,
                    bonificaciones = z.total
                }),
                empresaRequest = new EmpresaRequest()
                {
                    nombre = x.empresa.nombre,
                    rtnRequest = new RTNRequest() { RTN = x.empresa.Id.rtn}
                },
                sucursalRequest = new SucursalRequest()
                {
                    guid = x.sucursal.Id,
                    nombre = x.sucursal.nombre

                },

                  auditoriaRequest = new AuditoriaRequest()
                {
                    fechaCreo = x.auditoria.fechaCreo,
                    fechaModifico = x.auditoria.fechaModifico,
                    usuarioCreo = x.auditoria.usuarioCreo,
                    usuarioModifico = x.auditoria.usuarioModifico
                },
                fechaDeInicio = x.fechaDeInicio,
                horarioLaboralRequest = new HorarioLaboralRequest()
                {
                    diasLaborablesRequest = new DiasLaborablesRequest()
                    {
                        domingo = x.horarioLaboral.diasLaborables.domingo,
                        lunes = x.horarioLaboral.diasLaborables.lunes,
                        martes = x.horarioLaboral.diasLaborables.martes,
                        miercoles = x.horarioLaboral.diasLaborables.miercoles,
                        jueves = x.horarioLaboral.diasLaborables.jueves,
                        viernes = x.horarioLaboral.diasLaborables.viernes,
                        sabado = x.horarioLaboral.diasLaborables.sabado
                    },
                    horaEntrada = new HoraRequest()
                    {
                        hora = x.horarioLaboral.horaEntrada.hora,
                        minutos = x.horarioLaboral.horaEntrada.minutos,
                        parte = x.horarioLaboral.horaEntrada.parte

                    },
                    horaSalida = new HoraRequest()
                    {
                        hora = x.horarioLaboral.horaSalida.hora,
                        minutos = x.horarioLaboral.horaSalida.minutos,
                        parte = x.horarioLaboral.horaSalida.parte

                    }
                },
                contrato = x.contrato == null ? "" : x.contrato.Id.ToString(),
                sueldo = x.sueldo,
                tipoEmpleoRequest = new TipoEmpleoRequest()
                {
                    descripcion = x.tipoEmpleo.descripcion,
                    IdGuid = x.tipoEmpleo.Id
                },
                IdGuid = x.Id
            }
                );
        }
    }
}