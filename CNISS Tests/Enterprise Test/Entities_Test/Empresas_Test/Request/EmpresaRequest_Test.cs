using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Ports.Input.REST.Request.EmpresaRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.GremioRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.RolModule;
using CNISS.CommonDomain.Ports.Input.REST.Request.UserRequest;
using CNISS.EnterpriseDomain.Domain.Entities;
using NUnit.Framework;

namespace CNISS_Tests.Enterprise_Test.Entities_Test.Empresas_Test.Request
{
    [TestFixture]
    public class EmpresaRequest_Test
    {
        public Object[] badDataForPost;

        public EmpresaRequest_Test()
        {
            badDataForPost = new object[]
            {
                new Object[]
                {
                    new RTNRequest(),getGremio(),getGoodSucursales(),getActividades(),"Empresa","file"
                },
                new Object[]
                {
                    null,getGremio(),getGoodSucursales(),getActividades(),"Empresa","file"
                },
                new Object[]
                {
                    getValidRTN(),new GremioRequest(),getGoodSucursales(),getActividades(),"Empresa","file"
                },
                new Object[]
                {
                    getValidRTN(),null,getGoodSucursales(),getActividades(),"Empresa","file"
                },
                new Object[]
                {
                    getValidRTN(),getGremio(),getBadSucursales(),getActividades(),"Empresa","file"
                },

                new Object[]
                {
                    getValidRTN(),getGremio(),null,getActividades(),"Empresa","file"
                },
                new Object[]
                {
                    getValidRTN(),getGremio(),getGoodSucursales(),null,"Empresa","file"
                },
                new Object[]
                {
                    getValidRTN(),getGremio(),getGoodSucursales(),getActividades(),string.Empty,"file"
                },
                new Object[]
                {
                    getValidRTN(),getGremio(),getGoodSucursales(),getActividades(),null,"file"
                },
                new Object[]
                {
                    getValidRTN(),getGremio(),getGoodSucursales(),getActividades(),"Empresa",string.Empty
                }
                
            };
        }

        [TestCaseSource("badDataForPost")]
        public void isValidPost_invalidData_returnFalse(RTNRequest rtn, GremioRequest gremio, 
            IEnumerable<SucursalRequest> sucursales,
            IEnumerable<ActividadEconomicaRequest> actividades, string nombre,string file)
        {
            var empresa = new EmpresaRequest()
            {
                actividadEconomicaRequests = actividades,
                contentFile = file,
                empleadosTotales = 0,
                gremioRequest = gremio,
                programaPiloto = true,
                rtnRequest = rtn,
                sucursalRequests = sucursales,
                nombre = nombre
            };

            var respuesta = empresa.isValidPost();

            Assert.IsFalse(respuesta);

        }

        [Test]
        public void isValidPost_validData_returnTrue()
        {
            var empresa = new EmpresaRequest()
            {
                actividadEconomicaRequests = getActividades(),
                contentFile = "file",
                empleadosTotales = 0,
                gremioRequest = getGremio(),
                programaPiloto = true,
                rtnRequest = getValidRTN(),
                sucursalRequests = getGoodSucursales(),
                nombre = "Empresa"
            };

            var respuesta = empresa.isValidPost();

            Assert.IsTrue(respuesta);
        }

        private IEnumerable<ActividadEconomicaRequest> getActividades()
        {
            return new List<ActividadEconomicaRequest>()
            {
                new ActividadEconomicaRequest() {descripcion = "Camarones", guid = Guid.NewGuid()}
            };
        }
        private RTNRequest getValidRTN()
        {
            return new RTNRequest() { RTN = "08011985123960" };
        }

        private IEnumerable<SucursalRequest> getGoodSucursales()
        {
            return new List<SucursalRequest>()
            {
                getSucursalGood(),
                getSucursalGood()
            };
        }

        private IEnumerable<SucursalRequest> getBadSucursales()
        {
            return new List<SucursalRequest>()
            {
                getSucursalGood(),
                getSucursalBad()
            };
        }


        private SucursalRequest getSucursalGood()
        {
            return new SucursalRequest(){direccionRequest = getValidDireccion(),firmaRequest = getUserRequest(),nombre = "El centro"};
        }

        private SucursalRequest getSucursalBad()
        {
            return new SucursalRequest() { direccionRequest = getValidDireccion(), firmaRequest = new UserRequest(), nombre = "El centro" };
        }

        private UserRequest getUserRequest()
        {
            var rol = new RolRequest() { idGuid = Guid.NewGuid() };
            var user = new UserRequest { firstName = "Dante", Id = "DRCD", userRol = rol, mail = "xx", password = "dd", secondName = "Castillo" };
            return user;
        }

       
        private RepresentanteLegalRequest getValidRepresentanteLegal()
        {
            return new RepresentanteLegalRequest()
            {
                identidadRequest = new IdentidadRequest() { identidad = "0801198512396" },
                nombre = "Julio"
            };
        }

        private DireccionRequest getValidDireccion()
        {
            return new DireccionRequest()
            {
                departamentoRequest = new DepartamentoRequest() { idDepartamento = "01", nombre = "departamento" },
                municipioRequest =
                    new MunicipioRequest() { idDepartamento = "01", idMunicipio = "01", nombre = "municipio" },
                descripcion = "Barrio Abajo"
            };
        }

        public GremioRequest getGremio()
        {

           return new GremioRequest(){direccionRequest = getValidDireccion()
               ,nombre = "Camara",
               representanteLegalRequest =getValidRepresentanteLegal(),
               rtnRequest = getValidRTN()};

        }
    }
}