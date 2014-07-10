using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Ports.Input.REST.Request.AuditoriaRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.EmpresaRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.GremioRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.RolModule;
using CNISS.CommonDomain.Ports.Input.REST.Request.UserRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.VisitaRequest;
using Nancy;

namespace CNISS.CommonDomain.Ports.Input.REST.Modules.VisitaModule.Query
{
    public class VisitaModuleQuery:NancyModule
    {
        public VisitaModuleQuery()
        {
            Get["/visita"] = parameters =>
            {

                var visita = new VisitaRequest()
                {
                    auditoriaRequest = getAuditoriaRequest(),
                    fechaInicial = new DateTime(2014, 8, 1),
                    fechaFinal = new DateTime(2014, 8, 30),
                    nombre = "Gira San Pedro",
                    supervisoresRequests = getSupervisorRequests()
                };

                return Response.AsJson(visita);
            };
        }

        private IList<SupervisorRequest> getSupervisorRequests()
        {
            return new List<SupervisorRequest>()
            {
                new SupervisorRequest()
                {
                    auditoriaRequest = getAuditoriaRequest(),
                    lugarVisitaRequests = new List<LugarVisitaRequest>(){getLugarVisitaRequest()},
                    userRequest = getUserRequest()
                }
            };
        }

        private AuditoriaRequest getAuditoriaRequest()
        {
            return new AuditoriaRequest()
            {
                fechaCreo = new DateTime(2014, 8, 2),
                usuarioCreo = "usuarioCreo",
                fechaModifico = new DateTime(2014, 8, 2),
                usuarioModifico = "usuarioModifico"
            };
        }

        private UserRequest getUserRequest()
        {
            var rol = new RolRequest() { idGuid = Guid.NewGuid() };
            var user = new UserRequest { firstName = "Dante", Id = "DRCD", userRol = rol, mail = "xx", password = "dd", secondName = "Castillo" };
            return user;
        }

        private LugarVisitaRequest getLugarVisitaRequest()
        {
            return new LugarVisitaRequest()
            {
                empresaRequest = getEmpresaRequest(),
                sucursalRequest = getSucursalRequest(),
                auditoriaRequest = getAuditoriaRequest()
            };
        }

        private EmpresaRequest getEmpresaRequest()
        {
            return new EmpresaRequest()
            {
                rtnRequest = new RTNRequest() { RTN = "08011985123960" },
                nombre = "Empresa"

            };
        }

        private SucursalRequest getSucursalRequest()
        {
            return new SucursalRequest()
            {
                guid = Guid.NewGuid(),
                nombre = "Sucursal"
            };
        }
    }
}