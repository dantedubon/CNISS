using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Ports.Input.REST.Request.EmpresaRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.VisitaRequest;

namespace CNISS.CommonDomain.Ports.Input.REST.Request.EmpleoRequest
{
    public class NotaDespidoRequest:IValidPost
    {
        public MotivoDespidoRequest.MotivoDespidoRequest motivoDespidoRequest { get; set; }
        public DateTime fechaDespido { get; set; }
        public Guid imagenNotaDespido { get; set; }
        public Guid empleoId { get; set; }
        public string posicionGPS { get; set; }
        public SupervisorRequest supervisorRequest { get; set; }
        public FirmaAutorizadaRequest firmaAutorizadaRequest { get; set; }
        public AuditoriaRequest.AuditoriaRequest auditoriaRequest { get; set; }

        public bool isValidPost()
        {
            return !string.IsNullOrEmpty(posicionGPS) 
                && fechaDespido > new DateTime(2014,1,1)
                && Guid.Empty != imagenNotaDespido
                && supervisorRequest != null
                && supervisorRequest.isValidPostFichaSupervision()
                && firmaAutorizadaRequest != null
                && firmaAutorizadaRequest.isValidPostForFichaSupervision()
                && auditoriaRequest!=null
                && auditoriaRequest.isValidPost()
                && motivoDespidoRequest != null
                && motivoDespidoRequest.isValidPostNotaDespido()
                && Guid.Empty != empleoId;
        }

        
    }
}