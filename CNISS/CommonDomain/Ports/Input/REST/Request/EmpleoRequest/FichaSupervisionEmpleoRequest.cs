using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Ports.Input.REST.Request.EmpresaRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.VisitaRequest;

namespace CNISS.CommonDomain.Ports.Input.REST.Request.EmpleoRequest
{
    public class FichaSupervisionEmpleoRequest:IValidPost
    {
        public  Guid fotografiaBeneficiario { get; set; }
        public  string posicionGPS { get; set; }
        public  string cargo { get; set; }
        public  string funciones { get; set; }
        public  string telefonoFijo { get; set; }
        public  string telefonoCelular { get; set; }
        public  FirmaAutorizadaRequest firma { get; set; }
        public  int desempeñoEmpleado { get; set; }
        public  SupervisorRequest supervisor { get; set; }
        public  Guid empleoId { get; set; }
        public AuditoriaRequest.AuditoriaRequest auditoriaRequest { get; set; }

        public BeneficiarioRequest.BeneficiarioRequest beneficiarioRequest { get; set; }

        public bool isValidPost()
        {
            return !string.IsNullOrEmpty(cargo)
                   && !string.IsNullOrEmpty(posicionGPS)
                   && !string.IsNullOrEmpty(funciones)
                   && !string.IsNullOrEmpty(telefonoFijo)
                   && !string.IsNullOrEmpty(telefonoCelular)
                   && telefonosNumeric()
                   && telefonosLength()
                  && firma.isValidPostForFichaSupervision()
                   && desempeñoEmpleado > 0 && desempeñoEmpleado <=10
                   && supervisor.isValidPostFichaSupervision()
                   && Guid.Empty!= fotografiaBeneficiario
                   && Guid.Empty!=empleoId
                   && auditoriaRequest.isValidPost()
                   && beneficiarioRequest!=null 
                   &&beneficiarioRequest.isValidPost()
               ;
        }

        private bool telefonosNumeric()
        {
            return telefonoFijo.ToCharArray().All(Char.IsNumber) && telefonoCelular.ToCharArray().All(char.IsNumber);
        }

        private bool telefonosLength()
        {
            return telefonoFijo.Length == telefonoCelular.Length &&  telefonoCelular.Length == 8 ;
        }
    }
}