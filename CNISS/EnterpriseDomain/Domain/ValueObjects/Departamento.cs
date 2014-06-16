using System.Collections.Generic;
using System.Linq;
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

        public virtual bool isMunicipioFromDepartamento(Municipio municipio)
        {
            return municipios.Any(x => x.Id == municipio.Id && x.departamentoId == municipio.departamentoId);
        }
    }
}