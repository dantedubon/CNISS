using CNISS.CommonDomain.Ports.Input.REST.Request.GremioRequest;
using NUnit.Framework;

namespace CNISS_Tests.Enterprise_Test.ValueObject_Test.DireccionTest
{
    [TestFixture]
    public class DireccionRequest_Test
    {

        public object[] badRequestForPost =
        {
            new object[] {new MunicipioRequest(),new DepartamentoRequest(){idDepartamento = "08",nombre ="departamento"},"Barrio Abajo"},
             new object[] {null,new DepartamentoRequest(){idDepartamento = "08",nombre ="departamento"},"Barrio Abajo"},
             new object[]{new MunicipioRequest(){idDepartamento = "01",idMunicipio = "02",nombre = "municipio"},new DepartamentoRequest(),"Barrio Abajo"},
              new object[]{new MunicipioRequest(){idDepartamento = "01",idMunicipio = "02",nombre = "municipio"},null,"Barrio Abajo"},
              new object[]{new MunicipioRequest(){idDepartamento = "01",idMunicipio = "02",nombre = "municipio"},
                  new DepartamentoRequest(){idDepartamento = "08",nombre ="departamento"},null},
              new object[]{new MunicipioRequest(){idDepartamento = "01",idMunicipio = "02",nombre = "municipio"},
                  new DepartamentoRequest(){idDepartamento = "08",nombre ="departamento"},string.Empty}
        };

         [TestCaseSource("badRequestForPost")]
        public void isValidPost_invalidData_ReturnFalse(MunicipioRequest municipio, DepartamentoRequest departamento, string descripcion)
        {
            var direccion = new DireccionRequest();
            direccion.departamentoRequest = departamento;
            direccion.municipioRequest = municipio;
            direccion.descripcion = descripcion;

            var respuesta = direccion.isValidPost();
            Assert.IsFalse(respuesta);

        }
    }
}