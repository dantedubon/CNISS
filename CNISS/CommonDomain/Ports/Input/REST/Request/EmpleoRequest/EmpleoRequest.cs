using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Ports.Input.REST.Request.EmpresaRequest;

namespace CNISS.CommonDomain.Ports.Input.REST.Request.EmpleoRequest
{
    public class EmpleoRequest:IValidPost, IValidPut
    {
        public Guid IdGuid { get; set; }
        public EmpresaRequest.EmpresaRequest empresaRequest { get; set; }
        public SucursalRequest sucursalRequest { get; set; }
        public BeneficiarioRequest.BeneficiarioRequest beneficiarioRequest { get; set; }
        public HorarioLaboralRequest horarioLaboralRequest { get; set; }
        public TipoEmpleoRequest.TipoEmpleoRequest tipoEmpleoRequest { get; set; }
        public string cargo { get; set; }
        public decimal sueldo { get; set; }
        public DateTime fechaDeInicio { get; set; }
        public string contrato { get; set; }
        public byte[] contentFile { get; set; }
        public IEnumerable<ComprobantePagoRequest> comprobantes { get; set; }
        public AuditoriaRequest.AuditoriaRequest auditoriaRequest { get; set; }
      

        public bool isValidPost()
        {
            return empresaRequest != null && empresaRequest.isValidPostForEmpleo()
                   && beneficiarioRequest != null && beneficiarioRequest.isValidPost()
                   && sucursalRequest != null && sucursalRequest.isValidForPostEmpleo()
                   && !string.IsNullOrEmpty(cargo) && cargo != null && sueldoMayorA0()
                   && fechaDeInicio >= new DateTime(2012, 1, 1)
                   && horarioLaboralRequest != null && horarioLaboralRequest.isValidPost()
                   && tipoEmpleoRequest != null
                   && comprobantes!=null&& isGoodComprobantes();
              
            ;
        }

        private bool isGoodComprobantes()
        {
            return comprobantes.All(x => x.isValidPost());
        }

        private bool sueldoMayorA0()
        {
            return sueldo > 0;
        }

        public bool isValidPut()
        {
            return IdGuid != Guid.Empty && isValidPost();
        }
    }
}