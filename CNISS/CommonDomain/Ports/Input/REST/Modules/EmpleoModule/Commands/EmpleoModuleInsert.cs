using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Application;
using CNISS.CommonDomain.Ports.Input.REST.Request.BeneficiarioRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.EmpleoRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.EmpresaRequest;
using CNISS.EnterpriseDomain.Domain;
using CNISS.EnterpriseDomain.Domain.Entities;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using Nancy;
using Nancy.ModelBinding;
using NHibernate.Linq;
using NHibernate.Param;

namespace CNISS.CommonDomain.Ports.Input.REST.Modules.EmpleoModule.Commands
{
    public class EmpleoModuleInsert:NancyModule
    {
        private readonly EmpleoMapping _empleoMapping;
        private readonly IFileGetter _fileGetter;
        private const string DirectorioArchivosContratos = @"/EmpleoContratos";
        private const string DirectorioArchivosVouchers = @"/EmpleoComprobantesPago";
        private const string Extension = ".pdf";

        public EmpleoModuleInsert(ICommandInsertIdentity<Empleo> command , IFileGetter fileGetter)
        {
            _fileGetter = fileGetter;
            _empleoMapping = new EmpleoMapping();
            Post["/enterprise/empleos"] = parameters =>
            {
                var request = this.Bind<EmpleoRequest>();
                if (request.isValidPost())
                {

                    if (contratoNotEmpty(request))
                    {
                        var contrato = request.contrato;
                        if (!_fileGetter.existsFile(DirectorioArchivosContratos, contrato, Extension))
                        {
                            return new Response()
                                    .WithStatusCode(HttpStatusCode.BadRequest);
                        }
                        request.contentFile = fileGetter.getFile(@"/EmpleoContratos", contrato, ".pdf");
                    }

                    if (someFileVoucherNotEmpty(request))
                    {
                        if (!existsFilesVoucher(request))
                        {
                            return new Response()
                                    .WithStatusCode(HttpStatusCode.BadRequest);
                        }

                        setFileContentRequest(request);
                    }


                    var empleo = _empleoMapping.getEmpleoForPost(request);
                    if (command.isExecutable(empleo))
                    {             
                        command.execute(empleo);
                        return new Response()
                       .WithStatusCode(HttpStatusCode.OK);
                    }

                   
                }
                return new Response()
                    .WithStatusCode(HttpStatusCode.BadRequest);
            };
        }

        private bool someFileVoucherNotEmpty(EmpleoRequest request)
        {
            return  request.comprobantes.Select(x => !string.IsNullOrEmpty(x.archivoComprobante)).Any();
        }

        private bool contratoNotEmpty(EmpleoRequest request)
        {
            return !string.IsNullOrEmpty(request.contrato);
        }

        private bool existsFilesVoucher(EmpleoRequest request)
        {

            var comprobantesConArchivo = request.comprobantes.Where(x => !string.IsNullOrEmpty(x.archivoComprobante));
            return
                comprobantesConArchivo.All(
                    x => _fileGetter.existsFile(DirectorioArchivosVouchers, x.archivoComprobante, Extension));


        }

        private void setFileContentRequest(EmpleoRequest request)
        {
            var comprobantesConArchivo = request.comprobantes.Where(x => !string.IsNullOrEmpty(x.archivoComprobante));
            comprobantesConArchivo.ForEach(x => x.contentFile = _fileGetter.getFile(DirectorioArchivosVouchers, x.archivoComprobante, Extension));

        }
    }
}