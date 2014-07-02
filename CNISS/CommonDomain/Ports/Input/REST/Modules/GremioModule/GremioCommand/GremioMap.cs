using CNISS.CommonDomain.Domain;
using CNISS.CommonDomain.Ports.Input.REST.Request.AuditoriaRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.GremioRequest;
using CNISS.EnterpriseDomain.Domain;
using CNISS.EnterpriseDomain.Domain.Entities;
using CNISS.EnterpriseDomain.Domain.ValueObjects;

namespace CNISS.CommonDomain.Ports.Input.REST.Modules.GremioModule.GremioCommand
{
    public class GremioMap
    {
        public GremioMap()
        {
        }

        public Gremio getGremioForPost(GremioRequest gremioRequest)
        {
            var representanteLegal = getRepresentanteLegal(gremioRequest);
            var rtn = getRTN(gremioRequest);
            var direccion = getDireccion(gremioRequest);
            var nombre = gremioRequest.nombre;
            var gremio = new Gremio(rtn,representanteLegal,direccion,nombre);
            gremio.auditoria = getAuditoria(gremioRequest.auditoriaRequest);
            return gremio;



        }


        private Auditoria getAuditoria(AuditoriaRequest auditoria)
        {
            return new Auditoria(auditoria.usuarioCreo,auditoria.fechaCreo, auditoria.usuarioModifico,auditoria.fechaModifico);
        }
        private Direccion getDireccion(GremioRequest gremioRequest)
        {
            var direccionRequest = gremioRequest.direccionRequest;
            var departamento = getDepartamento(direccionRequest);
            var municipio = getMunicipio(direccionRequest);
            return new Direccion(departamento,municipio,direccionRequest.descripcion);
        }

        private RTN getRTN(GremioRequest gremioRequest)
        {
            var rtn = new RTN(gremioRequest.rtnRequest.RTN);
            return rtn;
        }

        private RepresentanteLegal getRepresentanteLegal(GremioRequest gremioRequest)
        {
            var rtnRequest = gremioRequest.rtnRequest;
            var representanteRequest = gremioRequest.representanteLegalRequest;
            var identidadRepresentante = representanteRequest.identidadRequest;
            var identidad = new Identidad(identidadRepresentante.identidad);
            

           
            var representanteLegal = new RepresentanteLegal(identidad, representanteRequest.nombre);
            return representanteLegal;
        }

        private Municipio getMunicipio(DireccionRequest direccionRequest)
        {
            var municipio = new Municipio()
            {
                departamentoId = direccionRequest.municipioRequest.idDepartamento,
                Id = direccionRequest.municipioRequest.idMunicipio,
                nombre = direccionRequest.municipioRequest.nombre
            };
            return municipio;
        }

        private Departamento getDepartamento(DireccionRequest direccionRequest)
        {
            var departamento = new Departamento()
            {
                Id = direccionRequest.departamentoRequest.idDepartamento,
                nombre = direccionRequest.departamentoRequest.nombre
            };
            return departamento;
        }
    }
}