using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Application;
using CNISS.CommonDomain.Domain;
using CNISS.EnterpriseDomain.Domain.Entities;
using CNISS.EnterpriseDomain.Domain.Repositories;

namespace CNISS.EnterpriseDomain.Application
{
    public class CommandInsertBeneficiario:CommandInsertIdentity<Beneficiario>
    {
        public CommandInsertBeneficiario(IBeneficiarioRepositoryCommands repository, Func<IUnitOfWork> unitOfWork) : base(repository, unitOfWork)
        {
        }


    }
}