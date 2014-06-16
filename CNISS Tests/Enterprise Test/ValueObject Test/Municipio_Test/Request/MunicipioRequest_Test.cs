using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Ports.Input.REST.Request.GremioRequest;
using NUnit.Framework;

namespace CNISS_Tests.Enterprise_Test.ValueObject_Test.Municipio_Test.Request
{
    [TestFixture]
    public class MunicipioRequest_Test
    {
        public object[] badRequestForPost =
            { 
                new object[]{"1","11","nombre municipio"},
                new object[]{"11","1","nombre municipio"},
                new object[]{"XX","11","nombre municipio"},

                new object[]{"11","XX","nombre municipio"},
                new object[]{"11","11",string.Empty},
                new object[]{string.Empty,string.Empty,string.Empty}
                
         
            
            };

         [TestCaseSource("badRequestForPost")]
        public void isValidPost_invalidData_returnFalse(string idMunicipio, string idDepartamento, string nombre)
        {
            var municipio = new MunicipioRequest();
            municipio.idMunicipio = idMunicipio;
            municipio.idDepartamento = idDepartamento;
            municipio.nombre = nombre;


            var respuesta = municipio.isValidPost();

            Assert.IsFalse(respuesta);

        }

        [Test]
        public void isValidPost_validData_returnTrue()
        {
            var municipio = new MunicipioRequest();
            municipio.idDepartamento = "01";
            municipio.idMunicipio = "23";
            municipio.nombre = "nombre";

            var respuesta = municipio.isValidPost();

            Assert.IsTrue(respuesta);
        }
    }
}