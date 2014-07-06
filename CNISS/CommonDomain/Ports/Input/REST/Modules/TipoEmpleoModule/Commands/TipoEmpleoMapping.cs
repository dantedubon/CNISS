using CNISS.CommonDomain.Ports.Input.REST.Request.TipoEmpleoRequest;
using CNISS.EnterpriseDomain.Domain.ValueObjects;

namespace CNISS.CommonDomain.Ports.Input.REST.Modules.TipoEmpleoModule.Commands
{
    public class TipoEmpleoMapping
    {
        public TipoEmpleoMapping()
        {
        }


        public TipoEmpleo getTipoEmpleoForPut(TipoEmpleoRequest tipoEmpleoRequest)
        {
            var tipoEmpleo = getTipoEmpleoForPost(tipoEmpleoRequest);
            tipoEmpleo.Id = tipoEmpleoRequest.IdGuid;
           
            return tipoEmpleo;
        }
        public TipoEmpleo getTipoEmpleoForPost(TipoEmpleoRequest tipoEmpleoRequest)
        {
            var tipoEmpleo = new TipoEmpleo(tipoEmpleoRequest.descripcion)
            {
                auditoria = new Domain.Auditoria(
                    tipoEmpleoRequest.auditoriaRequest.usuarioCreo,
                    tipoEmpleoRequest.auditoriaRequest.fechaCreo,
                    tipoEmpleoRequest.auditoriaRequest.usuarioModifico,
                    tipoEmpleoRequest.auditoriaRequest.fechaModifico

                    )
            };
            return tipoEmpleo;
        }
    }
}