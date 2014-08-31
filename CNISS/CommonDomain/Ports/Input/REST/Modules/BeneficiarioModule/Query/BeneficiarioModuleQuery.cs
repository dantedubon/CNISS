using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using CNISS.CommonDomain.Ports.Input.REST.Request.AuditoriaRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.BeneficiarioRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.GremioRequest;
using CNISS.EnterpriseDomain.Domain.Entities;
using CNISS.EnterpriseDomain.Domain.Repositories;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using Nancy;
using Nancy.ModelBinding;

namespace CNISS.CommonDomain.Ports.Input.REST.Modules.BeneficiarioModule.Query
{
    public class BeneficiarioModuleQuery:NancyModule
    {
        public BeneficiarioModuleQuery(IBeneficiarioRepositoryReadOnly repository)
        {
            Get["/enterprise/beneficiarios"] = parameters =>
            {
                var beneficiarios = repository.getAll();
                return Response.AsJson(beneficiarios.Select(getShortBeneficiarioRequest).ToList());
            };

            Get["/enterprise/beneficiarios/id="] = parameters =>
            {
                var id = this.Bind<IdentidadRequest>();
                if (id.isValidPost())
                {
                    var identidad = new Identidad(id.identidad);
                    var beneficiario = repository.get(identidad);
                    return Response.AsJson(getLongBeneficiarioRequest(beneficiario))
                        .WithStatusCode(HttpStatusCode.OK);
                }

                return new Response()
                    .WithStatusCode(HttpStatusCode.BadRequest);
            };
        }

        private BeneficiarioRequest getShortBeneficiarioRequest(Beneficiario beneficiario)
        {
            return new BeneficiarioRequest()
            {
                identidadRequest = new IdentidadRequest() { identidad = beneficiario.Id.identidad},
                nombreRequest = new NombreRequest() {
                    nombres = beneficiario.Nombre.Nombres,
                    primerApellido = beneficiario.Nombre.PrimerApellido, 
                    segundoApellido = beneficiario.Nombre.SegundoApellido
                },
                fechaNacimiento = beneficiario.FechaNacimiento,
                dependienteRequests = new List<DependienteRequest>(),
                auditoriaRequest = new AuditoriaRequest()
                {
                    fechaCreo = beneficiario.Auditoria.FechaCreacion,
                    fechaModifico = beneficiario.Auditoria.FechaActualizacion,
                    usuarioCreo = beneficiario.Auditoria.CreadoPor,
                    usuarioModifico = beneficiario.Auditoria.ActualizadoPor
                }
                
            };
        }

        private  BeneficiarioRequest getLongBeneficiarioRequest(Beneficiario beneficiario)
        {
            return new BeneficiarioRequest()
            {
                nombreRequest = new NombreRequest()
                {
                    nombres = beneficiario.Nombre.Nombres,
                    primerApellido = beneficiario.Nombre.PrimerApellido,
                    segundoApellido = beneficiario.Nombre.SegundoApellido
                },
                fechaNacimiento = beneficiario.FechaNacimiento,
                identidadRequest = new IdentidadRequest() { identidad = beneficiario.Id.identidad },
                dependienteRequests = beneficiario.Dependientes.Select(x => new DependienteRequest()
                {
                    IdGuid = x.idGuid,
                    fechaNacimiento = x.FechaNacimiento,
                    identidadRequest = new IdentidadRequest() { identidad = x.Id.identidad },
                    nombreRequest = new NombreRequest()
                    {
                        nombres = x.Nombre.Nombres,
                        primerApellido = x.Nombre.PrimerApellido,
                        segundoApellido = x.Nombre.SegundoApellido
                    },
                    parentescoRequest = new ParentescoRequest()
                    {
                        descripcion = x.Parentesco.Descripcion,
                        guid = x.Parentesco.Id
                    },
                    auditoriaRequest = new AuditoriaRequest()
                    {
                        fechaCreo = x.auditoria.FechaCreacion,
                        fechaModifico = x.auditoria.FechaActualizacion,
                        usuarioCreo = x.auditoria.CreadoPor,
                        usuarioModifico = x.auditoria.ActualizadoPor
                    }

                }),
                auditoriaRequest  = new AuditoriaRequest()
                {
                    fechaCreo = beneficiario.Auditoria.FechaCreacion,
                    fechaModifico = beneficiario.Auditoria.FechaActualizacion,
                    usuarioCreo = beneficiario.Auditoria.CreadoPor,
                    usuarioModifico = beneficiario.Auditoria.ActualizadoPor
                }

            };
        }
    }
}