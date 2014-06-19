using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Ports.Input.REST.Request.GremioRequest;
using CNISS.EnterpriseDomain.Domain;
using CNISS.EnterpriseDomain.Domain.Entities;
using CNISS.EnterpriseDomain.Domain.Repositories;
using Nancy;
using NHibernate.Criterion;

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
            var direccion = gremio.direccion;
            var departamento = direccion.departamento;
            var municipio = direccion.municipio;

            var rtn = gremio.Id;
            var representante = gremio.representanteLegal;
            var nombreGremio = gremio.nombre;

            var representanteRequest = new RepresentanteLegalRequest()
            {
                identidadRequest = new IdentidadRequest() {identidad = representante.Id.identidad},
                nombre = representante.nombre
            };

            var rtnRequestGremio = new RTNRequest() {RTN = rtn.rtn};
            var departamentoRequestGremio = new DepartamentoRequest()
            {
                idDepartamento = departamento.Id,
                nombre = departamento.nombre
            };

            var municipioRequestGremio = new MunicipioRequest()
            {
                idDepartamento = municipio.departamentoId,
                idMunicipio = municipio.Id,
                nombre = municipio.nombre
            };
            var direccionRequestGremio = new DireccionRequest()
            {
               departamentoRequest = departamentoRequestGremio,
               municipioRequest = municipioRequestGremio,
               descripcion = direccion.referenciaDireccion,
               IdGuid = direccion.Id
            };

            var gremioRequest = new GremioRequest()
            {
                direccionRequest = direccionRequestGremio,
                rtnRequest = rtnRequestGremio,
                representanteLegalRequest = representanteRequest,
                nombre = nombreGremio
            };
            
        
            return gremioRequest;
        }
    }
}