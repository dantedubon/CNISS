using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Ports.Input.REST.Request.BeneficiarioRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.GremioRequest;
using Nancy;

namespace CNISS.CommonDomain.Ports.Input.REST.Modules.BeneficiarioModule.Query
{
    public class BeneficiarioModuleQuery:NancyModule
    {
        public BeneficiarioModuleQuery()
        {
            Get["/enterprise/beneficiarios"] = parameters =>
            {
                var beneficiario = new BeneficiarioRequest();
                var nombre = new NombreRequest()
                {
                    nombres = "Dante Ruben",
                    primerApellido = "Castillo",
                    segundoApellido = "Dubón"
                };
                var identidad = new IdentidadRequest()
                {
                    identidad = "0801198512396"
                };

                var dependientes = new List<DependienteRequest>()
                {
                    new DependienteRequest()
                    {
                        edad = 56,
                        identidadRequest = new IdentidadRequest() {identidad = "0801195732456"},
                        nombreRequest =
                            new NombreRequest()
                            {
                                nombres = "Lavinia",
                                primerApellido = "Dubón",
                                segundoApellido = "Fajardo"
                            },
                        parentescoRequest = new ParentescoRequest() {descripcion = "Madre", guid = Guid.NewGuid()}
                    },
                    new DependienteRequest()
                    {
                        edad = 56,
                        identidadRequest = new IdentidadRequest() {identidad = "0801195732456"},
                        nombreRequest =
                            new NombreRequest()
                            {
                                nombres = "Lavinia",
                                primerApellido = "Dubón",
                                segundoApellido = "Fajardo"
                            },
                        parentescoRequest = new ParentescoRequest() {descripcion = "Madre", guid = Guid.NewGuid()}
                    }
                };

                beneficiario.dependienteRequests = dependientes;
                beneficiario.identidadRequest = identidad;
                beneficiario.nombreRequest = nombre;
                beneficiario.fechaNacimiento = new DateTime(1984, 8, 2);

                return Response.AsJson(beneficiario);

            };
        }
    }
}