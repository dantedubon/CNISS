using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Ports.Input.REST.Request.BeneficiarioRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.EmpleoRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.EmpresaRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.GremioRequest;
using CNISS.EnterpriseDomain.Domain;
using CNISS.EnterpriseDomain.Domain.Entities;
using CNISS.EnterpriseDomain.Domain.Repositories;
using Nancy;
using Nancy.ModelBinding;

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

                        return Response.AsJson(getEmpleoRequests(empleo));
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
                        return new Response()
                      .WithStatusCode(HttpStatusCode.OK);
                    }
                  
                }
                return new Response()
                    .WithStatusCode(HttpStatusCode.BadRequest);
            };
        }
        private  IEnumerable<EmpleoRequest> getEmpleosRequests(IEnumerable<Empleo> empleos)
        {
            return empleos.Select(getEmpleoRequests);
        }
        private static EmpleoRequest getEmpleoRequests(Empleo empleo)
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
                    fechaNacimiento = empleo.beneficiario.fechaNacimiento


                },
                cargo = empleo.cargo,
                comprobantes = empleo.comprobantesPago.Select(z => new ComprobantePagoRequest()
                {
                    deducciones = z.deducciones,
                    fechaPago = z.fechaPago,
                    guid = z.Id,
                    percepciones = z.percepciones,
                    total = z.total
                }),
                empresaRequest = new EmpresaRequest()
                {
                    nombre = empleo.empresa.nombre,
                    rtnRequest = new RTNRequest() { RTN = empleo.empresa.Id.rtn }
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
                IdGuid = empleo.Id
            };

        }
    }
      
    
}