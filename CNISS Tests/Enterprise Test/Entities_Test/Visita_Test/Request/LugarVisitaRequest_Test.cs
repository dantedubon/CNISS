using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Ports.Input.REST.Request.AuditoriaRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.EmpresaRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.GremioRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.VisitaRequest;
using NUnit.Framework;

namespace CNISS_Tests.Enterprise_Test.Entities_Test.Visita_Test.Request
{
    [TestFixture]
    public class LugarVisitaRequest_Test
    {
        private object[] badDataForPost;

        public LugarVisitaRequest_Test()
        {
            badDataForPost = new object[]
            {
                new object[]
                {
                    new EmpresaRequest(),getSucursalRequest(),getAuditoriaRequest()
                },
                 new object[]
                {
                    null,getSucursalRequest(),getAuditoriaRequest()
                }
                ,
                 new object[]
                {
                    getEmpresaRequest(),new SucursalRequest(),getAuditoriaRequest()
                } ,
                 new object[]
                {
                    getEmpresaRequest(),null,getAuditoriaRequest()
                },
                 new object[]
                {
                    getEmpresaRequest(),getSucursalRequest(),new AuditoriaRequest()
                },
                 new object[]
                {
                    getEmpresaRequest(),getSucursalRequest(),null
                }


            };
        }

        [TestCaseSource("badDataForPost")]
        public void isValidPost_BadData_ReturnFalse(EmpresaRequest empresa, SucursalRequest sucursal, AuditoriaRequest auditoria)
        {
            var lugarVisita = new LugarVisitaRequest()
            {
                empresaRequest = empresa,
                sucursalRequest = sucursal,
                auditoriaRequest = auditoria

            };

            var response = lugarVisita.isValidPost();

            Assert.IsFalse(response);

        }

        [Test]
        public void isValidPost_ValidData_ReturnTrue()
        {
            var lugarVisita = new LugarVisitaRequest()
            {
                empresaRequest = getEmpresaRequest(),
                sucursalRequest = getSucursalRequest(),
                auditoriaRequest = getAuditoriaRequest()

            };

            var response = lugarVisita.isValidPost();

            Assert.IsTrue(response);
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