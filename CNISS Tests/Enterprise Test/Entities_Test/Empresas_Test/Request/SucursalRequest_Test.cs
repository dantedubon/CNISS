using System;
using CNISS.CommonDomain.Ports.Input.REST.Request.EmpresaRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.GremioRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.RolModule;
using CNISS.CommonDomain.Ports.Input.REST.Request.UserRequest;
using NUnit.Framework;

namespace CNISS_Tests.Enterprise_Test.Entities_Test.Empresas_Test.Request
{
    [TestFixture]
    public class SucursalRequest_Test
    {
        public object[] badRequestForPost;
        public object[] badRequestForPostEmpleo;

        public SucursalRequest_Test()
        {
            badRequestForPost = new object[]
            {
                new object[]
                {
                     getDireccionRequest(), new UserRequest(),"Sucursal"
                },
                 new object[]
                {
                     getDireccionRequest(), null,"Sucursal"
                },
               
                  new object[]
                {
                     new DireccionRequest(), getUserRequest(),"Sucursal"
                },
                  new object[]
                {
                    null, getUserRequest(),"Sucursal"
                },
                 new object[]
                {
                     getDireccionRequest(), getUserRequest(),string.Empty
                },
                 new object[]
                {
                     getDireccionRequest(), getUserRequest(),null
                }
            };

            badRequestForPostEmpleo = new object[]
            {
                new object[]
                {
                    Guid.Empty,"Sucursal"
                },
                    new object[]
                {
                   Guid.NewGuid(),null
                },
                 new object[]
                {
                   Guid.NewGuid(),""
                }
            };
        }

         [TestCaseSource("badRequestForPost")]
        public void isValidPost_invalidData_returnFalse(DireccionRequest direccion,UserRequest firma,string nombre)
        {
          
            var sucursal = new SucursalRequest() {direccionRequest = direccion, firmaRequest = firma, nombre = nombre};

            var respuesta = sucursal.isValidPost();

            Assert.IsFalse(respuesta);
        }

         [TestCaseSource("badRequestForPostEmpleo")]
         public void isValidPostForPostEmpleo_invalidData_returnFalse(Guid idGuid, string nombre)
         {

             var sucursal = new SucursalRequest()
             {
                 guid = idGuid,
                 nombre = nombre
             };

             var respuesta = sucursal.isValidForPostBasicData();

             Assert.IsFalse(respuesta);
         }

        [Test]
         public void isValidPostForPostEmpleo_validData_returnTrue()
         {

             var sucursal = new SucursalRequest()
             {
                 guid = Guid.NewGuid(),
                 nombre = "Sucursal"
             };

             var respuesta = sucursal.isValidForPostBasicData();

             Assert.IsTrue(respuesta);
         }
        [Test]
        public void isValidPost_validData_returnTrue()
        {
            var direccion = getDireccionRequest();
            var firma = getUserRequest();

            var sucursal = new SucursalRequest()
            {
                direccionRequest = direccion,
                firmaRequest = firma,
                nombre = "Sucursal"
            };

            var respuesta = sucursal.isValidPost();

            Assert.IsTrue(respuesta);
        }
        private UserRequest getUserRequest()
        {
            var rol = new RolRequest() {idGuid = Guid.NewGuid()};
            var user = new UserRequest {firstName = "Dante",Id = "DRCD",userRol = rol,mail = "xx",password = "dd",secondName = "Castillo"};
            return user;
        }

        private DireccionRequest getDireccionRequest()
        {
            var departamento = new DepartamentoRequest() {idDepartamento = "01",nombre =  "Departamento"};
            var municipio = new MunicipioRequest() {idMunicipio = "01", idDepartamento = "01", nombre = "Municipio"};
            var direccion = new DireccionRequest()
            {
                departamentoRequest = departamento,
                municipioRequest = municipio,
                descripcion = "B Abajo"
            };
            return direccion;

        }
    }
}