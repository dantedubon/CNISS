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
                    nombres = beneficiario.nombre.nombres,
                    primerApellido = beneficiario.nombre.primerApellido, 
                    segundoApellido = beneficiario.nombre.segundoApellido
                },
                fechaNacimiento = beneficiario.fechaNacimiento,
                dependienteRequests = new List<DependienteRequest>(),
                auditoriaRequest = new AuditoriaRequest()
                {
                    fechaCreo = beneficiario.auditoria.fechaCreo,
                    fechaModifico = beneficiario.auditoria.fechaModifico,
                    usuarioCreo = beneficiario.auditoria.usuarioCreo,
                    usuarioModifico = beneficiario.auditoria.usuarioModifico
                }
                
            };
        }

        private  BeneficiarioRequest getLongBeneficiarioRequest(Beneficiario beneficiario)
        {
            return new BeneficiarioRequest()
            {
                nombreRequest = new NombreRequest()
                {
                    nombres = beneficiario.nombre.nombres,
                    primerApellido = beneficiario.nombre.primerApellido,
                    segundoApellido = beneficiario.nombre.segundoApellido
                },
                fechaNacimiento = beneficiario.fechaNacimiento,
                identidadRequest = new IdentidadRequest() { identidad = beneficiario.Id.identidad },
                dependienteRequests = beneficiario.dependientes.Select(x => new DependienteRequest()
                {
                    IdGuid = x.idGuid,
                    fechaNacimiento = x.fechaNacimiento,
                    identidadRequest = new IdentidadRequest() { identidad = x.Id.identidad },
                    nombreRequest = new NombreRequest()
                    {
                        nombres = x.nombre.nombres,
                        primerApellido = x.nombre.primerApellido,
                        segundoApellido = x.nombre.segundoApellido
                    },
                    parentescoRequest = new ParentescoRequest()
                    {
                        descripcion = x.parentesco.descripcion,
                        guid = x.parentesco.Id
                    },
                    auditoriaRequest = new AuditoriaRequest()
                    {
                        fechaCreo = x.auditoria.fechaCreo,
                        fechaModifico = x.auditoria.fechaModifico,
                        usuarioCreo = x.auditoria.usuarioCreo,
                        usuarioModifico = x.auditoria.usuarioModifico
                    }

                }),
                auditoriaRequest  = new AuditoriaRequest()
                {
                    fechaCreo = beneficiario.auditoria.fechaCreo,
                    fechaModifico = beneficiario.auditoria.fechaModifico,
                    usuarioCreo = beneficiario.auditoria.usuarioCreo,
                    usuarioModifico = beneficiario.auditoria.usuarioModifico
                }

            };
        }
    }
}