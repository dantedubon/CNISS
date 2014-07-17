using System;
using System.Collections.Generic;
using System.Linq;
using CNISS.CommonDomain.Domain;
using CNISS.CommonDomain.Ports.Input.REST.Request.AuditoriaRequest;
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

            dependientes.ToArray().ForEach( beneficiario.addDependiente);

            beneficiario.auditoria = getAuditoria(request.auditoriaRequest);
            beneficiario.telefonoCelular = request.telefonoCelular;
            beneficiario.telefonoFijo = request.telefonoFijo;
            beneficiario.fotografiaBeneficiario = getFotografia(request);
            beneficiario.direccion = getDireccion(request);
            return beneficiario;
        }

        private Direccion getDireccion(BeneficiarioRequest beneficiarioRequest)
        {
            if (beneficiarioRequest.direccionRequest == null)
            {
                return null;
            }
            var direccionRequest = beneficiarioRequest.direccionRequest;
            var departamento = new Departamento()
            {
                Id = direccionRequest.departamentoRequest.idDepartamento,
                nombre = direccionRequest.departamentoRequest.nombre
            };

            var municipio = new Municipio(direccionRequest.municipioRequest.idMunicipio,
                direccionRequest.municipioRequest.idDepartamento, direccionRequest.municipioRequest.nombre);
            return new Direccion(departamento, municipio, direccionRequest.descripcion);
        }

        private Auditoria getAuditoria(AuditoriaRequest auditoriaRequest)
        {
            return new Auditoria(auditoriaRequest.usuarioCreo,auditoriaRequest.fechaCreo,auditoriaRequest.usuarioModifico,auditoriaRequest.fechaModifico);
        }

        private IEnumerable<Dependiente> getDependientes(IEnumerable<DependienteRequest> dependientes )
        {
            return
                dependientes.Select(
                    getDependiente).ToList();
        }

        private ContentFile getFotografia(BeneficiarioRequest beneficiarioRequest)
        {
            var fotografiaBeneficiario = beneficiarioRequest.fotografiaBeneficiario;
            if (!string.IsNullOrEmpty(fotografiaBeneficiario))
            {
                return new ContentFileNull(Guid.Parse(fotografiaBeneficiario));
            }
            return null;
        }

        private Dependiente getDependiente(DependienteRequest dependienteRequest)
        {
            var dependiente = new Dependiente(getIdentidad(dependienteRequest.identidadRequest),
                getNombre(dependienteRequest.nombreRequest), getParentesco(dependienteRequest.parentescoRequest),
                dependienteRequest.fechaNacimiento);

            if (dependienteRequest.IdGuid != Guid.Empty)
            {
                dependiente.idGuid = dependienteRequest.IdGuid;
            }
            dependiente.auditoria = getAuditoria(dependienteRequest.auditoriaRequest);
            return dependiente;
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