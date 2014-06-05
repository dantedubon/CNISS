﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace CNISS.CommonDomain.Domain
{
    public interface IDomainObjectIdentified<Tkey>
    {
        Tkey idKey { get; }
    }
}