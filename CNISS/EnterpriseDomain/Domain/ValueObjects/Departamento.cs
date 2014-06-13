using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Domain;
using FluentNHibernate.Testing.Values;
using NHibernate.Proxy;

namespace CNISS.EnterpriseDomain.Domain.ValueObjects
{
    public class Departamento:ValueObject<string>
    {
        public virtual string nombre { get; set; }
       public virtual IEnumerable<Municipio> municipios { get; protected set; }

        protected Departamento()
        {
            municipios = new List<Municipio>();
        }
    }
}