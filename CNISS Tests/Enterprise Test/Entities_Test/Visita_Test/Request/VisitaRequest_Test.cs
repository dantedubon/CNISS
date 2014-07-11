using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Ports.Input.REST.Request.AuditoriaRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.EmpresaRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.GremioRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.RolModule;
using CNISS.CommonDomain.Ports.Input.REST.Request.UserRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.VisitaRequest;
using NUnit.Framework;

namespace CNISS_Tests.Enterprise_Test.Entities_Test.Visita_Test.Request
{
    [TestFixture]
    public class VisitaRequest_Test
    {
        private object[] badDataForPost;
        private object[] badDataForPut;

        public VisitaRequest_Test()
        {
            badDataForPost = new object[]
            {
                new object[]
                {
                    "",new DateTime(2014,8,1),new DateTime(2014,8,30),getAuditoriaRequest(),getSupervisorRequests()
                },
                new object[]
                {
                    null,new DateTime(2014,8,1),new DateTime(2014,8,30),getAuditoriaRequest(),getSupervisorRequests()
                },
                new object[]
                {
                    "Gira San Pedro",null,new DateTime(2014,8,30),getAuditoriaRequest(),getSupervisorRequests()
                },
                new object[]
                {
                    "Gira San Pedro",new DateTime(2014,8,1),null,getAuditoriaRequest(),getSupervisorRequests()
                },
                new object[]
                {
                    "Gira San Pedro",new DateTime(2014,9,1),new DateTime(2014,8,30),getAuditoriaRequest(),getSupervisorRequests()
                },
                new object[]
                {
                    "Gira San Pedro",new DateTime(2014,8,1),new DateTime(2014,8,30),new AuditoriaRequest(),getSupervisorRequests()
                },
                  new object[]
                {
                    "Gira San Pedro",new DateTime(2014,8,1),new DateTime(2014,8,30),null,getSupervisorRequests()
                },
                new object[]
                {
                    "Gira San Pedro",new DateTime(2014,8,1),new DateTime(2014,8,30),getAuditoriaRequest(),new List<SupervisorRequest>(){new SupervisorRequest()}
                }
                ,
                new object[]
                {
                    "Gira San Pedro",new DateTime(2014,8,1),new DateTime(2014,8,30),getAuditoriaRequest(),null
                }
            };

            badDataForPut = new object[]
            {
                 new object[]
                {
                   null, "Gira San Pedro",new DateTime(2014,8,1),new DateTime(2014,8,30),getAuditoriaRequest(),getSupervisorRequests()
                },
                 new object[]
                {
                    Guid.NewGuid(),"",new DateTime(2014,8,1),new DateTime(2014,8,30),getAuditoriaRequest(),getSupervisorRequests()
                },
                new object[]
                {
                   Guid.NewGuid(), null,new DateTime(2014,8,1),new DateTime(2014,8,30),getAuditoriaRequest(),getSupervisorRequests()
                },
                new object[]
                {
                    Guid.NewGuid(),"Gira San Pedro",null,new DateTime(2014,8,30),getAuditoriaRequest(),getSupervisorRequests()
                },
                new object[]
                {
                    Guid.NewGuid(),"Gira San Pedro",new DateTime(2014,8,1),null,getAuditoriaRequest(),getSupervisorRequests()
                },
                new object[]
                {
                   Guid.NewGuid(), "Gira San Pedro",new DateTime(2014,9,1),new DateTime(2014,8,30),getAuditoriaRequest(),getSupervisorRequests()
                },
                new object[]
                {
                    Guid.NewGuid(),"Gira San Pedro",new DateTime(2014,8,1),new DateTime(2014,8,30),new AuditoriaRequest(),getSupervisorRequests()
                },
                  new object[]
                {
                  Guid.NewGuid(),  "Gira San Pedro",new DateTime(2014,8,1),new DateTime(2014,8,30),null,getSupervisorRequests()
                },
                new object[]
                {
                  Guid.NewGuid(),  "Gira San Pedro",new DateTime(2014,8,1),new DateTime(2014,8,30),getAuditoriaRequest(),new List<SupervisorRequest>(){new SupervisorRequest()}
                }
                ,
                new object[]
                {
                   Guid.NewGuid(), "Gira San Pedro",new DateTime(2014,8,1),new DateTime(2014,8,30),getAuditoriaRequest(),null
                }
            };
        }


         [TestCaseSource("badDataForPost")]
        public void isValidPost_invalidData_returnFalse(string nombre, DateTime fechaInicial, DateTime fechaFinal, AuditoriaRequest auditoria, IList<SupervisorRequest> supervisoresRequests)
        {
        

            var visita = new VisitaRequest()
            {
                
                auditoriaRequest = auditoria,
                fechaFinal = fechaFinal,
                fechaInicial = fechaInicial,
                nombre = nombre,
                supervisoresRequests = supervisoresRequests
            };

            var respuesta = visita.isValidPost();
            Assert.IsFalse(respuesta);


        }

         [TestCaseSource("badDataForPut")]
         public void isValidPut_invalidData_returnFalse(Guid idGuid,string nombre, DateTime fechaInicial, DateTime fechaFinal, AuditoriaRequest auditoria, IList<SupervisorRequest> supervisoresRequests)
         {


             var visita = new VisitaRequest()
             {
                 guid = idGuid,
                 auditoriaRequest = auditoria,
                 fechaFinal = fechaFinal,
                 fechaInicial = fechaInicial,
                 nombre = nombre,
                 supervisoresRequests = supervisoresRequests
             };

             var respuesta = visita.isValidPut();
             Assert.IsFalse(respuesta);


         }

         [Test]
         public void isValidPost_validData_returnTrue()
         {


             var visita = new VisitaRequest()
             {
                 auditoriaRequest = getAuditoriaRequest(),
                 fechaInicial = new DateTime(2014,8,1),
                 fechaFinal =  new DateTime(2014,8,30),
                 nombre = "Gira San Pedro",
                 supervisoresRequests = getSupervisorRequests()
             };

             var respuesta = visita.isValidPost();
             Assert.IsTrue(respuesta);


         }

        private IList<SupervisorRequest> getSupervisorRequests()
        {
            return new List<SupervisorRequest>()
            {
                new SupervisorRequest()
                {
                    auditoriaRequest = getAuditoriaRequest(),
                    lugarVisitaRequests = new List<LugarVisitaRequest>(){getLugarVisitaRequest()},
                    userRequest = getUserRequest()
                }
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

        private UserRequest getUserRequest()
        {
            var rol = new RolRequest() { idGuid = Guid.NewGuid() };
            var user = new UserRequest { firstName = "Dante", Id = "DRCD", userRol = rol, mail = "xx", password = "dd", secondName = "Castillo" };
            return user;
        }

        private LugarVisitaRequest getLugarVisitaRequest()
        {
            return new LugarVisitaRequest()
            {
                empresaRequest = getEmpresaRequest(),
                sucursalRequest = getSucursalRequest(),
                auditoriaRequest = getAuditoriaRequest()
            };
        }

        private EmpresaRequest getEmpresaRequest()
        {
            return new EmpresaRequest()
            {
                rtnRequest = new RTNRequest() { RTN = "08011985123960" },
                nombre = "Empresa"

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
    }
}