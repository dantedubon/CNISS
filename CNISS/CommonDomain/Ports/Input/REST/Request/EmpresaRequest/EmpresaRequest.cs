using System;
using System.Collections.Generic;
using System.Linq;
using CNISS.CommonDomain.Ports.Input.REST.Request.GremioRequest;


namespace CNISS.CommonDomain.Ports.Input.REST.Request.EmpresaRequest
{
    public class EmpresaRequest:IValidPost
    {
        public IEnumerable<ActividadEconomicaRequest> actividadEconomicaRequests { get; set; }
        public IEnumerable<SucursalRequest> sucursalRequests { get; set; }
        public GremioRequest.GremioRequest gremioRequest { get; set; }
        public int empleadosTotales { get; set; }
        public bool programaPiloto { get; set; }
        public string contentFile { get; set; }
        public RTNRequest rtnRequest { get; set; }
        public DateTime fechaIngreso { get; set; }
        public string nombre { get; set; }


        public bool isValidPost()
        {
            return rtnRequest != null && rtnRequest.isValidPost()
                   && gremioRequest != null && gremioRequest.isValidPost()
                   && sucursalRequests != null && isValidSucursales()
                   && actividadEconomicaRequests != null
                   && !string.IsNullOrEmpty(nombre)
                   &&fechaIngreso >= new DateTime(2010,1,1);


        }

        public bool isValidPostForEmpleo()
        {
            return rtnRequest != null && rtnRequest.isValidPost() && !string.IsNullOrEmpty(nombre);


        }

        private bool isValidSucursales()
        {
            return sucursalRequests.All(x => x.isValidPost());
        }
    }
}