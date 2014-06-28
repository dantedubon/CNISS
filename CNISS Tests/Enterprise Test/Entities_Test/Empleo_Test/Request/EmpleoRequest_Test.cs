using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Ports.Input.REST.Request.BeneficiarioRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.EmpleoRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.EmpresaRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.GremioRequest;
using NUnit.Framework;
using List = NHibernate.Mapping.List;

namespace CNISS_Tests.Enterprise_Test.Entities_Test.Empleo_Test.Request
{
    [TestFixture]
    public class EmpleoRequest_Test
    {

        private object[] badRequestForPost;

        public EmpleoRequest_Test()
        {
            badRequestForPost = new object[]
            {
                
                new object[]
                {
                    new EmpresaRequest(),getBeneficiario(),getSucursalRequest(),"Ingeniero",24000.0m,new DateTime(2014,4,1),getHorarioLaboralRequest(),"contrato",getTipoEmpleoRequest(),getGoodComprobantes()
                }, 
                new object[]
                {
                    null,getBeneficiario(),getSucursalRequest(),"Ingeniero",24000.0m,new DateTime(2014,4,1),getHorarioLaboralRequest(),"contrato",getTipoEmpleoRequest(),getGoodComprobantes()
                },
                 new object[]
                {
                    getEmpresaRequest(),new BeneficiarioRequest(),getSucursalRequest(),"Ingeniero",24000.0m,new DateTime(2014,4,1),getHorarioLaboralRequest(),"contrato",getTipoEmpleoRequest(),getGoodComprobantes()
                },
                   new object[]
                {
                    getEmpresaRequest(),null,getSucursalRequest(),"Ingeniero",24000.0m,new DateTime(2014,4,1),getHorarioLaboralRequest(),"contrato",getTipoEmpleoRequest(),getGoodComprobantes()
                },
                new object[]
                {
                    getEmpresaRequest(),getBeneficiario(),new SucursalRequest(),"Ingeniero",24000.0m,new DateTime(2014,4,1),getHorarioLaboralRequest(),"contrato",getTipoEmpleoRequest(),getGoodComprobantes()
                },
                new object[]
                {
                    getEmpresaRequest(),getBeneficiario(),null,"Ingeniero",24000.0m,new DateTime(2014,4,1),getHorarioLaboralRequest(),"contrato",getTipoEmpleoRequest(),getGoodComprobantes()
                },
                new object[]
                {
                    getEmpresaRequest(),getBeneficiario(),getSucursalRequest(),null,24000.0m,new DateTime(2014,4,1),getHorarioLaboralRequest(),"contrato",getTipoEmpleoRequest(),getGoodComprobantes()
                },
                 new object[]
                {
                    getEmpresaRequest(),getBeneficiario(),getSucursalRequest(),"",24000.0m,new DateTime(2014,4,1),getHorarioLaboralRequest(),"contrato",getTipoEmpleoRequest(),getGoodComprobantes()
                },

                 new object[]
                {
                    getEmpresaRequest(),getBeneficiario(),getSucursalRequest(),"Ingeniero",0.0m,new DateTime(2014,4,1),getHorarioLaboralRequest(),"contrato",getTipoEmpleoRequest(),getGoodComprobantes()
                },
                  new object[]
                {
                    getEmpresaRequest(),getBeneficiario(),getSucursalRequest(),"Ingeniero",25000.0m,null,getHorarioLaboralRequest(),"contrato",getTipoEmpleoRequest(),getGoodComprobantes()
                },
              new object[]
                {
                    getEmpresaRequest(),getBeneficiario(),getSucursalRequest(),"Ingeniero",25000.0m,new DateTime(2014,4,1),new HorarioLaboralRequest(),"contrato",getTipoEmpleoRequest(),getGoodComprobantes()
                },
                new object[]
                {
                    getEmpresaRequest(),getBeneficiario(),getSucursalRequest(),"Ingeniero",25000.0m,new DateTime(2014,4,1),null,"contrato",getTipoEmpleoRequest(),getGoodComprobantes()
                },
              
                 new object[]
                {
                    getEmpresaRequest(),getBeneficiario(),getSucursalRequest(),"Ingeniero",25000.0m,new DateTime(2014,4,1),getHorarioLaboralRequest(),"contrato",null,getGoodComprobantes()
                },
                   new object[]
                {
                    getEmpresaRequest(),getBeneficiario(),getSucursalRequest(),"Ingeniero",25000.0m,new DateTime(2014,4,1),getHorarioLaboralRequest(),"contrato",getTipoEmpleoRequest(),getBadComprobantes()
                },
                  new object[]
                {
                    getEmpresaRequest(),getBeneficiario(),getSucursalRequest(),"Ingeniero",25000.0m,new DateTime(2014,4,1),getHorarioLaboralRequest(),"contrato",getTipoEmpleoRequest(),null
                },
            };
        }
        [TestCaseSource("badRequestForPost")]
        public void isValidPost_invalidData_returnFalse(EmpresaRequest empresa,
            BeneficiarioRequest beneficiario, SucursalRequest sucursal,
            string cargo, decimal sueldo, 
            DateTime fechaDeInicio, HorarioLaboralRequest horario, 
            string contrato, TipoEmpleoRequest tipoEmpleo,
            IEnumerable<ComprobantePagoRequest> comprobantes )


        {
            var empleo = new EmpleoRequest()
            {
                beneficiarioRequest = beneficiario,
                cargo = cargo,
                contrato = contrato,
                empresaRequest = empresa,
                fechaDeInicio = fechaDeInicio,
                horarioLaboralRequest = horario,
                IdGuid = Guid.Empty,
                sucursalRequest = sucursal,
                sueldo = sueldo,
                tipoEmpleoRequest = tipoEmpleo,
                comprobantes = comprobantes

            };

            var respuesta = empleo.isValidPost();
            Assert.IsFalse(respuesta);

        }
        [Test]
        public void isValidPost_validData_returnTrue()
        {
            var empleo = new EmpleoRequest()
            {
                beneficiarioRequest = getBeneficiario(),
                cargo = "ingeniero",
                contrato = "contrato",
                empresaRequest = getEmpresaRequest(),
                fechaDeInicio = new DateTime(2014,4,2),
                horarioLaboralRequest = getHorarioLaboralRequest(),
                IdGuid = Guid.Empty,
                sucursalRequest = getSucursalRequest(),
                sueldo = 12000m,
                tipoEmpleoRequest = getTipoEmpleoRequest(),
                comprobantes = new List<ComprobantePagoRequest>()

            };

            var respuesta = empleo.isValidPost();
            Assert.IsTrue(respuesta);

        }




        private TipoEmpleoRequest getTipoEmpleoRequest()
        {
            return new TipoEmpleoRequest()
            {
                descripcion = "Por Hora",
                IdGuid = Guid.NewGuid()
            };
        }

        private EmpresaRequest getEmpresaRequest()
        {
            return new EmpresaRequest()
            {
                rtnRequest = new RTNRequest() { RTN = "08011985123960"},
                nombre = "Empresa"
                
            };
        }

        private HorarioLaboralRequest getHorarioLaboralRequest()
        {
            return new HorarioLaboralRequest()
            {
                diasLaborablesRequest = new DiasLaborablesRequest() { lunes = true,martes=true},
                horaEntrada = new HoraRequest() { hora = 2,minutos = 10,parte = "AM"},
                horaSalida = new HoraRequest() { hora = 3,minutos = 10,parte = "PM"}
            };
        }

        private BeneficiarioRequest getBeneficiario()
        {
            return new BeneficiarioRequest()
            {
                identidadRequest = new IdentidadRequest(){identidad = "0801198512396"},
                fechaNacimiento = new DateTime(1984,8,2),
                nombreRequest = new NombreRequest()
                {
                    nombres = "Dante Ruben",
                    primerApellido = "Castillo",
                    segundoApellido = ""

                },
                dependienteRequests = new List<DependienteRequest>()
            
            };
        }

        private SucursalRequest getSucursalRequest()
        {
            return new SucursalRequest()
            {
                guid = Guid.NewGuid(),
                nombre = "Sucursal"
            };
        }

        private IEnumerable<ComprobantePagoRequest> getGoodComprobantes()
        {
            return new List<ComprobantePagoRequest>()
            {
                new ComprobantePagoRequest()
                {
                    deducciones = 12m,
                    fechaPago = new DateTime(2014, 8, 2),
                    percepciones = 5m,
                    total = 12m
                }

            };
        }

        private IEnumerable<ComprobantePagoRequest> getBadComprobantes()
        {
            return new List<ComprobantePagoRequest>()
            {
                new ComprobantePagoRequest()

            };
        }
    }
}