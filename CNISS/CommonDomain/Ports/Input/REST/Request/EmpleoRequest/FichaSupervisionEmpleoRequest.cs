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
        public virtual Guid fotografiaBeneficiario { get; set; }
        public virtual string posicionGPS { get; set; }
        public virtual string cargo { get; set; }
        public virtual string funciones { get; set; }
        public virtual string telefonoFijo { get; set; }
        public virtual string telefonoCelular { get; set; }
        public virtual FirmaAutorizadaRequest firma { get; set; }
        public virtual int desempeñoEmpleado { get; set; }
        public virtual SupervisorRequest supervisor { get; set; }
        public virtual Guid empleoId { get; set; }


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