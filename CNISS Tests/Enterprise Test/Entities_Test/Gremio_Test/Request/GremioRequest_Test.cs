using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Ports.Input.REST.Request.GremioRequest;
using NUnit.Framework;

namespace CNISS_Tests.Enterprise_Test.Entities_Test.Gremio_Test.RequestTest
{
    [TestFixture]
    public class GremioRequest_Test
    {
        public object[] badRequestForPost;
        public object[] badRequestForPutRepresentante;

        public GremioRequest_Test()
        {
            badRequestForPost = new object[]
            {
                new object[]
                {
                    new RepresentanteLegalRequest(),getValidRTN(),getValidDireccion(),"Gremio"
                },
                new object[]
                {
                    null,getValidRTN(),getValidDireccion(),"Gremio"
                },
                new object[]
                {
                    getValidRepresentanteLegal(),new RTNRequest(),getValidDireccion(),"Gremio"
                },
                new object[]
                {
                    getValidRepresentanteLegal(),null,getValidDireccion(),"Gremio"
                },
                  new object[]
                {
                    getValidRepresentanteLegal(),getValidRTN(),new DireccionRequest(),"Gremio"
                },
                 new object[]
                {
                    getValidRepresentanteLegal(),getValidRTN(),null,"Gremio"
                },
                new object[]
                {
                    getValidRepresentanteLegal(),getValidRTN(),getValidDireccion(),string.Empty
                },
                new object[]
                {
                    getValidRepresentanteLegal(),getValidRTN(),getValidDireccion(),null
                }
            };

            badRequestForPutRepresentante = new object[]
            {
                new object[]
                {
                    getValidRTN(),new RepresentanteLegalRequest()
                },
                new object[]
                {
                    getValidRTN(),null
                },
                 new object[]
                {
                    new RTNRequest(),getValidRepresentanteLegal()
                },
                 new object[]
                {
                    null,getValidRepresentanteLegal()
                }
            };

            
        }

        private RTNRequest getValidRTN()
        {
            return new RTNRequest(){RTN = "08011985123960"};
        }

        private RepresentanteLegalRequest getValidRepresentanteLegal()
        {
            return new RepresentanteLegalRequest()
            {
                identidadRequest = new IdentidadRequest(){identidad = "0801198512396"},
                nombre ="Julio"
            };
        }

        private DireccionRequest getValidDireccion()
        {
            return new DireccionRequest()
            {
                departamentoRequest = new DepartamentoRequest() {idDepartamento = "01", nombre = "departamento"},
                municipioRequest =
                    new MunicipioRequest() {idDepartamento = "01", idMunicipio = "01", nombre = "municipio"},
                descripcion = "Barrio Abajo"
            };
        }

        [TestCaseSource("badRequestForPost")]
        public void isValidRequest_dataInvalid_returnFalse(RepresentanteLegalRequest representante, RTNRequest rtn, DireccionRequest direccion, string nombre)
        {
            var gremio = new GremioRequest();
            gremio.representanteLegalRequest = representante;
            gremio.rtnRequest = rtn;
            gremio.direccionRequest = direccion;
            gremio.nombre = nombre;

            var respuesta = gremio.isValidPost();

            Assert.IsFalse(respuesta);

        }

        [Test]
        public void isValidRequest_dataValid_returnTrue()
        {
            var representante = getValidRepresentanteLegal();
            var rtn = getValidRTN();
            var direccion = getValidDireccion();


            var gremio = new GremioRequest()
            {
                direccionRequest = direccion,
                nombre = "Camara",
                representanteLegalRequest = representante,
                rtnRequest = rtn
            };

            var respuesta = gremio.isValidPost();


            Assert.IsTrue(respuesta);
        }

         [TestCaseSource("badRequestForPutRepresentante")]
        public void isValidPutRepresentante_dataInvalid_returnFalse(RTNRequest rtn,RepresentanteLegalRequest representante)
        {
            var gremio = new GremioRequest();
            gremio.rtnRequest = rtn;
            gremio.representanteLegalRequest = representante;

            var respuesta = gremio.isValidPutRepresentante();

            Assert.IsFalse(respuesta);
        }

        [Test]
        public void isValidPutRepresentante_dataValid_returnTrue()
        {
            var gremio = new GremioRequest();
            gremio.rtnRequest = getValidRTN();
            gremio.representanteLegalRequest = getValidRepresentanteLegal();

            var respuesta = gremio.isValidPutRepresentante();

            Assert.IsTrue(respuesta);
        }
    }
}