using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Ports.Input.REST.Request.BeneficiarioRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.GremioRequest;
using NUnit.Framework;

namespace CNISS_Tests.Enterprise_Test.Entities_Test.Beneficiario_Test.Request
{
    [TestFixture]
    public class DependienteRequest_Test
    {
        public object[] badRequestPost;

        public DependienteRequest_Test()
        {
            badRequestPost = new object[]
            {
                new Object[]
                {
                    new NombreRequest(),getIdentidadRequest(),getParentescoRequest()
                },
                new Object[]
                {
                    null,getIdentidadRequest(),getParentescoRequest()
                },
                new Object[]
                {
                    getNombreRequest(),new IdentidadRequest(),getParentescoRequest()
                },
                 new Object[]
                {
                    getNombreRequest(),null,getParentescoRequest()
                },
                 new Object[]
                {
                    getNombreRequest(),getIdentidadRequest(),null
                }
            };
        }

         [TestCaseSource("badRequestPost")]
        public void isValidPost_invalidData_returnFalse(NombreRequest nombre, IdentidadRequest identidad, ParentescoRequest parentesco)
        {
            var dependiente = new DependienteRequest()
            {
                identidadRequest = identidad,
                nombreRequest = nombre,
                parentescoRequest = parentesco
            };

            var respuesta = dependiente.isValidPost();

            Assert.IsFalse(respuesta);
        }

        [Test]
        public void isValidPost_validData_returnTrue()
        {
            var dependiente = new DependienteRequest()
            {
                identidadRequest = getIdentidadRequest(),
                nombreRequest = getNombreRequest(),
                parentescoRequest = getParentescoRequest()
            };

            var respuesta = dependiente.isValidPost();

            
            Assert.IsTrue(respuesta);
        }

        private NombreRequest getNombreRequest()
        {
            return new NombreRequest()
            {
                nombres = "Dante Ruben",
                primerApellido = "Castillo",
                segundoApellido = "Dubon"
            };
        }

        private IdentidadRequest getIdentidadRequest()
        {
            return new IdentidadRequest()
            {
                identidad = "0801198512396"
            };
        }

        private ParentescoRequest getParentescoRequest()
        {
            return new ParentescoRequest()
            {
                descripcion = "Esposo"
            };
        }
    }
}