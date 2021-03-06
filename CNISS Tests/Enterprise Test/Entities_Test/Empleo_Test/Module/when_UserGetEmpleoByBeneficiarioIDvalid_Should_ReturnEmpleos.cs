﻿using System;
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

               ).With(x => x.Auditoria = Builder<Auditoria>.CreateNew().Build()).Build();

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
            var direccion = beneficiario.Direccion;
            if (direccion == null)
            {
                return new DireccionRequest();
            }
            var departamentoRequest = new DepartamentoRequest()
            {
                idDepartamento = direccion.Departamento.Id,
                nombre = direccion.Departamento.Nombre
            };
            var municipioRequest = new MunicipioRequest()
            {
                idMunicipio = direccion.Municipio.Id,
                idDepartamento = direccion.Municipio.Id,
                nombre = direccion.Municipio.Nombre
            };
            return new DireccionRequest()
            {
                departamentoRequest = departamentoRequest,
                municipioRequest = municipioRequest,
                descripcion = direccion.ReferenciaDireccion,
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
                    fechaNacimiento = x.FechaNacimiento,
                    nombreRequest = new NombreRequest()
                    {
                        nombres = x.Nombre.Nombres,
                        primerApellido = x.Nombre.PrimerApellido,
                        segundoApellido = x.Nombre.SegundoApellido
                    },
                    parentescoRequest = new ParentescoRequest()
                    {
                        descripcion = x.Parentesco.Descripcion,
                        guid = x.Parentesco.Id
                    },
                    auditoriaRequest = new AuditoriaRequest()
                    {
                        fechaCreo = x.auditoria.FechaCreacion,
                        fechaModifico = x.auditoria.FechaActualizacion,
                        usuarioCreo = x.auditoria.CreadoPor,
                        usuarioModifico = x.auditoria.ActualizadoPor
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
                    identidadRequest = new IdentidadRequest() { identidad = x.Beneficiario.Id.identidad },
                    nombreRequest = new NombreRequest()
                    {
                        nombres = x.Beneficiario.Nombre.Nombres,
                        primerApellido = x.Beneficiario.Nombre.PrimerApellido,
                        segundoApellido = x.Beneficiario.Nombre.SegundoApellido
                    },
                    fechaNacimiento = x.Beneficiario.FechaNacimiento,
                    dependienteRequests = getDependienteRequests(x.Beneficiario.Dependientes),
                    direccionRequest = getDireccionRequest(x.Beneficiario),
                    telefonoCelular = x.Beneficiario.TelefonoCelular ?? "",
                    telefonoFijo = x.Beneficiario.TelefonoFijo ?? ""
                    

                },
                cargo = x.Cargo,
                supervisado = x.Supervisado,
                comprobantes = x.ComprobantesPago.Select(z => new ComprobantePagoRequest()
                {
                    deducciones = z.Deducciones,
                    fechaPago = z.FechaPago,
                    guid = z.Id,
                    sueldoNeto = z.SueldoNeto,
                    bonificaciones = z.Total
                }),
                empresaRequest = new EmpresaRequest()
                {
                    nombre = x.Empresa.Nombre,
                    rtnRequest = new RTNRequest() { RTN = x.Empresa.Id.Rtn }
                },
                sucursalRequest = new SucursalRequest()
                {
                    guid = x.Sucursal.Id,
                    nombre = x.Sucursal.Nombre,
                     firmaAutorizadaRequest = new FirmaAutorizadaRequest()
                    {
                        IdGuid = x.Sucursal.Firma.Id,
                        fechaCreacion = x.Sucursal.Firma.fechaCreacion,
                        userRequest = new UserRequest()
                        {
                            Id = x.Sucursal.Firma.User.Id
                        }
                    }

                },
               
                contrato = x.Contrato == null ? "" : x.Contrato.Id.ToString(),
                horarioLaboralRequest = new HorarioLaboralRequest()
                {
                    diasLaborablesRequest = new DiasLaborablesRequest()
                    {
                        domingo = x.HorarioLaboral.DiasLaborables.Domingo,
                        lunes = x.HorarioLaboral.DiasLaborables.Lunes,
                        martes = x.HorarioLaboral.DiasLaborables.Martes,
                        miercoles = x.HorarioLaboral.DiasLaborables.Miercoles,
                        jueves = x.HorarioLaboral.DiasLaborables.Jueves,
                        viernes = x.HorarioLaboral.DiasLaborables.Viernes,
                        sabado = x.HorarioLaboral.DiasLaborables.Sabado
                    },
                    horaEntrada = new HoraRequest()
                    {
                        hora = x.HorarioLaboral.HoraEntrada.HoraEntera,
                        minutos = x.HorarioLaboral.HoraEntrada.Minutos,
                        parte = x.HorarioLaboral.HoraEntrada.Parte

                    },
                    horaSalida = new HoraRequest()
                    {
                        hora = x.HorarioLaboral.HoraSalida.HoraEntera,
                        minutos = x.HorarioLaboral.HoraSalida.Minutos,
                        parte = x.HorarioLaboral.HoraSalida.Parte

                    }
                },
                fechaDeInicio = x.FechaDeInicio,
                  auditoriaRequest = new AuditoriaRequest()
                {
                    fechaCreo = x.Auditoria.FechaCreacion,
                    fechaModifico = x.Auditoria.FechaActualizacion,
                    usuarioCreo = x.Auditoria.CreadoPor,
                    usuarioModifico = x.Auditoria.ActualizadoPor
                },
                sueldo = x.Sueldo,
                tipoEmpleoRequest = new TipoEmpleoRequest()
                {
                    descripcion = x.TipoEmpleo.Descripcion,
                    IdGuid = x.TipoEmpleo.Id
                },
                IdGuid = x.Id
            }
                );
        }

    }
}