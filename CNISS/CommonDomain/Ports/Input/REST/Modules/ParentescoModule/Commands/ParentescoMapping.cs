using CNISS.CommonDomain.Domain;
using CNISS.CommonDomain.Ports.Input.REST.Request.BeneficiarioRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.MotivoDespidoRequest;
using CNISS.EnterpriseDomain.Domain.ValueObjects;

namespace CNISS.CommonDomain.Ports.Input.REST.Modules.ParentescoModule.Commands
{
    public class ParentescoMapping
    {
        public ParentescoMapping()
        {
        }


        public Parentesco getParentescoForPut(ParentescoRequest motivoDespidoRequest)
        {
            var motivoDespido = getParentescoForPost(motivoDespidoRequest);
            motivoDespido.Id = motivoDespidoRequest.guid;
           
            return motivoDespido;
        }
        public Parentesco getParentescoForPost(ParentescoRequest motivoDespidoRequest)
        {
            var parentesco = new Parentesco(motivoDespidoRequest.descripcion)
            {
                Auditoria = new Auditoria()
                {
                    FechaCreacion = motivoDespidoRequest.auditoriaRequest.fechaCreo,
                    FechaActualizacion = motivoDespidoRequest.auditoriaRequest.fechaModifico,
                    CreadoPor = motivoDespidoRequest.auditoriaRequest.usuarioCreo,
                    ActualizadoPor = motivoDespidoRequest.auditoriaRequest.usuarioModifico

                }
            };
            
            return parentesco;
        }
    }
}