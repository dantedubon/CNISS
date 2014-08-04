using System;
using System.Collections.Generic;
using System.Linq;
using CNISS.AutenticationDomain.Domain.Entities;
using CNISS.AutenticationDomain.Domain.Services;
using CNISS.AutenticationDomain.Domain.ValueObjects;
using CNISS.CommonDomain.Domain;
using CNISS.CommonDomain.Ports;
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
using Nancy;
using Nancy.Authentication.Token;
using Nancy.Testing;
using Should;
using It = Machine.Specifications.It;

namespace CNISS_Tests.Enterprise_Test.Entities_Test.Empleo_Test.Module
{
    [Subject(typeof(EmpleoModuleQuery))]
    public class when_UserMovilGetEmpleosByBeneficiario_Should_ReturnEmpleo
    {
        private static Browser _browser;
        private static BrowserResponse _browserResponse;
        private static string _Id;
        private static string _rtn;
        private static Guid _sucursalGuid;
        private static EmpleoRequest _empleoExpected;
        private static EmpleoRequest _empleoResponse;
        static string _empleoExpectedString;
        static string _empleoResponseString;

        private Establish context = () =>
        {
            var userIdentityMovil = new DummyUserIdentityMovil("DRCD");
            _Id = "0801198512396";
            _rtn = "08011985123960";
            _sucursalGuid = Guid.NewGuid();

            var empleo = Builder<Empleo>.CreateNew().WithConstructor(
              () => new Empleo(Builder<Empresa>.CreateNew().WithConstructor(
                  () => new Empresa(new RTN("08011985123960"), "empresa", new DateTime(2014, 2, 1), new GremioNull())
                  ).Build(), Builder<Sucursal>.CreateNew().WithConstructor(() => new Sucursal("Sucursal", new DireccionNull(), new FirmaAutorizada(new User("DRCD", "", "XX", "", "", new RolNull()), DateTime.Now.Date))).Build(),
                  Builder<Beneficiario>.CreateNew().WithConstructor(() => new Beneficiario(new Identidad("0801198512396"), Builder<Nombre>.CreateNew().Build(), new DateTime(1984, 8, 2))).Build(),
                  Builder<HorarioLaboral>.CreateNew().WithConstructor(() => new HorarioLaboral(Builder<Hora>.CreateNew().Build(), Builder<Hora>.CreateNew().Build(), Builder<DiasLaborables>.CreateNew().Build())).Build(),
                  "Ingeniero", 12000m, Builder<TipoEmpleo>.CreateNew().Build(), new DateTime(2014, 8, 2))

              ).With(x => x.auditoria = Builder<Auditoria>.CreateNew().Build()).Build();
            empleo.sucursal.Id = _sucursalGuid;
           
            _empleoExpected = getEmpleoRequests(empleo);

            var tokenizer = Mock.Of<ITokenizer>();
            Mock.Get(tokenizer)
                .Setup(x => x.Detokenize(Moq.It.IsAny<string>(), Moq.It.IsAny<NancyContext>()))
                .Returns(userIdentityMovil);
      
            var encryptRequestProvider = getEncrypter();

            var repository = Mock.Of<IEmpleoRepositoryReadOnly>();
            Mock.Get(repository).Setup(x => x.getEmpleoMasRecienteBeneficiario(empleo.beneficiario.Id)).Returns(empleo);


            var serializer = new SerializerRequest();
            _empleoExpectedString = serializer.toJson(_empleoExpected);

            _browser = new Browser(x =>
            {
                x.Module<EmpleoModuleQueryMovil>();
                
                x.MappedDependencies(new[]
                                    {
                                        new Tuple<Type, object>(typeof (ISerializeJsonRequest), new SerializerRequest()),
                                        new Tuple<Type, object>(typeof (Func<string, IEncrytRequestProvider>),
                                            encryptRequestProvider),
                                        new Tuple<Type, object>(typeof (ITokenizer), tokenizer),
                                        new Tuple<Type, object>(typeof(IEmpleoRepositoryReadOnly),repository)
                                    });
            });
        };

        private Because of = () =>
        {
            _empleoResponseString =
                _browser.Post("/movil/empleo/id=" + _Id + "/rtn=" + _rtn + "/sucursal=" +
                                       _sucursalGuid).Body.AsString();
        };

        It should_return_empleo = () => _empleoResponseString.ShouldBeEquivalentTo(_empleoExpectedString);

        private static Func<string, IEncrytRequestProvider> getEncrypter()
        {
            var x = new Func<string, IEncrytRequestProvider>(z => new DummyEncrytRequestProvider());
            return x;
        }

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
                    fechaNacimiento = empleo.beneficiario.fechaNacimiento,
                    dependienteRequests = getDependienteRequests(empleo.beneficiario.dependientes),
                    direccionRequest = getDireccionRequest(empleo.beneficiario),
                    telefonoCelular = empleo.beneficiario.telefonoCelular ?? "",
                    telefonoFijo = empleo.beneficiario.telefonoFijo ?? ""

                },
                cargo = empleo.cargo,
                comprobantes = empleo.comprobantesPago.Select(z => new ComprobantePagoRequest()
                {
                    deducciones = z.deducciones,
                    fechaPago = z.fechaPago,
                    guid = z.Id,
                    sueldoNeto = z.sueldoNeto,
                    bonificaciones = z.total
                }),
                empresaRequest = new EmpresaRequest()
                {
                    nombre = empleo.empresa.nombre,
                    rtnRequest = new RTNRequest() { RTN = empleo.empresa.Id.rtn }
                },
                auditoriaRequest = new AuditoriaRequest()
                {
                    fechaCreo = empleo.auditoria.fechaCreo,
                    fechaModifico = empleo.auditoria.fechaModifico,
                    usuarioCreo = empleo.auditoria.usuarioCreo,
                    usuarioModifico = empleo.auditoria.usuarioModifico
                },
                sucursalRequest = new SucursalRequest()
                {
                    guid = empleo.sucursal.Id,
                    nombre = empleo.sucursal.nombre,
                    firmaAutorizadaRequest = new FirmaAutorizadaRequest()
                    {
                        IdGuid = empleo.sucursal.firma.Id,
                        fechaCreacion = empleo.sucursal.firma.fechaCreacion,
                        userRequest = new UserRequest()
                        {
                            Id = empleo.sucursal.firma.user.Id
                        }
                    }
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