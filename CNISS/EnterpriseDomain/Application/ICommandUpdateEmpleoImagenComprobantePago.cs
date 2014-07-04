using System;
using CNISS.CommonDomain.Application;
using CNISS.EnterpriseDomain.Domain.Entities;
using CNISS.EnterpriseDomain.Domain.ValueObjects;

namespace CNISS.EnterpriseDomain.Application
{
    public interface ICommandUpdateEmpleoImagenComprobantePago:ICommandUpdateIdentity<Empleo>
    {
        bool isExecutable(Guid empleoid, Guid comprobanteId);
        void execute(Guid empleoid, Guid comprobanteId, ContentFile contentFile);
    }
}