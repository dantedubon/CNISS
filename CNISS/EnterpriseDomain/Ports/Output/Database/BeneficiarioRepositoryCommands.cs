using System.Security.Cryptography.X509Certificates;
using CNISS.CommonDomain.Ports.Output.Database;
using CNISS.EnterpriseDomain.Domain.Entities;
using CNISS.EnterpriseDomain.Domain.Repositories;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using NHibernate;

namespace CNISS.EnterpriseDomain.Ports.Output.Database
{
    public class BeneficiarioRepositoryCommands:NHibernateCommandRepository<Beneficiario,Identidad>, IBeneficiarioRepositoryCommands
    {
        public BeneficiarioRepositoryCommands(ISession session) : base(session)
        {

            
        }

      


        public void updateInformationFromMovil(Beneficiario beneficiario)
        {
            if (beneficiario.fotografiaBeneficiario != null)
            {
                saveFotografia(beneficiario.fotografiaBeneficiario);
            }

            
            update(beneficiario);
        }

        private void saveFotografia(ContentFile fotografia)
        {

            _session.Save(fotografia);
        }
    }
}