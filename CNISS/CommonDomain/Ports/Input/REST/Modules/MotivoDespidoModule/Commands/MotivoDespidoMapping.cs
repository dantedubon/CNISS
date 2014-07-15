using CNISS.CommonDomain.Domain;
using CNISS.CommonDomain.Ports.Input.REST.Request.EmpleoRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.MotivoDespidoRequest;
using CNISS.EnterpriseDomain.Domain.ValueObjects;

namespace CNISS.CommonDomain.Ports.Input.REST.Modules.MotivoDespidoModule.Commands
{
    public class MotivoDespidoMapping
    {
        public MotivoDespidoMapping()
        {
        }


        public MotivoDespido getMotivoDespidoForPut(MotivoDespidoRequest motivoDespidoRequest)
        {
            var motivoDespido = getMotivoDespidoForPost(motivoDespidoRequest);
            motivoDespido.Id = motivoDespidoRequest.IdGuid;
           
            return motivoDespido;
        }
        public MotivoDespido getMotivoDespidoForPost(MotivoDespidoRequest motivoDespidoRequest)
        {
            var motivoDespido = new MotivoDespido(motivoDespidoRequest.descripcion)
            {
               auditoria = new Auditoria()
               {
                   fechaCreo = motivoDespidoRequest.auditoriaRequest.fechaCreo,
                   fechaModifico = motivoDespidoRequest.auditoriaRequest.fechaModifico,
                   usuarioCreo = motivoDespidoRequest.auditoriaRequest.usuarioCreo,
                   usuarioModifico = motivoDespidoRequest.auditoriaRequest.usuarioModifico
                   
               }
            };
            return motivoDespido;
        }
    }
}