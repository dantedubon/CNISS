using System;
using System.Collections.Generic;
using System.Linq;
using CNISS.CommonDomain.Domain;
using CNISS.CommonDomain.Ports.Input.REST.Request.AuditoriaRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.BeneficiarioRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.EmpleoRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.EmpresaRequest;
using CNISS.EnterpriseDomain.Domain;
using CNISS.EnterpriseDomain.Domain.Entities;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using NHibernate.Linq;

namespace CNISS.CommonDomain.Ports.Input.REST.Modules.EmpleoModule.Commands
{
    public class EmpleoMapping
    {
        public EmpleoMapping()
        {
        }

        public Empleo getEmpleoForPut(EmpleoRequest empleoRequest)
        {
            var empleo = getEmpleoForPost(empleoRequest);
            empleo.Id = empleoRequest.IdGuid;
            return empleo;
        }
        public Empleo getEmpleoForPost(EmpleoRequest empleoRequest)
        {
            var empresaRequest = empleoRequest.empresaRequest;
            var beneficiarioRequest = empleoRequest.beneficiarioRequest;
            var horarioRequest = empleoRequest.horarioLaboralRequest;
            var comprobantes = empleoRequest.comprobantes;
            var sucursal = empleoRequest.sucursalRequest;
            var tipoEmpleo = empleoRequest.tipoEmpleoRequest;

            var empleo = new Empleo(getEmpresa(empresaRequest),getSucursal(sucursal),
                getBeneficiario(beneficiarioRequest),
                getHorarioLaboral(horarioRequest),
                empleoRequest.cargo,empleoRequest.sueldo,getTipoEmpleo(tipoEmpleo),empleoRequest.fechaDeInicio);

            empleo.contrato = getContrato(empleoRequest);

            var comprobantesEmpleo = getComprobantes(comprobantes);

            comprobantesEmpleo.ForEach(empleo.addComprobante);
            empleo.auditoria = getAuditoria(empleoRequest.auditoriaRequest);
            
            return empleo;

        }

        private ContentFile getContrato(EmpleoRequest empleoRequest)
        {
            var contrato = empleoRequest.contrato;
            var contenido = empleoRequest.contentFile;
            if (!string.IsNullOrEmpty(contrato))
            {
                return contenido != null ? new ContentFile(contenido) : new ContentFileNull(Guid.Parse(contrato));
            }
            return null;
        }
        private Auditoria getAuditoria(AuditoriaRequest auditoria)
        {
            return new Auditoria(auditoria.usuarioCreo,auditoria.fechaCreo, auditoria.usuarioModifico, auditoria.fechaModifico);
        }

        private TipoEmpleo getTipoEmpleo(TipoEmpleoRequest tipoEmpleoRequest)
        {
            var tipoEmpleo = new TipoEmpleo(tipoEmpleoRequest.descripcion);
            tipoEmpleo.Id = tipoEmpleoRequest.IdGuid;
            return tipoEmpleo;
        }

        private Sucursal getSucursal(SucursalRequest sucursalRequest)
        {
            var sucursal = new Sucursal(sucursalRequest.nombre, new DireccionNull(), new FirmaAutorizadaNull());
            sucursal.Id = sucursalRequest.guid;

            return sucursal;
        }

        private IEnumerable<ComprobantePago> getComprobantes(IEnumerable<ComprobantePagoRequest> comprobantePagoRequests)
        {
            return
                comprobantePagoRequests.Select(
                    getComprobantePago);

        }

        private ComprobantePago getComprobantePago(ComprobantePagoRequest comprobantePagoRequest)
        {
            var comprobante = new ComprobantePago(comprobantePagoRequest.fechaPago, comprobantePagoRequest.deducciones,
                comprobantePagoRequest.sueldoNeto, comprobantePagoRequest.bonificaciones);

            var guiRequest = comprobantePagoRequest.guid;
            if (Guid.Empty != guiRequest)
            {
                comprobante.Id = guiRequest;
            }
            comprobante.imagenComprobante = getImagenComprobante(comprobantePagoRequest);
            comprobante.auditoria = getAuditoria(comprobantePagoRequest.auditoriaRequest);
            return comprobante;
        }

        private ContentFile getImagenComprobante(ComprobantePagoRequest comprobantePago)
        {


            var archivoComprobante = comprobantePago.archivoComprobante;
            var contenido = comprobantePago.contentFile;
            if (!string.IsNullOrEmpty(archivoComprobante))
            {
                return contenido != null ? new ContentFile(contenido) : new ContentFileNull(Guid.Parse(archivoComprobante));
            }
            return null;

        }

        private HorarioLaboral getHorarioLaboral(HorarioLaboralRequest horarioLaboralRequest)
        {
            var horaEntrada = new Hora(horarioLaboralRequest.horaEntrada.hora, horarioLaboralRequest.horaEntrada.minutos,
                horarioLaboralRequest.horaEntrada.parte);

            var horarioSalida = new Hora(horarioLaboralRequest.horaSalida.hora, horarioLaboralRequest.horaSalida.minutos,
                horarioLaboralRequest.horaSalida.parte);

            var diasLaborables = new DiasLaborables()
            {
                domingo = horarioLaboralRequest.diasLaborablesRequest.domingo,
                lunes = horarioLaboralRequest.diasLaborablesRequest.lunes,
                martes = horarioLaboralRequest.diasLaborablesRequest.martes,
                miercoles = horarioLaboralRequest.diasLaborablesRequest.miercoles,
                jueves = horarioLaboralRequest.diasLaborablesRequest.jueves,
                viernes = horarioLaboralRequest.diasLaborablesRequest.viernes,
                sabado = horarioLaboralRequest.diasLaborablesRequest.sabado
            };

            return new HorarioLaboral(horaEntrada, horarioSalida, diasLaborables);
        }

        private Empresa getEmpresa(EmpresaRequest empresaRequest)
        {
            var empresa = new Empresa(new RTN(empresaRequest.rtnRequest.RTN), empresaRequest.nombre,
                empresaRequest.fechaIngreso, new GremioNull());
            return empresa;
        }

        private Beneficiario getBeneficiario(BeneficiarioRequest beneficiarioRequest)
        {
            var nombre = new Nombre(beneficiarioRequest.nombreRequest.nombres,
                beneficiarioRequest.nombreRequest.primerApellido, beneficiarioRequest.nombreRequest.segundoApellido);

            var beneficiario = new Beneficiario(new Identidad(beneficiarioRequest.identidadRequest.identidad), nombre,
                beneficiarioRequest.fechaNacimiento);

            return beneficiario;
        }
    }
}