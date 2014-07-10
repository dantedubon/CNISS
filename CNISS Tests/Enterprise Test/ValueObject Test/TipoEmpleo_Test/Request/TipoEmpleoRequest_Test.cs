using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Ports.Input.REST.Request.AuditoriaRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.EmpleoRequest;
using NUnit.Framework;

namespace CNISS_Tests.Enterprise_Test.ValueObject_Test.TipoEmpleo_Test.Request
{
    [TestFixture]
    public class TipoEmpleoRequest_Test
    {
        private object[] badDataForPost;
        private object[] badDataForPut;

        public TipoEmpleoRequest_Test()
        {
            badDataForPost = new object[]
            {
                new object[]
                {
                    "",getAuditoriaRequest()
                }, 
                new object[]
                {
                    null,getAuditoriaRequest()
                }, 
                new object[]
                {
                    "tipo",null
                }, 
                new object[]
                {
                    "tipo",new AuditoriaRequest()
                }, 
            };
            badDataForPut = new object[]
            {
                 new object[]
                {
                    null,"tipo",getAuditoriaRequest()
                }, 
                 new object[]
                {
                    Guid.NewGuid(),"",getAuditoriaRequest()
                }, 
                new object[]
                {
                    Guid.NewGuid(),null,getAuditoriaRequest()
                }, 
                new object[]
                {
                    Guid.NewGuid(),"tipo",null
                }, 
                new object[]
                {
                    Guid.NewGuid(),"tipo",new AuditoriaRequest()
                }, 
            };
        }
        [TestCaseSource("badDataForPut")]
        public void isValidPut_invalidaData_returnFalse(Guid idGuid,string descripcion, AuditoriaRequest auditoria)
        {
            var tipoEmpleo = new TipoEmpleoRequest()
            {
                IdGuid = idGuid,
                descripcion = descripcion,
                auditoriaRequest = auditoria
            };

            Assert.IsFalse(tipoEmpleo.isValidPut());
        }


        [TestCaseSource("badDataForPost")]
        public void isValidPost_invalidaData_returnFalse(string descripcion, AuditoriaRequest auditoria)
        {
            var tipoEmpleo = new TipoEmpleoRequest()
            {
                descripcion = descripcion,
                auditoriaRequest = auditoria
            };

            Assert.IsFalse(tipoEmpleo.isValidPost());
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