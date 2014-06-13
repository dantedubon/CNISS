using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Domain;

namespace CNISS.EnterpriseDomain.Domain.Entities
{
    public abstract class EnterpriseEntities:Entity<RTN>
    {
        protected EnterpriseEntities()
        {
            
        }

        protected EnterpriseEntities(RTN RTN)
        {
            if (RTN == null) throw new ArgumentNullException("RTN");
             Id = RTN;
        }
    }
}