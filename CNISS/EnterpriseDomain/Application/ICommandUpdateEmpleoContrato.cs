using System;
using System.Security.Cryptography.X509Certificates;
using CNISS.CommonDomain.Application;
using CNISS.EnterpriseDomain.Domain.Entities;
using CNISS.EnterpriseDomain.Domain.ValueObjects;

namespace CNISS.EnterpriseDomain.Application
{
    public interface ICommandUpdateEmpleoContrato : ICommandUpdateIdentity<Empleo>
    {
        void execute(Guid idEmpleo, ContentFile contrato);
        bool isExecutable(Guid idEmpleo);
    }
}