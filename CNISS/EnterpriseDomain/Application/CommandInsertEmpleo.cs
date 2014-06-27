using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Application;
using CNISS.EnterpriseDomain.Domain.Entities;

namespace CNISS.EnterpriseDomain.Application
{
    public class CommandInsertEmpleo:ICommandInsertIdentity<Empleo>
    {

        public void execute(Empleo identity)
        {
            throw new NotImplementedException();
        }

        public bool isExecutable(Empleo identity)
        {
            throw new NotImplementedException();
        }
    }
}