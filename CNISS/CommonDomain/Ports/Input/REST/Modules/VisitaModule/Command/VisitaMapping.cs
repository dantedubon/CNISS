using System.Collections.Generic;
using System.Linq;
using CNISS.AutenticationDomain.Domain.Entities;
using CNISS.AutenticationDomain.Domain.ValueObjects;
using CNISS.CommonDomain.Domain;
using CNISS.CommonDomain.Ports.Input.REST.Request.EmpresaRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.VisitaRequest;
using CNISS.EnterpriseDomain.Domain;
using CNISS.EnterpriseDomain.Domain.Entities;
using CNISS.EnterpriseDomain.Domain.ValueObjects;

namespace CNISS.CommonDomain.Ports.Input.REST.Modules.VisitaModule.Command
{
    public class VisitaMapping
    {
        public VisitaMapping()
        {
        }

        public Visita getVisitaForPut(VisitaRequest visitaRequest)
        {
            var visita = getVisita(visitaRequest);
            visita.Id = visitaRequest.guid;

            return visita;
        }

        public Visita getVisita(VisitaRequest visitaRequest)
        {
            var auditoria = new Auditoria(visitaRequest.auditoriaRequest.usuarioCreo,
                visitaRequest.auditoriaRequest.fechaCreo, visitaRequest.auditoriaRequest.usuarioModifico,
                visitaRequest.auditoriaRequest.fechaModifico);


            return new Visita(visitaRequest.nombre, visitaRequest.fechaInicial, visitaRequest.fechaFinal)
            {
                Auditoria = auditoria,
                Supervisores = getSupervisores(visitaRequest.supervisoresRequests)

            };
        }

        private  IList<Supervisor> getSupervisores(IEnumerable<SupervisorRequest> supervisorRequests)
        {

            var supervisores = supervisorRequests.Select(
                x =>
                    new Supervisor(new User(x.userRequest.Id, x.userRequest.firstName, x.userRequest.secondName,
                        x.userRequest.password, x.userRequest.mail,
                        new Rol(x.userRequest.userRol.name, x.userRequest.userRol.description)))
                    {
                        Auditoria =
                            new Auditoria(x.auditoriaRequest.usuarioCreo, x.auditoriaRequest.fechaCreo,
                                x.auditoriaRequest.usuarioModifico, x.auditoriaRequest.fechaModifico),
                        LugaresVisitas = getLugaresVisitas(x.lugarVisitaRequests)


                    }).ToList();

            return supervisores;
        }

        private  IList<LugarVisita> getLugaresVisitas(IEnumerable<LugarVisitaRequest> lugarVisitaRequests)
        {
            return
                lugarVisitaRequests.Select(
                    x => new LugarVisita(getEmpresa(x.empresaRequest), getSucursal(x.sucursalRequest))
                    {
                        Auditoria = new Auditoria(x.auditoriaRequest.usuarioCreo,x.auditoriaRequest.fechaCreo,x.auditoriaRequest.usuarioModifico,x.auditoriaRequest.fechaModifico)
                    }).ToList();
        }

        private  Empresa getEmpresa(EmpresaRequest empresaRequest)
        {
            var empresa = new Empresa(new RTN(empresaRequest.rtnRequest.RTN), empresaRequest.nombre,
                empresaRequest.fechaIngreso, new GremioNull());
            return empresa;
        }

        private  Sucursal getSucursal(SucursalRequest sucursalRequest)
        {
            var sucursal = new Sucursal(sucursalRequest.nombre, new DireccionNull(), new FirmaAutorizadaNull())
            {
                Id = sucursalRequest.guid
            };

            return sucursal;
        }
    }
}