using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Ports.Input.REST.Request.AuditoriaRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.BeneficiarioRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.EmpleoRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.EmpresaRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.GremioRequest;
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
                    "","GPS","Funciones","31804433","31804433",getFirmaAutorizadaRequest(),9,getSupervisorRequest(),Guid.NewGuid(), Guid.NewGuid(),getAuditoriaRequest(),getBeneficiarioRequest()
                },
                new object[]
                {
                    "cargo","","Funciones","31804433","31804433",getFirmaAutorizadaRequest(),9,getSupervisorRequest(),Guid.NewGuid(), Guid.NewGuid(),getAuditoriaRequest(),getBeneficiarioRequest()
                }
                ,
                new object[]
                {
                    "cargo","GPS","","31804433","31804433",getFirmaAutorizadaRequest(),9,getSupervisorRequest(),Guid.NewGuid(), Guid.NewGuid(),getAuditoriaRequest(),getBeneficiarioRequest()
                },
                new object[]
                {
                    "cargo","GPS","Funciones","","31804433",getFirmaAutorizadaRequest(),9,getSupervisorRequest(),Guid.NewGuid(), Guid.NewGuid(),getAuditoriaRequest(),getBeneficiarioRequest()
                },
                 new object[]
                {
                    "cargo","GPS","Funciones","XXX","31804433",getFirmaAutorizadaRequest(),9,getSupervisorRequest(),Guid.NewGuid(), Guid.NewGuid(),getAuditoriaRequest(),getBeneficiarioRequest()
                },
                 new object[]
                {
                    "cargo","GPS","Funciones","3180443","31804433",getFirmaAutorizadaRequest(),9,getSupervisorRequest(),Guid.NewGuid(), Guid.NewGuid(),getAuditoriaRequest(),getBeneficiarioRequest()
                },
                  new object[]
                {
                    "cargo","GPS","Funciones","31804433","",getFirmaAutorizadaRequest(),9,getSupervisorRequest(),Guid.NewGuid(), Guid.NewGuid(),getAuditoriaRequest(),getBeneficiarioRequest()
                },
                 new object[]
                {
                    "cargo","GPS","Funciones","31804433","XX",getFirmaAutorizadaRequest(),9,getSupervisorRequest(),Guid.NewGuid(), Guid.NewGuid(),getAuditoriaRequest(),getBeneficiarioRequest()
                },
                 new object[]
                {
                    "cargo","GPS","Funciones","31804433","31804",getFirmaAutorizadaRequest(),9,getSupervisorRequest(),Guid.NewGuid(), Guid.NewGuid(),getAuditoriaRequest(),getBeneficiarioRequest()
                },

                new object[]
                {
                    "cargo","GPS","Funciones","31804433","31804433",new FirmaAutorizadaRequest(),9,getSupervisorRequest(),Guid.NewGuid(), Guid.NewGuid(),getAuditoriaRequest(),getBeneficiarioRequest()
                },
                new object[]
                {
                    "cargo","GPS","Funciones","31804433","31804433",getFirmaAutorizadaRequest(),-1,getSupervisorRequest(),Guid.NewGuid(), Guid.NewGuid(),getAuditoriaRequest(),getBeneficiarioRequest()
                },

                 new object[]
                {
                    "cargo","GPS","Funciones","31804433","31804433",getFirmaAutorizadaRequest(),11,getSupervisorRequest(),Guid.NewGuid(), Guid.NewGuid(),getAuditoriaRequest(),getBeneficiarioRequest()
                },
                new object[]
                {
                    "cargo","GPS","Funciones","31804433","31804433",getFirmaAutorizadaRequest(),9,new SupervisorRequest(),Guid.NewGuid(), Guid.NewGuid(),getAuditoriaRequest(),getBeneficiarioRequest()
                },
                new object[]
                {
                    "cargo","GPS","Funciones","31804433","31804433",getFirmaAutorizadaRequest(),9,getSupervisorRequest(),Guid.Empty, Guid.NewGuid(),getAuditoriaRequest(),getBeneficiarioRequest()
                },
                new object[]
                {
                    "cargo","GPS","Funciones","31804433","31804433",getFirmaAutorizadaRequest(),9,getSupervisorRequest(),Guid.NewGuid(), Guid.Empty,getAuditoriaRequest(),getBeneficiarioRequest()
                },
                new object[]
                {
                    "cargo","GPS","Funciones","31804433","31804433",getFirmaAutorizadaRequest(),9,getSupervisorRequest(),Guid.NewGuid(), Guid.NewGuid(), new AuditoriaRequest(), getBeneficiarioRequest()
                },
                 new object[]
                {
                    "cargo","GPS","Funciones","31804433","31804433",getFirmaAutorizadaRequest(),9,getSupervisorRequest(),Guid.NewGuid(), Guid.NewGuid(), getAuditoriaRequest(), new BeneficiarioRequest()
                }
                ,
                 new object[]
                {
                    "cargo","GPS","Funciones","31804433","31804433",getFirmaAutorizadaRequest(),9,getSupervisorRequest(),Guid.NewGuid(), Guid.NewGuid(), getAuditoriaRequest(), null
                },
                new object[]
                {
                    "cargo","GPS","Funciones","31804433","31804433",null,9,getSupervisorRequest(),Guid.NewGuid(), Guid.NewGuid(), getAuditoriaRequest(), getBeneficiarioRequest()
                }
                ,
                new object[]
                {
                    "cargo","GPS","Funciones","31804433","31804433",getFirmaAutorizadaRequest(),9,null,Guid.NewGuid(), Guid.NewGuid(), getAuditoriaRequest(), getBeneficiarioRequest()
                }
                ,
                new object[]
                {
                    "cargo","GPS","Funciones","31804433","31804433",getFirmaAutorizadaRequest(),9,getSupervisorRequest(),Guid.NewGuid(), Guid.NewGuid(), null, getBeneficiarioRequest()
                }
                  ,
                new object[]
                {
                    "cargo","GPS","Funciones","31804433","31804433",getFirmaAutorizadaRequest(),9,getSupervisorRequest(),Guid.NewGuid(), Guid.NewGuid(), getAuditoriaRequest(), null
                }
            };
        }


        [TestCaseSource("badDataForPost")]
        public void isValidPost_DataInvalid_ReturnFalse(string cargo, string posicionGps, string funciones, string telefonoFijo, string telefonoCelular, FirmaAutorizadaRequest userRequest, int desempeñoEmpleado, SupervisorRequest supervisor, Guid fotografia, Guid empleoId, AuditoriaRequest auditoriaRequest,BeneficiarioRequest beneficiarioRequest)
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
                empleoId = empleoId,
                auditoriaRequest = auditoriaRequest,
                beneficiarioRequest = beneficiarioRequest
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
                empleoId = Guid.NewGuid(),
                auditoriaRequest = getAuditoriaRequest(),
                beneficiarioRequest = getBeneficiarioRequest()

            };

            var response = ficha.isValidPost();
            Assert.IsTrue(response);
        }

        private BeneficiarioRequest getBeneficiarioRequest()
        {
            var beneficiario = new BeneficiarioRequest()
            {
                dependienteRequests = getDependienteRequest(),
                fechaNacimiento = DateTime.Now.Date,
                identidadRequest = getIdentidadRequest(),
                nombreRequest = getNombreRequest()
            };
            return beneficiario;
        }

        private FirmaAutorizadaRequest getFirmaAutorizadaRequest()
        {
            return new FirmaAutorizadaRequest()
            {
                IdGuid = Guid.NewGuid(),
                userRequest = getUserRequest()
            
            };
        }


        private AuditoriaRequest getAuditoriaRequest()
        {
            return new AuditoriaRequest()
            {
                fechaCreo = new DateTime(2014, 8, 2),
                usuarioCreo = "usuarioCreo",
                fechaModifico = new DateTime(2014, 8, 2),
                usuarioModifico = "usuarioModifico"
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

        private IEnumerable<DependienteRequest> getDependienteRequest()
        {
            return new List<DependienteRequest>()
            {
                new DependienteRequest()
                {
                    identidadRequest = getIdentidadRequest(),
                    nombreRequest = getNombreRequest(),
                    parentescoRequest = getParentescoRequest()
                }
            };
        }

        private NombreRequest getNombreRequest()
        {
            return new NombreRequest()
            {
                nombres = "Dante Ruben",
                primerApellido = "Castillo",
                segundoApellido = "Dubon"
            };
        }

        private IdentidadRequest getIdentidadRequest()
        {
            return new IdentidadRequest()
            {
                identidad = "0801198512396"
            };
        }

        private ParentescoRequest getParentescoRequest()
        {
            return new ParentescoRequest()
            {
                descripcion = "Esposo"
            };
        }
    }
}