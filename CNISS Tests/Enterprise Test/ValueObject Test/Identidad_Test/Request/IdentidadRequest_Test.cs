using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using CNISS.CommonDomain.Ports.Input.REST.Request.GremioRequest;
using NUnit.Framework;

namespace CNISS_Tests.Enterprise_Test.ValueObject_Test.Identidad_Test.Request
{
    [TestFixture]
    public class IdentidadRequest_Test
    {
        public object[] badRequestForPost =
            { 
                new object[]{"0801"},
                new object[]{string.Empty},
                new object[]{"080119851239A"}
            };

        [TestCaseSource("badRequestForPost")]
        public void isValidPost_InvalidadData_RetornaFalse(string data)
        {
            
            

              var identidad = new IdentidadRequest();
            identidad.identidad = data;

            var respuesta = identidad.isValidPost();

            Assert.IsFalse(respuesta);
        }

        [Test]
        public void isValidPost_ValidData_RetornaVerdadero()
        {
            var identidad = new IdentidadRequest();
            identidad.identidad = "0801198512396";

            Assert.IsTrue(identidad.isValidPost());
        }
    }
}