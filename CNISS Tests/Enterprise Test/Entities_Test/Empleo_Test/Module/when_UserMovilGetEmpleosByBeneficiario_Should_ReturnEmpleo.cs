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

              ).With(x => x.Auditoria = Builder<Auditoria>.CreateNew().Build()).Build();
            empleo.Sucursal.Id = _sucursalGuid;
           
            _empleoExpected = getEmpleoRequests(empleo);

            var tokenizer = Mock.Of<ITokenizer>();
            Mock.Get(tokenizer)
                .Setup(x => x.Detokenize(Moq.It.IsAny<string>(), Moq.It.IsAny<NancyContext>()))
                .Returns(userIdentityMovil);
      
            var encryptRequestProvider = getEncrypter();

            var repository = Mock.Of<IEmpleoRepositoryReadOnly>();
            Mock.Get(repository).Setup(x => x.getEmpleoMasRecienteBeneficiario(empleo.Beneficiario.Id)).Returns(empleo);


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


        private static EmpleoRequest getEmpleoRequests(Empleo empleo)
        {
            return new EmpleoRequest()
            {
                beneficiarioRequest = new BeneficiarioRequest()
                {
                    identidadRequest = new IdentidadRequest() { identidad = empleo.Beneficiario.Id.identidad },
                    nombreRequest = new NombreRequest()
                    {
                        nombres = empleo.Beneficiario.Nombre.Nombres,
                        primerApellido = empleo.Beneficiario.Nombre.PrimerApellido,
                        segundoApellido = empleo.Beneficiario.Nombre.SegundoApellido
                    },
                    fechaNacimiento = empleo.Beneficiario.FechaNacimiento,
                    dependienteRequests = getDependienteRequests(empleo.Beneficiario.Dependientes),
                    direccionRequest = getDireccionRequest(empleo.Beneficiario),
                    telefonoCelular = empleo.Beneficiario.TelefonoCelular ?? "",
                    telefonoFijo = empleo.Beneficiario.TelefonoFijo ?? ""

                },
                cargo = empleo.Cargo,
                comprobantes = empleo.ComprobantesPago.Select(z => new ComprobantePagoRequest()
                {
                    deducciones = z.Deducciones,
                    fechaPago = z.FechaPago,
                    guid = z.Id,
                    sueldoNeto = z.SueldoNeto,
                    bonificaciones = z.Total
                }),
                empresaRequest = new EmpresaRequest()
                {
                    nombre = empleo.Empresa.Nombre,
                    rtnRequest = new RTNRequest() { RTN = empleo.Empresa.Id.Rtn }
                },
                auditoriaRequest = new AuditoriaRequest()
                {
                    fechaCreo = empleo.Auditoria.FechaCreacion,
                    fechaModifico = empleo.Auditoria.FechaActualizacion,
                    usuarioCreo = empleo.Auditoria.CreadoPor,
                    usuarioModifico = empleo.Auditoria.ActualizadoPor
                },
                sucursalRequest = new SucursalRequest()
                {
                    guid = empleo.Sucursal.Id,
                    nombre = empleo.Sucursal.Nombre,
                    firmaAutorizadaRequest = new FirmaAutorizadaRequest()
                    {
                        IdGuid = empleo.Sucursal.Firma.Id,
                        fechaCreacion = empleo.Sucursal.Firma.fechaCreacion,
                        userRequest = new UserRequest()
                        {
                            Id = empleo.Sucursal.Firma.User.Id
                        }
                    }
                },
                fechaDeInicio = empleo.FechaDeInicio,
                horarioLaboralRequest = new HorarioLaboralRequest()
                {
                    diasLaborablesRequest = new DiasLaborablesRequest()
                    {
                        domingo = empleo.HorarioLaboral.DiasLaborables.Domingo,
                        lunes = empleo.HorarioLaboral.DiasLaborables.Lunes,
                        martes = empleo.HorarioLaboral.DiasLaborables.Martes,
                        miercoles = empleo.HorarioLaboral.DiasLaborables.Miercoles,
                        jueves = empleo.HorarioLaboral.DiasLaborables.Jueves,
                        viernes = empleo.HorarioLaboral.DiasLaborables.Viernes,
                        sabado = empleo.HorarioLaboral.DiasLaborables.Sabado
                    },
                    horaEntrada = new HoraRequest()
                    {
                        hora = empleo.HorarioLaboral.HoraEntrada.HoraEntera,
                        minutos = empleo.HorarioLaboral.HoraEntrada.Minutos,
                        parte = empleo.HorarioLaboral.HoraEntrada.Parte

                    },
                    horaSalida = new HoraRequest()
                    {
                        hora = empleo.HorarioLaboral.HoraSalida.HoraEntera,
                        minutos = empleo.HorarioLaboral.HoraSalida.Minutos,
                        parte = empleo.HorarioLaboral.HoraSalida.Parte

                    }
                },
                contrato = empleo.Contrato == null ? "" : empleo.Contrato.Id.ToString(),
                sueldo = empleo.Sueldo,
                tipoEmpleoRequest = new TipoEmpleoRequest()
                {
                    descripcion = empleo.TipoEmpleo.Descripcion,
                    IdGuid = empleo.TipoEmpleo.Id
                },
                IdGuid = empleo.Id
            };

        }
    }
}