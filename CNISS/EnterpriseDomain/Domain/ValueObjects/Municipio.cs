using CNISS.CommonDomain.Domain;

namespace CNISS.EnterpriseDomain.Domain.ValueObjects
{
    public class Municipio:ValueObject<string>
    {
        public virtual string DepartamentoId { get; set; } 
        public virtual string Nombre { get; set; }

        public Municipio(string id, string departamentoId, string nombre) : base(id)
        {
            this.DepartamentoId = departamentoId;
            this.Nombre = nombre;
        }


        public Municipio()
        {
            
        }

        #region NHibernate Composite Key Requirements
        public override bool Equals(object obj) {
            if (obj == null) return false;
            var t = obj as Municipio;
            if (t == null) return false;
            if (DepartamentoId == t.DepartamentoId
             && Id == t.Id)
                return true;

            return false;
        }
        public override int GetHashCode() {
            int hash = GetType().GetHashCode();
            hash = (hash * 397) ^ DepartamentoId.GetHashCode();
            hash = (hash * 397) ^ Id.GetHashCode();

            return hash;
        }
        #endregion
    
      
    }

    public class MunicipioNull:Municipio
    {
        public virtual string DepartamentoId { get { return string.Empty; }  }
        public virtual string Id { get { return string.Empty; } }
        public virtual string Nombre { get { return string.Empty; }}
    }
}