using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Machine.Specifications;

namespace CNISS.CommonDomain.Domain
{
    public abstract class ValueObject<TKey>:IDomainObjectIdentified<TKey>
    {
        public virtual TKey idKey { get;  set; }

      
    }
}