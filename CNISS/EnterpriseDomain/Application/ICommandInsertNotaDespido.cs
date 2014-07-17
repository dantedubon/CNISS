using System;
using CNISS.EnterpriseDomain.Domain.Entities;

namespace CNISS.EnterpriseDomain.Application
{
    public interface ICommandInsertNotaDespido
    {
        bool isExecutable(Guid idEmpleo, NotaDespido notaDespido);
        void execute(Guid idEmpleo, NotaDespido notaDespido);

    }
}