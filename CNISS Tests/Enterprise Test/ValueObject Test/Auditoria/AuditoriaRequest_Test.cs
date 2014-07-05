using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Ports.Input.REST.Request.AuditoriaRequest;
using NUnit.Framework;

namespace CNISS_Tests.Enterprise_Test.ValueObject_Test.Auditoria
{
    [TestFixture]
    public class AuditoriaRequest_Test
    {
        private object[] badDataForPost;

        public AuditoriaRequest_Test()
        {
            badDataForPost = new object[]
            {
                new object[]
                {
                    "",new DateTime(2014,8,1),"usuarioModifico",new DateTime(2014,8,1)
                },
                new object[]
                {
                    null,new DateTime(2014,8,1),"usuarioModifico",new DateTime(2014,8,1)
                },
                new object[]
                {
                    "usuarioCreo",null,"usuarioModifico",new DateTime(2014,8,1)
                },
                 new object[]
                {
                    "usuarioCreo",new DateTime(2014,8,1),"",new DateTime(2014,8,1)
                },
                 new object[]
                {
                    "usuarioCreo",new DateTime(2014,8,1),null,new DateTime(2014,8,1)
                },
                    new object[]
                {
                    "usuarioCreo",new DateTime(2014,8,1),"usuarioModifico",null
                }
            };
        }


        [TestCaseSource("badDataForPost")]
        public void isValidPost_invalidData_returnFalse(string usuarioCreo, DateTime fechaCreo, string usuarioModificio, DateTime fechaModifico)
        {
            var auditoria = new AuditoriaRequest()
            {
                fechaCreo = fechaCreo,
                usuarioCreo = usuarioCreo,
                usuarioModifico = usuarioModificio,
                fechaModifico = fechaModifico
            };

            var respuesta = auditoria.isValidPost();

            Assert.IsFalse(respuesta);
        }

    }
}