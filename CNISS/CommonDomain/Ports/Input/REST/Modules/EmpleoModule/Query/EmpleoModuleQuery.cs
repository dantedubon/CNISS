using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using CNISS.CommonDomain.Ports.Input.REST.Request.AuditoriaRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.BeneficiarioRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.EmpleoRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.EmpresaRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.GremioRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.MotivoDespidoRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.UserRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.VisitaRequest;
using CNISS.EnterpriseDomain.Domain;
using CNISS.EnterpriseDomain.Domain.Entities;
using CNISS.EnterpriseDomain.Domain.Repositories;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Security;
using NUnit.Framework.Constraints;

namespace CNISS.CommonDomain.Ports.Input.REST.Modules.EmpleoModule.Query
{
    public class EmpleoModuleQuery:NancyModule
    {

        public EmpleoModuleQuery(IEmpleoRepositoryReadOnly repositoryRead)
        {
            Get["/enterprise/empleos"] = parameters =>
            {
                var empleos = repositoryRead.getAll();
                return Response.AsJson(getEmpleosRequests(empleos));

            };

            Get["/enterprise/empleos/id={id}"] = parameters =>
            {
                var id = parameters.id;

                Guid idRequest;
                if (Guid.TryParse(id, out idRequest))
                {
                    if (Guid.Empty != idRequest)
                    {
                        var empleo = repositoryRead.get(idRequest);

                        return Response.AsJson(getEmpleoRequest(empleo));
                    }
                 
                }

                return new Response()
                    .WithStatusCode(HttpStatusCode.BadRequest);
            };


            Get["/enterprise/empleos/empresa/id={rtn}"] = parameters =>
            {
                var rtnRequest = new RTNRequest() {RTN = parameters.rtn};
                if (rtnRequest.isValidPost())
                {
                    var rtn = new RTN(rtnRequest.RTN);
                    if (rtn.isRTNValid())
                    {
                        var empleos = repositoryRead.getEmpleosByEmpresa(rtn);
                        return Response.AsJson(getEmpleosRequests(empleos));
                    }
                  
                }
                return new Response()
                    .WithStatusCode(HttpStatusCode.BadRequest);
            };

            Get["/enterprise/empleos/beneficiario/id={identidad}"] = parameters =>
            {
                var identidadRequest = new IdentidadRequest() {identidad = parameters.identidad};
                if (identidadRequest.isValidPost())
                {
                    var identidad = new Identidad(identidadRequest.identidad);
                    var empleos = repositoryRead.getEmpleosByBeneficiario(identidad);
                    return Response.AsJson(getEmpleosRequests(empleos));

                }
                return new Response()
               .WithStatusCode(HttpStatusCode.BadRequest);
            };

            Get["/movil/empleo/id={identidad}/rtn={rtn}/sucursal={sucursal}"] = parameters =>
            {
                this.RequiresClaims(new[] { "movil" });
                string identidadFromClient = parameters.identidad;
                var identidadRequest = new IdentidadRequest() {identidad = identidadFromClient};

                if (identidadRequest.isValidPost())
                {
                    string rtnFromClient = parameters.rtn;
                    var rtnRequest = new RTNRequest() {RTN = rtnFromClient};
                    if (rtnRequest.isValidPost())
                    {
                        Guid idSucursal;
                        
                        if (Guid.TryParse(parameters.sucursal, out idSucursal))
                        {
                            if (idSucursal != Guid.Empty)
                            {
                                var identidad = new Identidad(identidadRequest.identidad);
                                var empleo = repositoryRead.getEmpleoMasRecienteBeneficiario(identidad);
                                if (empleo.empresa.Id.rtn == rtnRequest.RTN)
                                {
                                    if (empleo.sucursal.Id == idSucursal)
                                    return Response.AsJson(getEmpleoRequest(empleo));
                                }
                            }
                          
                            
                        }
       
                    }
           
                }
                return new Response()
                    .WithStatusCode(HttpStatusCode.BadRequest);
            };
        }
        private  IEnumerable<EmpleoRequest> getEmpleosRequests(IEnumerable<Empleo> empleos)
        {
            return empleos.Select(getEmpleoRequest);
        }

        private IEnumerable<DependienteRequest> getDependienteRequests(IEnumerable<Dependiente> dependientes)
        {
            var dependientesRequest = new List<DependienteRequest>();
            if (dependientes != null)
            {
                dependientesRequest = dependientes.Select(x => new DependienteRequest()
                {
                    IdGuid = x.idGuid,
                    identidadRequest = new IdentidadRequest() { identidad = x.Id.identidad},
                    fechaNacimiento = x.fechaNacimiento,
                    nombreRequest = new NombreRequest() { 
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
                }).ToList();
            }

            return dependientesRequest;
        }

        private DireccionRequest getDireccionRequest(Beneficiario beneficiario)
        {
            var direccion = beneficiario.direccion;
            if (direccion == null)
            {
                return new DireccionRequest();
            }
            var departamentoRequest = new DepartamentoRequest()
            {
                idDepartamento = direccion.departamento.Id,
                nombre = direccion.departamento.nombre
            };
            var municipioRequest = new MunicipioRequest()
            {
                idMunicipio = direccion.municipio.Id,
                idDepartamento = direccion.municipio.Id,
                nombre = direccion.municipio.nombre
            };
            return new DireccionRequest()
            {
                departamentoRequest = departamentoRequest,
                municipioRequest = municipioRequest,
                descripcion = direccion.referenciaDireccion,
                IdGuid = direccion.Id
            };
        }

        private IEnumerable<FichaSupervisionEmpleoRequest> getFichaSupervisionEmpleos(
            IEnumerable<FichaSupervisionEmpleo> fichasSupervision)
        {
           
            return fichasSupervision.Select(x => new FichaSupervisionEmpleoRequest()
            {
                telefonoCelular = x.telefonoCelular,
                telefonoFijo = x.telefonoFijo,
                fotografiaBeneficiario = x.fotografiaBeneficiario.Id,
                cargo = x.cargo,
                funciones = x.funciones,
                desempeñoEmpleado = x.desempeñoEmpleado,
                posicionGPS = x.posicionGPS,
                supervisor = new SupervisorRequest()
                {
                    guid = x.supervisor.Id,
                    userRequest = new UserRequest()
                    {
                        Id = x.supervisor.usuario.Id,
                        firstName = x.supervisor.usuario.firstName,
                        secondName = x.supervisor.usuario.secondName,
                        mail = x.supervisor.usuario.mail
                    }
   
                },
                auditoriaRequest = new AuditoriaRequest()
                {
                    fechaCreo = x.auditoria.fechaCreo,
                    fechaModifico = x.auditoria.fechaModifico,
                    usuarioCreo = x.auditoria.usuarioCreo,
                    usuarioModifico = x.auditoria.usuarioModifico
                },
                firma = new FirmaAutorizadaRequest()
                {
                    IdGuid = x.firma.Id,
                    userRequest = new UserRequest()
                    {
                        Id = x.firma.user.Id,
                        firstName = x.firma.user.firstName,
                        mail = x.firma.user.mail,
                        secondName = x.firma.user.secondName
                        
                    }


                }
            }).ToList();
        }

        private NotaDespidoRequest getNotaDespidoRequest(NotaDespido notaDespido)
        {
            if (notaDespido == null)
                return null;
            return new NotaDespidoRequest()
            {
                guid = notaDespido.Id,
                fechaDespido = notaDespido.fechaDespido,
                imagenNotaDespido = notaDespido.documentoDespido.Id,
                firmaAutorizadaRequest = new FirmaAutorizadaRequest()
                {
                    IdGuid = notaDespido.firmaAutorizada.Id,
                    userRequest = new UserRequest()
                    {
                        Id = notaDespido.firmaAutorizada.user.Id,
                        firstName = notaDespido.firmaAutorizada.user.firstName,
                        mail = notaDespido.firmaAutorizada.user.mail,
                        secondName = notaDespido.firmaAutorizada.user.secondName
                        
                    }


                },
                auditoriaRequest = new AuditoriaRequest()
                {
                    fechaCreo = notaDespido.auditoria.fechaCreo,
                    fechaModifico = notaDespido.auditoria.fechaModifico,
                    usuarioCreo = notaDespido.auditoria.usuarioCreo,
                    usuarioModifico = notaDespido.auditoria.usuarioModifico
                },
                motivoDespidoRequest = new MotivoDespidoRequest()
                {
                    IdGuid = notaDespido.motivoDespido.Id,
                    descripcion = notaDespido.motivoDespido.descripcion
                },
                posicionGPS = notaDespido.posicionGPS,
                supervisorRequest = new SupervisorRequest()
                {
                    guid = notaDespido.supervisor.Id,
                    userRequest = new UserRequest()
                    {
                        Id = notaDespido.supervisor.usuario.Id,
                        firstName = notaDespido.supervisor.usuario.firstName,
                        secondName = notaDespido.supervisor.usuario.secondName,
                        mail = notaDespido.supervisor.usuario.mail
                    }

                }
            };
        }

        private  EmpleoRequest getEmpleoRequest(Empleo empleo)
        {
            return new EmpleoRequest()
            {
                beneficiarioRequest = new BeneficiarioRequest()
                {
                    identidadRequest = new IdentidadRequest() { identidad = empleo.beneficiario.Id.identidad },
                    nombreRequest = new NombreRequest()
                    {
                        nombres = empleo.beneficiario.nombre.nombres,
                        primerApellido = empleo.beneficiario.nombre.primerApellido,
                        segundoApellido = empleo.beneficiario.nombre.segundoApellido
                    },
                    fechaNacimiento = empleo.beneficiario.fechaNacimiento,
                    dependienteRequests = getDependienteRequests(empleo.beneficiario.dependientes),
                    telefonoCelular = empleo.beneficiario.telefonoCelular ?? "",
                    telefonoFijo = empleo.beneficiario.telefonoFijo ?? "",
                    direccionRequest = getDireccionRequest(empleo.beneficiario)




                },
                cargo = empleo.cargo,
                supervisado = empleo.supervisado,
                comprobantes = empleo.comprobantesPago.Select(z => new ComprobantePagoRequest()
                {
                    deducciones = z.deducciones,
                    fechaPago = z.fechaPago,
                    guid = z.Id,
                    sueldoNeto = z.sueldoNeto,
                    
                    bonificaciones = z.bonificaciones,
                    archivoComprobante =  z.imagenComprobante== null ?"": z.imagenComprobante.Id.ToString()
                }),
                contrato = empleo.contrato==null?"":empleo.contrato.Id.ToString(),
                empresaRequest = new EmpresaRequest()
                {
                    nombre = empleo.empresa.nombre,
                    rtnRequest = new RTNRequest() { RTN = empleo.empresa.Id.rtn }
                },
                sucursalRequest = new SucursalRequest()
                {
                    guid = empleo.sucursal.Id,
                    nombre = empleo.sucursal.nombre,
                    firmaAutorizadaRequest = new FirmaAutorizadaRequest()
                    {
                        IdGuid = empleo.sucursal.firma.Id,
                        fechaCreacion = empleo.sucursal.firma.fechaCreacion,
                        userRequest = new UserRequest()
                        {
                            Id = empleo.sucursal.firma.user.Id
                        }
                    }
                    
                    
                },
                fechaDeInicio = empleo.fechaDeInicio,
                horarioLaboralRequest = new HorarioLaboralRequest()
                {
                    diasLaborablesRequest = new DiasLaborablesRequest()
                    {
                        domingo = empleo.horarioLaboral.diasLaborables.domingo,
                        lunes = empleo.horarioLaboral.diasLaborables.lunes,
                        martes = empleo.horarioLaboral.diasLaborables.martes,
                        miercoles = empleo.horarioLaboral.diasLaborables.miercoles,
                        jueves = empleo.horarioLaboral.diasLaborables.jueves,
                        viernes = empleo.horarioLaboral.diasLaborables.viernes,
                        sabado = empleo.horarioLaboral.diasLaborables.sabado
                    },
                    horaEntrada = new HoraRequest()
                    {
                        hora = empleo.horarioLaboral.horaEntrada.hora,
                        minutos = empleo.horarioLaboral.horaEntrada.minutos,
                        parte = empleo.horarioLaboral.horaEntrada.parte

                    },
                    horaSalida = new HoraRequest()
                    {
                        hora = empleo.horarioLaboral.horaSalida.hora,
                        minutos = empleo.horarioLaboral.horaSalida.minutos,
                        parte = empleo.horarioLaboral.horaSalida.parte

                    }
                },
                sueldo = empleo.sueldo,
                tipoEmpleoRequest = new TipoEmpleoRequest()
                {
                    descripcion = empleo.tipoEmpleo.descripcion,
                    IdGuid = empleo.tipoEmpleo.Id
                },
                IdGuid = empleo.Id,
                notaDespidoRequest = getNotaDespidoRequest(empleo.notaDespido),
                fichaSupervisionEmpleoRequests = getFichaSupervisionEmpleos(empleo.fichasSupervisionEmpleos),
                auditoriaRequest = new AuditoriaRequest()
                {
                    fechaCreo = empleo.auditoria.fechaCreo,
                    fechaModifico = empleo.auditoria.fechaModifico,
                    usuarioCreo = empleo.auditoria.usuarioCreo,
                    usuarioModifico = empleo.auditoria.usuarioModifico
                },
                
            };

        }
    }
      
    
}