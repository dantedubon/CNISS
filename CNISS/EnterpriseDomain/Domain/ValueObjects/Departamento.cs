using System.Collections.Generic;
using System.Linq;
using CNISS.CommonDomain.Domain;
using NHibernate.Mapping;

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

    public class DepartamentoNull:Departamento
    {
        public virtual string nombre { get { return string.Empty; }  }
        public virtual string Id { get { return string.Empty; }  }
        public virtual IEnumerable<MunicipioNull> municipios { get { return new List<MunicipioNull>(); }  }
    }
}