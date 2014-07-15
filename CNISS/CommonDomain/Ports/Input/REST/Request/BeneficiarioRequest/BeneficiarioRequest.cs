using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Ports.Input.REST.Request.GremioRequest;

namespace CNISS.CommonDomain.Ports.Input.REST.Request.BeneficiarioRequest
{
    public class BeneficiarioRequest:IValidPost
    {
        public NombreRequest nombreRequest { get; set; }
        public IdentidadRequest identidadRequest { get; set; }
        public IEnumerable<DependienteRequest> dependienteRequests { get; set; }
        public DateTime fechaNacimiento { get; set; }
        public AuditoriaRequest.AuditoriaRequest  auditoriaRequest { get; set; }
        public string telefonoFijo { get; set; }
        public string telefonoCelular { get; set; }
        public string fotografiaBeneficiario { get; set; }

        public bool isValidPost()
        {
            return identidadRequest!=null&&identidadRequest.isValidPost()
                &&nombreRequest!=null&& nombreRequest.isValidPost()
                && isValidDependientes();
        }

        

        private bool isValidDependientes()
        {
            return dependienteRequests!=null &&( !dependienteRequests.Any() || dependienteRequests.All(x => x.isValidPost()));
        }
    }
}