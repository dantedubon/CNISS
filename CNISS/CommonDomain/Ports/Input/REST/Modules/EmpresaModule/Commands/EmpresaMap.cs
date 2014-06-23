using System;
using System.Collections.Generic;
using System.Linq;
using CNISS.AutenticationDomain.Domain.Entities;
using CNISS.AutenticationDomain.Domain.ValueObjects;
using CNISS.CommonDomain.Ports.Input.REST.Modules.GremioModule.GremioCommand;
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

        public Empresa getEmpresa(EmpresaRequest request, byte[] contratoFile)
        {
            var rtn = getRTN(request.rtnRequest);
            var gremio = getGremio(request.gremioRequest);
            var empresa = new Empresa(rtn, request.nombre, request.fechaIngreso, gremio)
            {
                contrato = new ContentFile(contratoFile),
                empleadosTotales = request.empleadosTotales,
                actividadesEconomicas = getActividades(request.actividadEconomicaRequests),
                sucursales = getSucursales(request.sucursalRequests),
                proyectoPiloto = request.programaPiloto
            };


            return empresa;
        }

        private IEnumerable<ActividadEconomica> getActividades(
            IEnumerable<ActividadEconomicaRequest> actividadEconomicaRequests)
        {
            return
                actividadEconomicaRequests.Select(
                    x => new ActividadEconomica() {descripcion = x.descripcion, Id = x.guid}).ToList();
        }

        private IEnumerable<Sucursal> getSucursales(IEnumerable<SucursalRequest> requests)
        {
            return requests.Select(
                getSucursal).ToList();
        }

        private Sucursal getSucursal(SucursalRequest request)
        {
            var direccion = request.direccionRequest;
            var firma = request.firmaRequest;
            return new Sucursal(request.nombre,getDireccion(direccion),getFirmaAutorizada(firma));

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
                departamentoId = request.municipioRequest.idDepartamento,
                Id = request.municipioRequest.idMunicipio
            };
            return new Direccion(departamento,municipio,request.descripcion);
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