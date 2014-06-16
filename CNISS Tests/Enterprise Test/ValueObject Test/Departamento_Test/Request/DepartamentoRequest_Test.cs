using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Ports.Input.REST.Request.GremioRequest;
using NUnit.Framework;

namespace CNISS_Tests.Enterprise_Test.ValueObject_Test.DepartamentoRequest_Test.Request
{
    [TestFixture]
    public class DepartamentoRequest_Test
    {

        public object[] badRequestForPost =
            { 
                new object[]{"X","X"},
                new object[]{"1X","X"},
                new object[]{"11",string.Empty}
         
            
            };

         [TestCaseSource("badRequestForPost")]
        public void isValidPost_invalidData_ReturnFalse(string codigoDepartamento, string nombreDepartamento)
        {
            var departamento = new DepartamentoRequest();
            departamento.idDepartamento = codigoDepartamento;
            departamento.nombre = nombreDepartamento;

            var respuesta = departamento.isValidPost();

            Assert.IsFalse(respuesta);
        }

        [Test]
        public void isValidPost_validData_ReturnTrue()
        {
            var departamento = new DepartamentoRequest();
            departamento.idDepartamento = "01";
            departamento.nombre = "01";

            var respuesta = departamento.isValidPost();


            Assert.IsTrue(respuesta);
        }
    }
}