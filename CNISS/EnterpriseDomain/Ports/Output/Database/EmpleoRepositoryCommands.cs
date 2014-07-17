using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Ports.Output.Database;
using CNISS.EnterpriseDomain.Domain.Entities;
using CNISS.EnterpriseDomain.Domain.Repositories;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using NHibernate;
using NHibernate.Linq;

namespace CNISS.EnterpriseDomain.Ports.Output.Database
{
    public class EmpleoRepositoryCommands:NHibernateCommandRepository<Empleo,Guid>,IEmpleoRepositoryCommands
    {
        public EmpleoRepositoryCommands(ISession session) : base(session)
        {
        }

        public void save(Empleo entity)
        {
           if(entity.contrato!=null)
              saveContrato(entity.contrato);
            entity.comprobantesPago.ForEach(saveImagenesComprobantesPago);
            _session.Save(entity);
           
        }

        public void update(Empleo entity)
        {
      
            _session.Update(entity);
        }


        private void saveContrato(ContentFile contrato)
        {
           
            _session.Save(contrato);
        }



        private void saveImagenesComprobantesPago(ComprobantePago comprobantePago)
        {
            if (comprobantePago.imagenComprobante != null)
                _session.Save(comprobantePago.imagenComprobante);
        }


        public void updateContratoEmpleo(Guid entityId, ContentFile contrato)
        {
            var empleo = _session.Get<Empleo>(entityId);
            empleo.contrato = contrato;
            _session.Save(empleo.contrato);
            _session.Save(empleo);
        }

        public void updateImagenComprobante(Guid entityId, Guid comprobantePagoId, ContentFile imagenComprobante)
        {
            var empleo = _session.Get<Empleo>(entityId);
            var comprobante = empleo.comprobantesPago.FirstOrDefault(x => x.Id == comprobantePagoId);
            comprobante.imagenComprobante = imagenComprobante;
            _session.Save(comprobante.imagenComprobante);

            _session.Update(empleo);

        }

        public void updateFromMovilNotaDespido(Guid empleoId, NotaDespido notaDespido)
        {
            var empleo = _session.Get<Empleo>(empleoId);
            empleo.notaDespido = notaDespido;
            if (notaDespido.documentoDespido != null)
                _session.Save(notaDespido.documentoDespido);
            empleo.supervisado = true;
            update(empleo);
        }

        public void updateFromMovilVisitaSupervision(Guid empleoId, FichaSupervisionEmpleo fichaSupervision)
        {
            var empleo = _session.Get<Empleo>(empleoId);

            if (fichaSupervision.fotografiaBeneficiario != null)
            {
                _session.Save(fichaSupervision.fotografiaBeneficiario);
            }
            empleo.addFichaSupervision(fichaSupervision);


            empleo.supervisado = true;
            update(empleo);

        }
    }
}