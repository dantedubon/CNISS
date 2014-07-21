using System;
using CNISS.CommonDomain.Ports.Input.REST.Request.AuditoriaRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.BeneficiarioRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.EmpleoRequest;
using NUnit.Framework;

namespace CNISS_Tests.Parentesco_Test.Request
{
    [TestFixture]
    public class Parentesco_Test
    {
        private object[] badDataForPost;
        private object[] badDataForPut;

        public Parentesco_Test()
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
            var parentescoRequest = new ParentescoRequest()
            {
                guid = idGuid,
                descripcion = descripcion,
                auditoriaRequest = auditoria
            };

            Assert.IsFalse(parentescoRequest.isValidPut());
        }


        [TestCaseSource("badDataForPost")]
        public void isValidPost_invalidaData_returnFalse(string descripcion, AuditoriaRequest auditoria)
        {
            var parentescoRequest = new ParentescoRequest()
            {
                descripcion = descripcion,
                auditoriaRequest = auditoria
            };

            Assert.IsFalse(parentescoRequest.isValidPost());
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