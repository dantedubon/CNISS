using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNISS.EnterpriseDomain.Domain.ValueObjects
{
    public class DiasLaborables
    {
        public virtual bool Lunes { get; set; }
        public virtual bool Martes { get; set; }
        public virtual bool Miercoles { get; set; }
        public virtual bool Jueves { get; set; }
        public virtual bool Viernes { get; set; }
        public virtual bool Sabado { get; set; }
        public virtual bool Domingo { get; set; }
    }
}