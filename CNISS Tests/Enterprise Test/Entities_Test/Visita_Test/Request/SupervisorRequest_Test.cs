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
    public class SupervisorRequest_Test
    {
        private object[] badDataForPost;
        private object[] badDataForPostFichaSupervision;

        public SupervisorRequest_Test()
        {
            badDataForPost = new object[]
            {
                new object[]
                {
                    new UserRequest(),getLugarVisitaRequests(),getAuditoriaRequest()
                },
                new object[]
                {
                    null,getLugarVisitaRequests(),getAuditoriaRequest()
                },
                new object[]
                {
                    getUserRequest(),new List<LugarVisitaRequest>(){new LugarVisitaRequest()},getAuditoriaRequest()
                },
                new object[]
                {
                    getUserRequest(),null,getAuditoriaRequest()
                },
                new object[]
                {
                    getUserRequest(),getLugarVisitaRequests(),new AuditoriaRequest()
                },
                new object[]
                {
                    getUserRequest(),getLugarVisitaRequests(),null
                }
            };

            badDataForPostFichaSupervision = new object[]
            {
                new object[]
                {
                    null,Guid.NewGuid()
                },
                 new object[]
                {
                    new UserRequest(),Guid.NewGuid()
                }
                ,
                 new object[]
                {
                    getUserRequest(),null
                }
            };
        }

         [TestCaseSource("badDataForPost")]
        public void isValidPost_badDataForPost_returnFalse(UserRequest userRequest, IList<LugarVisitaRequest> lugaresVisita, AuditoriaRequest auditoria)
        {
            var supervisor = new SupervisorRequest()
            {
                userRequest = userRequest,
                lugarVisitaRequests = lugaresVisita,
                auditoriaRequest = auditoria

            };

            var response = supervisor.isValidPost();

            Assert.IsFalse(response);


        }

        [TestCaseSource("badDataForPostFichaSupervision")]
        public void isValidPostFichaSupervision_badDataForPost_returnFalse(UserRequest userRequest, Guid idGuid)
        {
            var supervisor = new SupervisorRequest()
            {
                guid = idGuid,
                userRequest = userRequest
               
            };

            var response = supervisor.isValidPostFichaSupervision();

            Assert.IsFalse(response);   
        }

        private IList<LugarVisitaRequest> getLugarVisitaRequests()
        {
            return new List<LugarVisitaRequest>() {getLugarVisitaRequest()};

        }

        private UserRequest getUserRequest()
        {
            var rol = new RolRequest() { idGuid = Guid.NewGuid() };
            var user = new UserRequest { firstName = "Dante", Id = "DRCD", userRol = rol, mail = "xx", password = "dd", secondName = "Castillo" };
            return user;
        }

        private LugarVisitaRequest getLugarVisitaRequest()
        {
            return  new LugarVisitaRequest()
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
    }
}