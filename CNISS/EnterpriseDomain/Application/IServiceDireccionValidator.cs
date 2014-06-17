using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using CNISS.EnterpriseDomain.Ports.Output.Database;

namespace CNISS.EnterpriseDomain.Application
{
    public interface IServiceDireccionValidator
    {
        bool isValidDireccion(Direccion direccion);
    }
}