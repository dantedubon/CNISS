using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Ports.Input.REST.Request.GremioRequest;
using CNISS.EnterpriseDomain.Domain.Entities;
using CNISS.EnterpriseDomain.Domain.ValueObjects;


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
        public string nombre { get; set; }


        public bool isValidPost()
        {
            return rtnRequest!=null &&rtnRequest.isValidPost()
               &&gremioRequest!=null && gremioRequest.isValidPost()
               &&sucursalRequests!=null &&isValidSucursales()
               &&actividadEconomicaRequests!=null
               &&!string.IsNullOrEmpty(nombre)
               &&!string.IsNullOrEmpty(contentFile);
                
        }

        private bool isValidSucursales()
        {
            return sucursalRequests.All(x => x.isValidPost());
        }
    }
}