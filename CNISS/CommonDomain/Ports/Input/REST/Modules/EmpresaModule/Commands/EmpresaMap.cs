using System;
using System.Collections.Generic;
using System.Linq;
using CNISS.AutenticationDomain.Domain.Entities;
using CNISS.AutenticationDomain.Domain.ValueObjects;
using CNISS.CommonDomain.Domain;
using CNISS.CommonDomain.Ports.Input.REST.Modules.GremioModule.GremioCommand;
using CNISS.CommonDomain.Ports.Input.REST.Request.AuditoriaRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.EmpresaRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.GremioRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.UserRequest;
using CNISS.EnterpriseDomain.Domain;
using CNISS.EnterpriseDomain.Domain.Entities;
using CNISS.EnterpriseDomain.Domain.ValueObjects;

namespace CNISS.CommonDomain.Ports.Input.REST.Modules.EmpresaModule.Commands
{
    public class EmpresaMap
    {
        public EmpresaMap()
        {
        }

        public Empresa getEmpresa(EmpresaRequest request)
        {
            var rtn = getRTN(request.rtnRequest);
            var gremio = getGremio(request.gremioRequest);
            var empresa = new Empresa(rtn, request.nombre, request.fechaIngreso, gremio)
            {
                
                EmpleadosTotales = request.empleadosTotales,
                ActividadesEconomicas = getActividades(request.actividadEconomicaRequests),
                Sucursales = getSucursales(request.sucursalRequests),
                ProyectoPiloto = request.programaPiloto,
                Contrato = getContrato(request)
                
            };
            empresa.Auditoria = getAuditoria(request.auditoriaRequest);
            
            return empresa;
        }

        private ContentFile getContrato(EmpresaRequest empresaRequest)
        {
            var contrato = empresaRequest.contentFile;
            if (!string.IsNullOrEmpty(contrato))
            {
                return new ContentFileNull(Guid.Parse(contrato));
            }
            return null;
        }

        private Auditoria getAuditoria(AuditoriaRequest auditoriaRequest)
        {
            return new Auditoria(auditoriaRequest.usuarioCreo,auditoriaRequest.fechaCreo, auditoriaRequest.usuarioModifico, auditoriaRequest.fechaModifico);
        }

        private IEnumerable<ActividadEconomica> getActividades(
            IEnumerable<ActividadEconomicaRequest> actividadEconomicaRequests)
        {
            return
                actividadEconomicaRequests.Select(
                    x => new ActividadEconomica() {Descripcion = x.descripcion, Id = x.guid}).ToList();
        }

        private IList<Sucursal> getSucursales(IEnumerable<SucursalRequest> requests)
        {
            return requests.Select(
                getSucursal).ToList();
        }

        private Sucursal getSucursal(SucursalRequest request)
        {
            var direccion = request.direccionRequest;
            var firma = request.userFirmaRequest;
            var sucursal = new Sucursal(request.nombre, getDireccion(direccion), getFirmaAutorizada(firma));
            if (request.guid != Guid.Empty)
            {
                sucursal.Id = request.guid;
            }

            sucursal.Auditoria = getAuditoria(request.auditoriaRequest);
            return sucursal;

        }

        private FirmaAutorizada getFirmaAutorizada(UserRequest request)
        {
            var rolRequest = request.userRol;
            var rol = new Rol(rolRequest.name, rolRequest.description) {Id = rolRequest.idGuid};
            var user = new User(request.Id, request.firstName, request.secondName, "", request.mail, rol);
           
            return new FirmaAutorizada(user,DateTime.Now);
        }

        private Direccion getDireccion(DireccionRequest request)
        {
            var departamento = new Departamento() {Id = request.departamentoRequest.idDepartamento};
            var municipio = new Municipio()
            {
                DepartamentoId = request.municipioRequest.idDepartamento,
                Id = request.municipioRequest.idMunicipio
            };
            var direccion = new Direccion(departamento,municipio,request.descripcion);
            if (request.IdGuid != Guid.Empty)
                direccion.Id = request.IdGuid;
            return direccion;
        }

        private RTN getRTN(RTNRequest request)
        {
            return new RTN(request.RTN);
        }

        private Gremio getGremio(GremioRequest request)
        {
            var gremioMap = new GremioMap();
            return gremioMap.getGremioForPost(request);
        }
    }
}