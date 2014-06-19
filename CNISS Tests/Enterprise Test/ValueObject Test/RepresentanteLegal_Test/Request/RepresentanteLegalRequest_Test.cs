using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Ports.Input.REST.Request.GremioRequest;
using NUnit.Framework;

namespace CNISS_Tests.Enterprise_Test.ValueObject_Test.RepresentanteLegal_Test.Request
{
    [TestFixture]
    public class RepresentanteLegalRequest_Test
    {
        public object[] badRequestForPost =
            { 
                new object[]{new IdentidadRequest(),"Juan"},
                 new object[]{null,"Juan"},
                 new object[]{new IdentidadRequest(){identidad = "0801198512396"},null},
                 new object[]{new IdentidadRequest(){identidad = "0801198512396"},string.Empty}

              
            };

        [TestCaseSource("badRequestForPost")]
        public void isValidPost_invalidaData_returnFalse(IdentidadRequest identidad, string nombre)
        {

            var representante = new RepresentanteLegalRequest {identidadRequest = identidad, nombre = nombre};

            var respuesta = representante.isValidPost();

            Assert.IsFalse(respuesta);


        }
    }
}