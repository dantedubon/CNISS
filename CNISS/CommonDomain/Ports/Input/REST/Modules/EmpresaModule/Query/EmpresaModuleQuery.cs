using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Ports.Input.REST.Request.EmpresaRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.GremioRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.RolModule;
using CNISS.CommonDomain.Ports.Input.REST.Request.UserRequest;
using Nancy;

namespace CNISS.CommonDomain.Ports.Input.REST.Modules.EmpresaModule.Query
{
    public class EmpresaModuleQuery:NancyModule
    {
        public EmpresaModuleQuery()
        {
            Get["/enterprise"] = parameters =>
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
                return Response.AsJson(empresa);
            };
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
            return new SucursalRequest() { direccionRequest = getValidDireccion(), firmaRequest = getUserRequest(), nombre = "El centro" };
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

            return new GremioRequest()
            {
                direccionRequest = getValidDireccion()
                ,
                nombre = "Camara",
                representanteLegalRequest = getValidRepresentanteLegal(),
                rtnRequest = getValidRTN()
            };

        }
    }
}