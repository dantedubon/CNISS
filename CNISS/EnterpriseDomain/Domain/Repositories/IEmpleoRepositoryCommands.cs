using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using CNISS.CommonDomain.Domain;
using CNISS.EnterpriseDomain.Domain.Entities;
using CNISS.EnterpriseDomain.Domain.ValueObjects;

namespace CNISS.EnterpriseDomain.Domain.Repositories
{
    public interface IEmpleoRepositoryCommands:IRepositoryCommands<Empleo>
    {

        void updateContratoEmpleo(Guid entityId, ContentFile contrato);
        void updateImagenComprobante(Guid entityId, Guid comprobantePagoId, ContentFile imagenComprobante);

    }
}