using System;
using System.Collections.Generic;
using System.Linq;
using CNISS.CommonDomain.Ports.Input.REST.Modules.EmpleoModule.Query;
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
using Machine.Specifications;
using Moq;
using Nancy;
using Nancy.Testing;
using Should;
using It = Machine.Specifications.It;

namespace CNISS_Tests.Enterprise_Test.Entities_Test.Empleo_Test.Module
{
    [Subject(typeof(EmpleoModuleQuery))]
    public class when_UserGetEmpleoById_Should_returnEmpleo
    {
        static Browser _browser;
        static BrowserResponse _response;
        private static IEmpleoRepositoryReadOnly _repositoryRead;
        private static Guid _idRequest;
        private static EmpleoRequest _expectedEmpleo;
        private static EmpleoRequest _responseEmpleo;

        private Establish context = () =>
        {

            _repositoryRead = Mock.Of<IEmpleoRepositoryReadOnly>();
            var empleo = Builder<Empleo>.CreateNew().WithConstructor(
               () => new Empleo(Builder<Empresa>.CreateNew().WithConstructor(
                   () => new Empresa(new RTN("08011985123960"), "empresa", new DateTime(2014, 2, 1), new GremioNull())
                   ).Build(), Builder<Sucursal>.CreateNew().WithConstructor(() => new Sucursal("Sucursal", new DireccionNull(), new FirmaAutorizadaNull())).Build(),
                   Builder<Beneficiario>.CreateNew().WithConstructor(() => new Beneficiario(new Identidad("0801198512396"), Builder<Nombre>.CreateNew().Build(), new DateTime(1984, 8, 2))).Build(),
                   Builder<HorarioLaboral>.CreateNew().WithConstructor(() => new HorarioLaboral(Builder<Hora>.CreateNew().Build(), Builder<Hora>.CreateNew().Build(), Builder<DiasLaborables>.CreateNew().Build())).Build(),
                   "Ingeniero", 12000m, Builder<TipoEmpleo>.CreateNew().Build(), new DateTime(2014, 8, 2))

               ).Build();
            Mock.Get(_repositoryRead).Setup(x => x.get(empleo.Id)).Returns(empleo);

            _expectedEmpleo = getEmpleoRequests(empleo);



            _idRequest = Guid.Empty;
            _browser = new Browser(
                x =>
                {
                    x.Module<EmpleoModuleQuery>();
                    x.Dependencies(_repositoryRead);
                }

                );


        };

        private Because of = () => { _responseEmpleo = _browser.GetSecureJson("/enterprise/empleos/id=" + _expectedEmpleo.IdGuid).Body.DeserializeJson<EmpleoRequest>(); };

        It should_return_empleo = () => _responseEmpleo.ShouldBeEquivalentTo(_expectedEmpleo);



        private static EmpleoRequest getEmpleoRequests(Empleo empleo)
        {
            return new EmpleoRequest()
            {
                beneficiarioRequest = new BeneficiarioRequest()
                {
                    identidadRequest = new IdentidadRequest() { identidad = empleo.beneficiario.Id.identidad },
                    nombreRequest = new NombreRequest()
                    {
                        nombres = empleo.beneficiario.nombre.nombres,
                        primerApellido = empleo.beneficiario.nombre.primerApellido,
                        segundoApellido = empleo.beneficiario.nombre.segundoApellido
                    },
                    fechaNacimiento = empleo.beneficiario.fechaNacimiento


                },
                cargo = empleo.cargo,
                comprobantes = empleo.comprobantesPago.Select(z => new ComprobantePagoRequest()
                {
                    deducciones = z.deducciones,
                    fechaPago = z.fechaPago,
                    guid = z.Id,
                    percepciones = z.percepciones,
                    total = z.total
                }),
                empresaRequest = new EmpresaRequest()
                {
                    nombre = empleo.empresa.nombre,
                    rtnRequest = new RTNRequest() { RTN = empleo.empresa.Id.rtn }
                },
                sucursalRequest = new SucursalRequest()
                {
                    guid = empleo.sucursal.Id,
                    nombre = empleo.sucursal.nombre
                },
                fechaDeInicio = empleo.fechaDeInicio,
                horarioLaboralRequest = new HorarioLaboralRequest()
                {
                    diasLaborablesRequest = new DiasLaborablesRequest()
                    {
                        domingo = empleo.horarioLaboral.diasLaborables.domingo,
                        lunes = empleo.horarioLaboral.diasLaborables.lunes,
                        martes = empleo.horarioLaboral.diasLaborables.martes,
                        miercoles = empleo.horarioLaboral.diasLaborables.miercoles,
                        jueves = empleo.horarioLaboral.diasLaborables.jueves,
                        viernes = empleo.horarioLaboral.diasLaborables.viernes,
                        sabado = empleo.horarioLaboral.diasLaborables.sabado
                    },
                    horaEntrada = new HoraRequest()
                    {
                        hora = empleo.horarioLaboral.horaEntrada.hora,
                        minutos = empleo.horarioLaboral.horaEntrada.minutos,
                        parte = empleo.horarioLaboral.horaEntrada.parte

                    },
                    horaSalida = new HoraRequest()
                    {
                        hora = empleo.horarioLaboral.horaSalida.hora,
                        minutos = empleo.horarioLaboral.horaSalida.minutos,
                        parte = empleo.horarioLaboral.horaSalida.parte

                    }
                },
                contrato = empleo.contrato == null ? "" : empleo.contrato.Id.ToString(),
                sueldo = empleo.sueldo,
                tipoEmpleoRequest = new TipoEmpleoRequest()
                {
                    descripcion = empleo.tipoEmpleo.descripcion,
                    IdGuid = empleo.tipoEmpleo.Id
                },
                IdGuid = empleo.Id
            };

        }
    }
}