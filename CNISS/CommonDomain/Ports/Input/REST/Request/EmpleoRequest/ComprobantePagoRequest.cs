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
        public  decimal percepciones { get;  set; }
        public  decimal total { get;  set; }
        public  string archivoComprobante { get;  set; }
        public byte[] contentFile { get; set; }
        public Guid guid { get;  set; }
        public AuditoriaRequest.AuditoriaRequest auditoriaRequest { get; set; }


        public bool isValidPost()
        {
            return esMayorA0(percepciones)&& esMayorA0(deducciones)&&esMayorA0(total)&&fechaPago> new DateTime(2012,1,1);
        }



        private bool esMayorA0(decimal property)
        {
            return property > 0;
        }
    }
}