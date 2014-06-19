using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Ports.Input.REST.Modules.GremioModule.GremioQuery;
using CNISS.CommonDomain.Ports.Input.REST.Request.GremioRequest;
using CNISS.EnterpriseDomain.Domain;
using CNISS.EnterpriseDomain.Domain.Entities;
using CNISS.EnterpriseDomain.Domain.Repositories;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using FizzWare.NBuilder;
using FluentAssertions;
using Machine.Specifications;
using Moq;
using Nancy.Testing;
using It = Machine.Specifications.It;

namespace CNISS_Tests.Enterprise_Test.Entities_Test.Gremio_Test.Module
{
    [Subject(typeof (GremioModuleQuery))]
    public class when_UserGetSpecicGremioById_Should_ReturnGremio
    {
        static Browser _browser;
        static BrowserResponse _response;
        static GremioRequest _originalGremio;
        private static GremioRequest _responseGremio; 


        private Establish context = () =>
        {
            _originalGremio = getGremioRequest();

            var repository = Mock.Of<IGremioRepositoryReadOnly>();
            Mock.Get(repository).Setup(x => x.get(Moq.It.IsAny<RTN>())).Returns(convertToGremio(_originalGremio));

            _browser = new Browser(
                x =>
                {
                    x.Module<GremioModuleQuery>();
                    x.Dependencies(repository);

                }
                
                );


        };

        private Because of = () =>
        {
            _responseGremio =
                _browser.GetSecureJson("/enterprise/gremio/id=" + _originalGremio.rtnRequest.RTN)
                    .Body.DeserializeJson<GremioRequest>();
        };

        It should_return_gremio = () =>
        {
            _responseGremio.ShouldBeEquivalentTo(_originalGremio);
                

        };

        private static GremioRequest getGremioRequest()
        {
            var representante1 = getRepresentanteLegal("0801198512396", "representante");
            var direccion1 = getDireccion("01", "01", "01", "Barrio Abajo");
            var gremio1 = new GremioRequest()
            {
                direccionRequest = direccion1,
                nombre = "Camara",
                representanteLegalRequest = representante1,
                rtnRequest = new RTNRequest() { RTN = "08011985123960" }
            };
            return gremio1;
        }
        private static Gremio convertToGremio(GremioRequest request)
        {

            var rtn = request.rtnRequest;
            var representante = request.representanteLegalRequest;
            var direccion = request.direccionRequest;
            var rtnGremio = new RTN(rtn.RTN);

            var representanteGremio = new RepresentanteLegal(new Identidad(representante.identidadRequest.identidad),
                representante.nombre);

            var municipioGremio = new Municipio()
            {
                departamentoId = direccion.departamentoRequest.idDepartamento,
                Id = direccion.municipioRequest.idMunicipio,
                nombre = direccion.municipioRequest.nombre
            };


            var departamentoGremio = new Departamento()
            {
                Id = direccion.departamentoRequest.idDepartamento,
                municipios = new List<Municipio>() { municipioGremio },
                nombre = direccion.departamentoRequest.nombre
            };
            var direccionGremio = new Direccion(departamentoGremio, municipioGremio, direccion.descripcion);
            direccionGremio.Id = direccion.IdGuid;

            return new Gremio(rtnGremio, representanteGremio, direccionGremio, request.nombre);

        }
        private static RepresentanteLegalRequest getRepresentanteLegal(string idRepresentante, string representante)
        {
            var id = idRepresentante;
            var nombre = representante;
            var identidad = new IdentidadRequest() { identidad = id };
            return new RepresentanteLegalRequest() { identidadRequest = identidad, nombre = nombre };
        }
        private static DireccionRequest getDireccion(string idDepartamentoMunicipio, string idMunicipio, string idDepartamento, string descripcionDireccion)
        {
            var municipio = Builder<MunicipioRequest>.CreateNew().Build();
            municipio.idDepartamento = idDepartamentoMunicipio;
            municipio.idMunicipio = idMunicipio;
            municipio.nombre = "municipio";
            var departamento = Builder<DepartamentoRequest>.CreateNew().Build();
            departamento.idDepartamento = idDepartamento;
            departamento.nombre = "departamento";


            return new DireccionRequest()
            {
                departamentoRequest = departamento,
                descripcion = descripcionDireccion,
                municipioRequest = municipio
            };
        } 
    }
}