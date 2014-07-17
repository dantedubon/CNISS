using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Ports.Input.REST.Request.AuditoriaRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.EmpleoRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.EmpresaRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.RolModule;
using CNISS.CommonDomain.Ports.Input.REST.Request.UserRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.VisitaRequest;
using CNISS.EnterpriseDomain.Domain.Entities;
using NUnit.Framework;

namespace CNISS_Tests.Enterprise_Test.Entities_Test.Empleo_Test.Request
{
    public class FichaSupervisionRequest_Test
    {
        private object[] badDataForPost;

        public FichaSupervisionRequest_Test()
        {
            badDataForPost = new object[]
            {
                new object[]
                {
                    "","GPS","Funciones","31804433","31804433",getFirmaAutorizadaRequest(),9,getSupervisorRequest(),Guid.NewGuid(), Guid.NewGuid()
                },
                new object[]
                {
                    "cargo","","Funciones","31804433","31804433",getFirmaAutorizadaRequest(),9,getSupervisorRequest(),Guid.NewGuid(), Guid.NewGuid()
                }
                ,
                new object[]
                {
                    "cargo","GPS","","31804433","31804433",getFirmaAutorizadaRequest(),9,getSupervisorRequest(),Guid.NewGuid(), Guid.NewGuid()
                },
                new object[]
                {
                    "cargo","GPS","Funciones","","31804433",getFirmaAutorizadaRequest(),9,getSupervisorRequest(),Guid.NewGuid(), Guid.NewGuid()
                },
                 new object[]
                {
                    "cargo","GPS","Funciones","XXX","31804433",getFirmaAutorizadaRequest(),9,getSupervisorRequest(),Guid.NewGuid(), Guid.NewGuid()
                },
                 new object[]
                {
                    "cargo","GPS","Funciones","3180443","31804433",getFirmaAutorizadaRequest(),9,getSupervisorRequest(),Guid.NewGuid(), Guid.NewGuid()
                },
                  new object[]
                {
                    "cargo","GPS","Funciones","31804433","",getFirmaAutorizadaRequest(),9,getSupervisorRequest(),Guid.NewGuid(), Guid.NewGuid()
                },
                 new object[]
                {
                    "cargo","GPS","Funciones","31804433","XX",getFirmaAutorizadaRequest(),9,getSupervisorRequest(),Guid.NewGuid(), Guid.NewGuid()
                },
                 new object[]
                {
                    "cargo","GPS","Funciones","31804433","31804",getFirmaAutorizadaRequest(),9,getSupervisorRequest(),Guid.NewGuid(), Guid.NewGuid()
                },

                new object[]
                {
                    "cargo","GPS","Funciones","31804433","31804433",new FirmaAutorizadaRequest(),9,getSupervisorRequest(),Guid.NewGuid(), Guid.NewGuid()
                },
                new object[]
                {
                    "cargo","GPS","Funciones","31804433","31804433",getFirmaAutorizadaRequest(),-1,getSupervisorRequest(),Guid.NewGuid(), Guid.NewGuid()
                },

                 new object[]
                {
                    "cargo","GPS","Funciones","31804433","31804433",getFirmaAutorizadaRequest(),11,getSupervisorRequest(),Guid.NewGuid(), Guid.NewGuid()
                },
                new object[]
                {
                    "cargo","GPS","Funciones","31804433","31804433",getFirmaAutorizadaRequest(),9,new SupervisorRequest(),Guid.NewGuid(), Guid.NewGuid()
                },
                new object[]
                {
                    "cargo","GPS","Funciones","31804433","31804433",getFirmaAutorizadaRequest(),9,getSupervisorRequest(),Guid.Empty, Guid.NewGuid()
                },
                new object[]
                {
                    "cargo","GPS","Funciones","31804433","31804433",getFirmaAutorizadaRequest(),9,getSupervisorRequest(),Guid.NewGuid(), Guid.Empty
                },
            };
        }


        [TestCaseSource("badDataForPost")]
        public void isValidPost_DataInvalid_ReturnFalse(string cargo, string posicionGps, string funciones, string telefonoFijo, string telefonoCelular, FirmaAutorizadaRequest userRequest, int desempeñoEmpleado, SupervisorRequest supervisor, Guid fotografia, Guid empleoId)
        {
            var ficha = new FichaSupervisionEmpleoRequest()
            {
                cargo = cargo,
                posicionGPS = posicionGps,
                funciones = funciones,
                telefonoFijo = telefonoFijo,
                telefonoCelular = telefonoCelular,
                desempeñoEmpleado = desempeñoEmpleado,
                supervisor = supervisor,
                firma = userRequest,
                fotografiaBeneficiario = fotografia,
                empleoId = empleoId
                

            };

            var response = ficha.isValidPost();
            Assert.IsFalse(response);
        }

        [Test]
        public void isValidPost_ValidData_ReturnTrue()
        {
            var ficha = new FichaSupervisionEmpleoRequest()
            {
                cargo = "Cargo",
                posicionGPS = "Posicion GPS",
                funciones = "Funciones",
                telefonoFijo = "31804422",
                telefonoCelular = "31804422",
                desempeñoEmpleado = 9,
                supervisor = getSupervisorRequest(),
                firma = getFirmaAutorizadaRequest(),
                fotografiaBeneficiario = Guid.NewGuid(),
                empleoId = Guid.NewGuid()

            };

            var response = ficha.isValidPost();
            Assert.IsTrue(response);
        }

        private FirmaAutorizadaRequest getFirmaAutorizadaRequest()
        {
            return new FirmaAutorizadaRequest()
            {
                IdGuid = Guid.NewGuid(),
                userRequest = getUserRequest()
            
            };
        }

        private SupervisorRequest getSupervisorRequest()
        {
            return new SupervisorRequest()
            {
                guid = Guid.NewGuid(),
                userRequest = new UserRequest()
                {
                    Id = "User",
               
                    password = "xxxx",
                
                }
            };

        }



        private UserRequest getUserRequest()
        {
            return new UserRequest()
            {
                Id = "User",
                password = "xxxx",
               
            };
        }
    }
}