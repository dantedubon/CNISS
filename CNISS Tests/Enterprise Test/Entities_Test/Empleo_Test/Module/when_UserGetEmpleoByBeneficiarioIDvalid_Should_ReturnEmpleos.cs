using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.AutenticationDomain.Domain.Entities;
using CNISS.AutenticationDomain.Domain.ValueObjects;
using CNISS.CommonDomain.Domain;
using CNISS.CommonDomain.Ports.Input.REST.Modules.EmpleoModule.Query;
using CNISS.CommonDomain.Ports.Input.REST.Request.AuditoriaRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.BeneficiarioRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.EmpleoRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.EmpresaRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.GremioRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.UserRequest;
using CNISS.EnterpriseDomain.Domain;
using CNISS.EnterpriseDomain.Domain.Entities;
using CNISS.EnterpriseDomain.Domain.Repositories;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using FizzWare.NBuilder;
using FluentAssertions;
using Machine.Specifications;
using Moq;
using Nancy.Testing;
using It = Machine.Specifications.It;

namespace CNISS_Tests.Enterprise_Test.Entities_Test.Empleo_Test.Module
{
    [Subject(typeof(EmpleoModuleQuery))]
    public class when_UserGetEmpleoByBeneficiarioIDvalid_Should_ReturnEmpleos
    {
        static Browser _browser;
        static BrowserResponse _response;
        private static IEmpleoRepositoryReadOnly _repositoryRead;
        private static IdentidadRequest _idRequest;
        private static IEnumerable<EmpleoRequest> _expectedEmpleos;
        private static IEnumerable<EmpleoRequest> _responseEmpleos;

        private Establish context = () =>
        {
            _idRequest = new IdentidadRequest(){identidad = "0801198512396"};
            _repositoryRead = Mock.Of<IEmpleoRepositoryReadOnly>();

            var empleos = Builder<Empleo>.CreateListOfSize(10).All().WithConstructor(
               () => new Empleo(Builder<Empresa>.CreateNew().WithConstructor(
                   () => new Empresa(new RTN("08011985123960"), "empresa", new DateTime(2014, 2, 1), new GremioNull())
                   ).Build(), Builder<Sucursal>.CreateNew().WithConstructor(() => new Sucursal("Sucursal", new DireccionNull(), new FirmaAutorizada(new User("DRCD","","XX","","",new RolNull()),DateTime.Now.Date ))).Build(),
                   Builder<Beneficiario>.CreateNew().WithConstructor(() => new Beneficiario(new Identidad("0801198512396"), Builder<Nombre>.CreateNew().Build(), new DateTime(1984, 8, 2))).Build(),
                   Builder<HorarioLaboral>.CreateNew().WithConstructor(() => new HorarioLaboral(Builder<Hora>.CreateNew().Build(), Builder<Hora>.CreateNew().Build(), Builder<DiasLaborables>.CreateNew().Build())).Build(),
                   "Ingeniero", 12000m, Builder<TipoEmpleo>.CreateNew().Build(), new DateTime(2014, 8, 2))

               ).With(x => x.auditoria = Builder<Auditoria>.CreateNew().Build()).Build();

            _expectedEmpleos = getEmpleoRequests(empleos);

            var identidad = new Identidad(_idRequest.identidad);
            _repositoryRead = Mock.Of<IEmpleoRepositoryReadOnly>();
            Mock.Get(_repositoryRead).Setup(x => x.getEmpleosByBeneficiario(identidad)).Returns(empleos);


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
            _responseEmpleos =
                _browser.GetSecureJson("/enterprise/empleos/beneficiario/id=" + _idRequest.identidad).Body.DeserializeJson<IEnumerable<EmpleoRequest>>();

        };

        It should_return_empleos = () => _responseEmpleos.ShouldBeEquivalentTo(_expectedEmpleos);


        private static DireccionRequest getDireccionRequest(Beneficiario beneficiario)
        {
            var direccion = beneficiario.direccion;
            if (direccion == null)
            {
                return new DireccionRequest();
            }
            var departamentoRequest = new DepartamentoRequest()
            {
                idDepartamento = direccion.departamento.Id,
                nombre = direccion.departamento.nombre
            };
            var municipioRequest = new MunicipioRequest()
            {
                idMunicipio = direccion.municipio.Id,
                idDepartamento = direccion.municipio.Id,
                nombre = direccion.municipio.nombre
            };
            return new DireccionRequest()
            {
                departamentoRequest = departamentoRequest,
                municipioRequest = municipioRequest,
                descripcion = direccion.referenciaDireccion,
                IdGuid = direccion.Id
            };
        }

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
                    identidadRequest = new IdentidadRequest() { identidad = x.beneficiario.Id.identidad },
                    nombreRequest = new NombreRequest()
                    {
                        nombres = x.beneficiario.nombre.nombres,
                        primerApellido = x.beneficiario.nombre.primerApellido,
                        segundoApellido = x.beneficiario.nombre.segundoApellido
                    },
                    fechaNacimiento = x.beneficiario.fechaNacimiento,
                    dependienteRequests = getDependienteRequests(x.beneficiario.dependientes),
                    direccionRequest = getDireccionRequest(x.beneficiario),
                    telefonoCelular = x.beneficiario.telefonoCelular ?? "",
                    telefonoFijo = x.beneficiario.telefonoFijo ?? ""
                    

                },
                cargo = x.cargo,
                comprobantes = x.comprobantesPago.Select(z => new ComprobantePagoRequest()
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
                    rtnRequest = new RTNRequest() { RTN = x.empresa.Id.rtn }
                },
                sucursalRequest = new SucursalRequest()
                {
                    guid = x.sucursal.Id,
                    nombre = x.sucursal.nombre,
                     firmaAutorizadaRequest = new FirmaAutorizadaRequest()
                    {
                        IdGuid = x.sucursal.firma.Id,
                        fechaCreacion = x.sucursal.firma.fechaCreacion,
                        userRequest = new UserRequest()
                        {
                            Id = x.sucursal.firma.user.Id
                        }
                    }

                },
               
                contrato = x.contrato == null ? "" : x.contrato.Id.ToString(),
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
                fechaDeInicio = x.fechaDeInicio,
                  auditoriaRequest = new AuditoriaRequest()
                {
                    fechaCreo = x.auditoria.fechaCreo,
                    fechaModifico = x.auditoria.fechaModifico,
                    usuarioCreo = x.auditoria.usuarioCreo,
                    usuarioModifico = x.auditoria.usuarioModifico
                },
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