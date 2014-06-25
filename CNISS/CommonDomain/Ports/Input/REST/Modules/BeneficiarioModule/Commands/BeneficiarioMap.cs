using System.Collections.Generic;
using System.Linq;
using CNISS.CommonDomain.Ports.Input.REST.Request.BeneficiarioRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.GremioRequest;
using CNISS.EnterpriseDomain.Domain.Entities;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using NHibernate.Linq;

namespace CNISS.CommonDomain.Ports.Input.REST.Modules.BeneficiarioModule.Commands
{
    public class BeneficiarioMap
    {
        public BeneficiarioMap()
        {
        }

        public Beneficiario getBeneficiario(BeneficiarioRequest request)
        {
            var identidad = request.identidadRequest;
            var nombre = request.nombreRequest;
         
            var fechaNacimiento = request.fechaNacimiento;

            var beneficiario = new Beneficiario(getIdentidad(identidad), getNombre(nombre), fechaNacimiento);

            var dependientes = getDependientes(request.dependienteRequests);

            Enumerable.ToArray<Dependiente>(dependientes).ForEach( beneficiario.addDependiente);

            return beneficiario;
        }

        private IEnumerable<Dependiente> getDependientes(IEnumerable<DependienteRequest> dependientes )
        {
            return
                dependientes.Select(
                    x =>
                        new Dependiente(getIdentidad(x.identidadRequest), getNombre(x.nombreRequest),
                            getParentesco(x.parentescoRequest), x.edad)).ToList();
        }

        private Parentesco getParentesco(ParentescoRequest parentesco)
        {
            var parentescoEntity = new Parentesco(parentesco.descripcion);
            parentescoEntity.Id = parentesco.guid;
            return parentescoEntity;
        }

        private Nombre getNombre(NombreRequest nombre)
        {
            return new Nombre(nombre.nombres,nombre.primerApellido,nombre.segundoApellido);   
        }

        private Identidad getIdentidad(IdentidadRequest identidad)
        {
            return new Identidad(identidad.identidad);
        }
    }
}