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
           if(entity.Contrato!=null)
              saveContrato(entity.Contrato);
            entity.ComprobantesPago.ForEach(saveImagenesComprobantesPago);
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
            if (comprobantePago.ImagenComprobante != null)
                _session.Save(comprobantePago.ImagenComprobante);
        }


        public void updateContratoEmpleo(Guid entityId, ContentFile contrato)
        {
            var empleo = _session.Get<Empleo>(entityId);
            empleo.Contrato = contrato;
            _session.Save(empleo.Contrato);
            _session.Save(empleo);
        }

        public void updateImagenComprobante(Guid entityId, Guid comprobantePagoId, ContentFile imagenComprobante)
        {
            var empleo = _session.Get<Empleo>(entityId);
            var comprobante = empleo.ComprobantesPago.FirstOrDefault(x => x.Id == comprobantePagoId);
            comprobante.ImagenComprobante = imagenComprobante;
            _session.Save(comprobante.ImagenComprobante);

            _session.Update(empleo);

        }

        public void updateFromMovilNotaDespido(Guid empleoId, NotaDespido notaDespido)
        {
            var empleo = _session.Get<Empleo>(empleoId);
            empleo.NotaDespido = notaDespido;
            if (notaDespido.DocumentoDespido != null)
                _session.Save(notaDespido.DocumentoDespido);
            empleo.Supervisado = true;
            update(empleo);
        }

        public void updateFromMovilVisitaSupervision(Guid empleoId, FichaSupervisionEmpleo fichaSupervision)
        {
            var empleo = _session.Get<Empleo>(empleoId);

            if (fichaSupervision.FotografiaBeneficiario != null)
            {
                _session.Save(fichaSupervision.FotografiaBeneficiario);
            }
            empleo.addFichaSupervision(fichaSupervision);


            empleo.Supervisado = true;
            update(empleo);

        }
    }
}