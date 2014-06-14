using System.Collections.Generic;
using CNISS.CommonDomain.Domain;

namespace CNISS.EnterpriseDomain.Domain.ValueObjects
{
    public class Departamento:ValueObject<string>
    {
        public virtual string nombre { get; set; }
       public virtual IEnumerable<Municipio> municipios { get;  set; }

        public Departamento()
        {
            municipios = new List<Municipio>();
        }
    }
}