using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Ports.Input.REST.Request.GremioRequest;
using Nancy;

namespace CNISS.CommonDomain.Ports.Input.REST.Modules.GremioModule.GremioQuery
{
    public class GremioModuleQuery:NancyModule
    {
        public GremioModuleQuery()
        {
            Get["enterprise/gremio"] = parameters =>
            {
                var respuestaDummy = new GremioRequest();
                var representanteDummy = new RepresentanteLegalRequest();

                var identidad = new IdentidadRequest();
                identidad.identidad = "0801198512396";
                representanteDummy.identidadRequest = identidad;
                representanteDummy.nombre = "Juan Perez";

                var RTN = new RTNRequest();
                RTN.RTN = "08011985123960";

                var municipio = new MunicipioRequest();
                municipio.idDepartamento = "01";
                municipio.idMunicipio = "01";
                municipio.nombre = "municipio";

                var departamento = new DepartamentoRequest();
                departamento.idDepartamento = "01";
                departamento.nombre = "departamento";

                var direccion = new DireccionRequest();
                direccion.municipioRequest = municipio;
                direccion.departamentoRequest = departamento;
                direccion.descripcion = "Barrio Abajo";

                respuestaDummy.rtnRequest = RTN;
                respuestaDummy.representanteLegalRequest = representanteDummy;
                respuestaDummy.nombre = "Camara";
                respuestaDummy.direccionRequest = direccion;
                return Response.AsJson(respuestaDummy);

            };
        }
    }
}