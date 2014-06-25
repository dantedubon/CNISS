using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Ports.Input.REST.Request.BeneficiarioRequest;
using NUnit.Framework;

namespace CNISS_Tests.Enterprise_Test.ValueObject_Test.Nombre_Test
{
    [TestFixture]
    public class NombreRequest_Test
    {
        public Object[] badRequestPost;

        public NombreRequest_Test()
        {
            badRequestPost = new object[]
            {
                new object[]
                {
                    "","apellido", "apellido2"
                },
                 new object[]
                {
                    null,"apellido", "apellido2"
                },
                 new object[]
                {
                    "1","apellido", "apellido2"
                },
                
                 new object[]
                {
                    "1","apellido", "apellido2"
                },
                 new object[]
                {
                    "nombre","", "apellido2"
                },
                new object[]
                {
                    "nombre","1", "apellido2"
                },
                 new object[]
                {
                    "nombre","primerApellido", "1"
                }

            };
        }

        [TestCaseSource("badRequestPost")]
        public void isValidPost_invalidData_returnFalse(string primerNombre, string primerApellido, string segundoApellido)
        {
            var nombre = new NombreRequest()
            {
                nombres = primerNombre,
               
                primerApellido = primerApellido,
                segundoApellido = segundoApellido

            };

            var respuesta = nombre.isValidPost();

            Assert.IsFalse(respuesta);

        }

        [Test]
        public void isValidPost_validData_returnTrue()
        {
            var nombre = new NombreRequest()
            {
                nombres = "Dante Ruben",

                primerApellido = "Castillo",
                segundoApellido = "Dubon"

            };

            var respuesta = nombre.isValidPost();

            Assert.IsTrue(respuesta);
        }
    }
}