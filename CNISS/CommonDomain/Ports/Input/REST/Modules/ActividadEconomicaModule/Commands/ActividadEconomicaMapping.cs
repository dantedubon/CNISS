using CNISS.CommonDomain.Ports.Input.REST.Request.EmpresaRequest;
using CNISS.EnterpriseDomain.Domain.ValueObjects;

namespace CNISS.CommonDomain.Ports.Input.REST.Modules.ActividadEconomicaModule.Commands
{
    public class ActividadEconomicaMapping
    {
        public ActividadEconomicaMapping()
        {
        }

        public ActividadEconomica getActividadEconomicaForPut(ActividadEconomicaRequest actividadEconomicaRequest)
        {
            var actividad = getActividadEconomicaForPost(actividadEconomicaRequest);
            actividad.Id = actividadEconomicaRequest.guid;
            return actividad;
        }

        public  ActividadEconomica getActividadEconomicaForPost(ActividadEconomicaRequest actividadEconomicaRequest)
        {
            var actividad = new ActividadEconomica(actividadEconomicaRequest.descripcion)
            {
                Auditoria = new CNISS.CommonDomain.Domain.Auditoria(
                    actividadEconomicaRequest.auditoriaRequest.usuarioCreo,
                    actividadEconomicaRequest.auditoriaRequest.fechaCreo,
                    actividadEconomicaRequest.auditoriaRequest.usuarioModifico,
                    actividadEconomicaRequest.auditoriaRequest.fechaModifico

                    )
            };
            return actividad;
        }
    }
}