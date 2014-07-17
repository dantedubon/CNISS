using System;
using CNISS.CommonDomain.Ports.Input.REST.Request.AuditoriaRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.MotivoDespidoRequest;
using NUnit.Framework;

namespace CNISS_Tests.MotivoDespido_Test.Request
{
    [TestFixture]
    public class MotivoDespidoRequest_Test
    {
        private object[] badDataForPost;
        private object[] badDataForPut;

        public MotivoDespidoRequest_Test()
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
            var motivoDespidoRequest = new MotivoDespidoRequest()
            {
                IdGuid = idGuid,
                descripcion = descripcion,
                auditoriaRequest = auditoria
            };

            Assert.IsFalse(motivoDespidoRequest.isValidPut());
        }


        [TestCaseSource("badDataForPost")]
        public void isValidPost_invalidaData_returnFalse(string descripcion, AuditoriaRequest auditoria)
        {
            var motivoDespidoRequest = new MotivoDespidoRequest()
            {
                descripcion = descripcion,
                auditoriaRequest = auditoria
            };

            Assert.IsFalse(motivoDespidoRequest.isValidPost());
        }

        [Test]
        public void isValidPostNotaDespido_invalidData_returnFalse()
        {
            var motivoDespidoRequest = new MotivoDespidoRequest();
         

            Assert.IsFalse(motivoDespidoRequest.isValidPostNotaDespido());
        }


        [Test]
        public void isValidPostNotaDespido_validData_returnFalse()
        {
            var motivoDespidoRequest = new MotivoDespidoRequest()
            {
                IdGuid = Guid.NewGuid()
            };


            Assert.IsTrue(motivoDespidoRequest.isValidPostNotaDespido());
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