using System;
using CNISS.EnterpriseDomain.Domain.Entities;

namespace CNISS.EnterpriseDomain.Application
{
    public interface ICommandInsertFichaDeSupervision
    {
        void execute(FichaSupervisionEmpleo ficha, Beneficiario beneficiario, Guid idEmpleo);
        bool isExecutable(FichaSupervisionEmpleo ficha, Beneficiario beneficiario, Guid idEmpleo);

    }
}