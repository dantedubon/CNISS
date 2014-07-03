using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Application;
using CNISS.CommonDomain.Ports.Input.REST;
using CNISS.CommonDomain.Ports.Input.REST.Modules.EmpleoModule.Commands;
using CNISS.CommonDomain.Ports.Input.REST.Request.AuditoriaRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.BeneficiarioRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.EmpleoRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.EmpresaRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.GremioRequest;
using CNISS.EnterpriseDomain.Domain.Entities;
using Machine.Specifications;
using Moq;
using Nancy;
using Nancy.Testing;
using Should;
using It = Machine.Specifications.It;

namespace CNISS_Tests.Enterprise_Test.Entities_Test.Empleo_Test.Module
{
    [Subject(typeof(EmpleoModuleUpdate))]
    public class when_UserPutEmpleoValidEmpleAndNotUpdateContractNameNotGUID_Should_ReturnError
    {
        static Browser _browser;
        static BrowserResponse _response;
        static ICommandUpdateIdentity<Empleo> _commandUpdate;
        static EmpleoRequest _request;
        static Empleo _expectedEmpleo;


        private Establish context = () =>
        {
            var contrato = new byte[] { 1, 1, 1 };
            _request = new EmpleoRequest()
            {
                IdGuid = Guid.NewGuid(),
                beneficiarioRequest = getBeneficiario(),
                cargo = "ingeniero",
                contrato = "contrato",
                empresaRequest = getEmpresaRequest(),
                fechaDeInicio = new DateTime(2014, 1, 1),
                horarioLaboralRequest = getHorarioLaboralRequest(),
                sucursalRequest = getSucursalRequest(),
                sueldo = 10m,
                tipoEmpleoRequest = getTipoEmpleoRequest(),
                comprobantes = new List<ComprobantePagoRequest>()
               {
                   new ComprobantePagoRequest()
                   {
                       deducciones = 15m,
                       fechaPago =new DateTime(2014,3,2),
                       percepciones = 12m,total = 13m,
                       auditoriaRequest = getAuditoriaRequest()
                   }
               },
                auditoriaRequest = getAuditoriaRequest(),
                updateContrato = false
          

            };

            _commandUpdate = Mock.Of<ICommandUpdateIdentity<Empleo>>();
            Mock.Get(_commandUpdate).Setup(x => x.isExecutable(Moq.It.IsAny<Empleo>())).Returns(true);
            var fileGetter = Mock.Of<IFileGetter>();
        

         
            _browser = new Browser(
                x =>
                {
                    x.Module<EmpleoModuleUpdate>();
                    x.Dependencies(_commandUpdate, fileGetter);
                }

                );


        };

        private Because of = () => { _response = _browser.PutSecureJson("/enterprise/empleos", _request); };

         It should_return_error = () => _response.StatusCode.ShouldEqual(HttpStatusCode.BadRequest);

        private static AuditoriaRequest getAuditoriaRequest()
        {
            return new AuditoriaRequest() { fechaCreo = DateTime.Now, fechaModifico = DateTime.Now, usuarioCreo = "", usuarioModifico = "" };
        }

        private static TipoEmpleoRequest getTipoEmpleoRequest()
        {
            return new TipoEmpleoRequest()
            {
                descripcion = "Por Hora",
                IdGuid = Guid.NewGuid()
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

        private static HorarioLaboralRequest getHorarioLaboralRequest()
        {
            return new HorarioLaboralRequest()
            {
                diasLaborablesRequest = new DiasLaborablesRequest() { lunes = true, martes = true },
                horaEntrada = new HoraRequest() { hora = 2, minutos = 10, parte = "AM" },
                horaSalida = new HoraRequest() { hora = 3, minutos = 10, parte = "PM" }
            };
        }

        private static BeneficiarioRequest getBeneficiario()
        {
            return new BeneficiarioRequest()
            {
                identidadRequest = new IdentidadRequest() { identidad = "0801198512396" },
                fechaNacimiento = new DateTime(1984, 8, 2),
                nombreRequest = new NombreRequest()
                {
                    nombres = "Dante Ruben",
                    primerApellido = "Castillo",
                    segundoApellido = ""

                },
                dependienteRequests = new List<DependienteRequest>()

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