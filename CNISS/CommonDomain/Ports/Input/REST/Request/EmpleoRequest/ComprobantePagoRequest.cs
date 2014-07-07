using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNISS.CommonDomain.Ports.Input.REST.Request.EmpleoRequest
{
    public class ComprobantePagoRequest:IValidPost
    {
        public  DateTime fechaPago { get;  set; }
        public  decimal deducciones { get;  set; }
        public  decimal sueldoNeto { get;  set; }
        public  decimal bonificaciones { get;  set; }
        public  string archivoComprobante { get;  set; }
        public byte[] contentFile { get; set; }
        public Guid guid { get;  set; }
        public AuditoriaRequest.AuditoriaRequest auditoriaRequest { get; set; }
        public bool actualizarArchivoComprobante { get; set; }


        public bool isValidPost()
        {
            return esMayorA0(sueldoNeto)&&fechaPago> new DateTime(2012,1,1);
        }



        private bool esMayorA0(decimal property)
        {
            return property > 0;
        }
    }
}