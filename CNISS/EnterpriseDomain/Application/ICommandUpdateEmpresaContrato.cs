using CNISS.EnterpriseDomain.Domain;
using CNISS.EnterpriseDomain.Domain.ValueObjects;

namespace CNISS.EnterpriseDomain.Application
{
    public interface ICommandUpdateEmpresaContrato
    {
        bool isExecutable(RTN empresa);
        void execute(RTN empresa, ContentFile contentFile);
    }
}