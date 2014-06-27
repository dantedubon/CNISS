using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNISS.EnterpriseDomain.Domain.ValueObjects
{
    public class DiasLaborables
    {
        public virtual bool lunes { get; set; }
        public virtual bool martes { get; set; }
        public virtual bool miercoles { get; set; }
        public virtual bool jueves { get; set; }
        public virtual bool viernes { get; set; }
        public virtual bool sabado { get; set; }
        public virtual bool domingo { get; set; }
    }
}