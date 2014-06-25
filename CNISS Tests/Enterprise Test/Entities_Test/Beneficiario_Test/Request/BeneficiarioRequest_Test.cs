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
    public class BeneficiarioRequest_Test
    {
        private object[] badRequestForPost;

        public BeneficiarioRequest_Test()
        {
            badRequestForPost = new object[]
            {
                new object[]
                {
                    getDependienteRequest(),getNombreRequest(),new IdentidadRequest()
                },
                 new object[]
                {
                    getDependienteRequest(),getNombreRequest(),null
                },
                 new object[]
                {
                    getDependienteRequest(),new NombreRequest(),getIdentidadRequest()
                },
                 new object[]
                {
                    getDependienteRequest(),null,getIdentidadRequest()
                },
                 new object[]
                {
                    getDependienteRequest(),null,getIdentidadRequest()
                }
                ,
                 new object[]
                {
                    new List<DependienteRequest>(){new DependienteRequest()},getNombreRequest(),getIdentidadRequest()
                },
                 new object[]
                {
                    null,getNombreRequest(),getIdentidadRequest()
                }
                
            };
        }

        [TestCaseSource("badRequestForPost")]
        public void isValidPost_invalidData_returnFalse(IEnumerable<DependienteRequest> dependientes, NombreRequest nombre, IdentidadRequest identidad)
        {
            var beneficiario = new BeneficiarioRequest()
            {
                dependienteRequests = dependientes,
                fechaNacimiento = DateTime.Now,
                identidadRequest = identidad,
                nombreRequest = nombre
            };

            var respuesta = beneficiario.isValidPost();

            Assert.IsFalse(respuesta);

        }


        [Test]
        public void isValidPost_validData_returnTrue()
        {
            var beneficiario = new BeneficiarioRequest()
            {
                dependienteRequests = getDependienteRequest(),
                fechaNacimiento = DateTime.Now,
                identidadRequest = getIdentidadRequest(),
                nombreRequest = getNombreRequest()
            };

            var respuesta = beneficiario.isValidPost();

            Assert.IsTrue(respuesta);
        }

        

        private IEnumerable<DependienteRequest> getDependienteRequest()
        {
            return new List<DependienteRequest>()
            {
                new DependienteRequest()
                {
                    identidadRequest = getIdentidadRequest(),
                    nombreRequest = getNombreRequest(),
                    parentescoRequest = getParentescoRequest()
                }
            };
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