using System.Linq;
using CNISS.CommonDomain.Ports.Input.REST.Request.AuditoriaRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.GremioRequest;
using CNISS.EnterpriseDomain.Domain;
using CNISS.EnterpriseDomain.Domain.Entities;
using CNISS.EnterpriseDomain.Domain.Repositories;
using Nancy;

namespace CNISS.CommonDomain.Ports.Input.REST.Modules.GremioModule.GremioQuery
{
    public class GremioModuleQuery:NancyModule
    {
        public GremioModuleQuery(IGremioRepositoryReadOnly repository)
        {
            Get["enterprise/gremio"] = parameters =>
            {
                var response = repository.getAll();
                return Response.AsJson(response.Select(convertToGremioRequest))
                    .WithStatusCode(HttpStatusCode.OK);

            };
            Get["enterprise/gremio/id={id}"] = parameters =>
            {

                var idRtn = parameters.id;
                var rtn = new RTNRequest()
                {
                    RTN = idRtn
                };
                if (rtn.isValidPost())
                {
                    RTN idGremio = new RTN(rtn.RTN);
                    if (idGremio.isRTNValid())
                    {
                        var gremio = repository.get(idGremio);
                        if (gremio != null)
                            return Response.AsJson(convertToGremioRequest(gremio));
                    }
                    

                }

                return new Response()
                    .WithStatusCode(HttpStatusCode.NotAcceptable);
            };

        }


        private GremioRequest convertToGremioRequest(Gremio gremio)
        {
            var direccion = gremio.Direccion;
            var departamento = direccion.Departamento;
            var municipio = direccion.Municipio;
            var auditoria = gremio.Auditoria;
            var rtn = gremio.Id;
            var representante = gremio.RepresentanteLegal;
            var nombreGremio = gremio.Nombre;

            
            var representanteRequest = new RepresentanteLegalRequest()
            {
                identidadRequest = new IdentidadRequest() {identidad = representante.Id.identidad},
                nombre = representante.Nombre
            };

            var rtnRequestGremio = new RTNRequest() {RTN = rtn.Rtn};
            var departamentoRequestGremio = new DepartamentoRequest()
            {
                idDepartamento = departamento.Id,
                nombre = departamento.Nombre
            };

            var municipioRequestGremio = new MunicipioRequest()
            {
                idDepartamento = municipio.DepartamentoId,
                idMunicipio = municipio.Id,
                nombre = municipio.Nombre
            };
            var direccionRequestGremio = new DireccionRequest()
            {
               departamentoRequest = departamentoRequestGremio,
               municipioRequest = municipioRequestGremio,
               descripcion = direccion.ReferenciaDireccion,
               IdGuid = direccion.Id
            };

            var auditoriaRequest = new AuditoriaRequest()
            {
                fechaCreo = auditoria.FechaCreacion,
                fechaModifico = auditoria.FechaActualizacion,
                usuarioCreo = auditoria.CreadoPor,
                usuarioModifico = auditoria.ActualizadoPor
            };
            var gremioRequest = new GremioRequest()
            {
                direccionRequest = direccionRequestGremio,
                rtnRequest = rtnRequestGremio,
                representanteLegalRequest = representanteRequest,
                nombre = nombreGremio
            };
            gremioRequest.auditoriaRequest = auditoriaRequest;
        
            return gremioRequest;
        }
    }
}