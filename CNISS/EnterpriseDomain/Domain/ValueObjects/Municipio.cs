using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Domain;

namespace CNISS.EnterpriseDomain.Domain.ValueObjects
{
    public class Municipio:ValueObject<string>
    {
        public virtual string departamentoId { get; set; } 
        public virtual string nombre { get; set; }
    

             #region NHibernate Composite Key Requirements
        public override bool Equals(object obj) {
            if (obj == null) return false;
            var t = obj as Municipio;
            if (t == null) return false;
            if (departamentoId == t.departamentoId
             && Id == t.Id)
                return true;

            return false;
        }
        public override int GetHashCode() {
            int hash = GetType().GetHashCode();
            hash = (hash * 397) ^ departamentoId.GetHashCode();
            hash = (hash * 397) ^ Id.GetHashCode();

            return hash;
        }
        #endregion
    
      
    }
}