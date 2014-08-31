using System.Collections.Generic;
using System.Linq;
using CNISS.CommonDomain.Domain;
using NHibernate.Mapping;

namespace CNISS.EnterpriseDomain.Domain.ValueObjects
{
    public class Departamento:ValueObject<string>
    {
        public virtual string Nombre { get; set; }
        public virtual IEnumerable<Municipio> Municipios { get;  set; }

        public Departamento()
        {
            Municipios = new List<Municipio>();
        }

       
        public virtual bool isMunicipioFromDepartamento(Municipio municipio)
        {
            return Municipios.Any(x => x.Id == municipio.Id && x.DepartamentoId == municipio.DepartamentoId);
        }

    }

    public class DepartamentoNull:Departamento
    {
        public virtual string Nombre { get { return string.Empty; }  }
        public virtual string Id { get { return string.Empty; }  }
        public virtual IEnumerable<MunicipioNull> Municipios { get { return new List<MunicipioNull>(); }  }
    }
}