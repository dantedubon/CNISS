using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNISS.CommonDomain.Domain
{
    public class Auditoria
    {
        public virtual string CreadoPor { get; set; }
        public virtual DateTime  FechaCreacion { get; set; }
        public virtual string ActualizadoPor { get; set; }
        public virtual DateTime FechaActualizacion { get; set; }

        public Auditoria(string creadoPor, DateTime fechaCreacion, string actualizadoPor, DateTime fechaActualizacion)
        {
            this.CreadoPor = creadoPor;
            this.FechaCreacion = fechaCreacion;
            this.ActualizadoPor = actualizadoPor;
            this.FechaActualizacion = fechaActualizacion;
        }

        public Auditoria()
        {
            
        }
    }
}