using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CNISS.AutenticationDomain.Domain.Entities;
using CNISS.CommonDomain.Domain;
using CNISS.EnterpriseDomain.Domain.Entities;

namespace CNISS.EnterpriseDomain.Domain.Repositories
{
    public interface IVisitaRepositoryReadOnly:IRepositoryReadOnly<Visita,Guid>
    {
        IEnumerable<User> usuariosSinVisitaAgendada(DateTime fechaInicial, DateTime fechaFinal);
        IEnumerable<Visita> visitasEntreFechas(DateTime fechaInicial, DateTime fechaFinal);
        Supervisor getAgendaSupervisor(User user);
    }
}
