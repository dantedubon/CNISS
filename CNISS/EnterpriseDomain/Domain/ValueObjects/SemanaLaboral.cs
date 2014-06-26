using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNISS.EnterpriseDomain.Domain.ValueObjects
{
    public class DiasLaborables
    {
        public virtual byte lunes { get; set; }
        public virtual byte martes { get; set; }
        public virtual byte miercoles { get; set; }
        public virtual byte jueves { get; set; }
        public virtual byte viernes { get; set; }
        public virtual byte sabado { get; set; }
        public virtual byte domingo { get; set; }
    }
}