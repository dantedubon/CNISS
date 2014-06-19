using CNISS.EnterpriseDomain.Domain.Repositories;
using CNISS.EnterpriseDomain.Domain.ValueObjects;

namespace CNISS.EnterpriseDomain.Application
{
    public class ServiceDireccionValidator:IServiceDireccionValidator
    {
        private IDepartamentRepositoryReadOnly repositorio { get; set; }

        public ServiceDireccionValidator(IDepartamentRepositoryReadOnly repositorio)
        {
            this.repositorio = repositorio;
           
        }


        public bool isValidDireccion(Direccion direccion)
        {
            var departamentoDireccion = direccion.departamento;
            var municipioDireccion = direccion.municipio;
            var departamento = repositorio.get(departamentoDireccion.Id);
            return departamento != null && departamento.isMunicipioFromDepartamento(municipioDireccion);
        }
    }
}