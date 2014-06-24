using CNISS.EnterpriseDomain.Domain.ValueObjects;

namespace CNISS.EnterpriseDomain.Application
{
    public interface IServiceDireccionValidator
    {
        bool isValidDireccion(Direccion direccion);
    }
}