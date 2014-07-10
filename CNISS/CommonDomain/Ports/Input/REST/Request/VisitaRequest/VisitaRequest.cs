using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNISS.CommonDomain.Ports.Input.REST.Request.VisitaRequest
{
    public class VisitaRequest:IValidPost
    {
        public string nombre { get; set; }
        public DateTime fechaInicial { get; set; }
        public DateTime fechaFinal { get; set; }
        public AuditoriaRequest.AuditoriaRequest auditoriaRequest { get; set; }
        public IList<SupervisorRequest> supervisoresRequests { get; set; }


        public bool isValidPost()
        {
            return !string.IsNullOrEmpty(nombre) && validDates()
                &&auditoriaRequest != null&&auditoriaRequest.isValidPost()
                &&supervisoresRequests!=null&&validSupervisor();
        }

        private bool validDates()
        {
            var fechaBase = new DateTime(2014, 1, 1);
            return fechaInicial > fechaBase && fechaFinal > fechaBase && fechaInicial< fechaFinal;
        }

        private bool validSupervisor()
        {
            return supervisoresRequests.All(x => x.isValidPost());
        }
    }
}