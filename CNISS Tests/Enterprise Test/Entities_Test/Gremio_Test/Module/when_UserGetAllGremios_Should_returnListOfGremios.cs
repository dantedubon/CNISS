using System.Collections.Generic;
using System.Linq;
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
    public class when_UserGetAllGremios_Should_returnListOfGremios
    {
        static Browser _browser;
        static BrowserResponse _response;
        
        static IEnumerable<GremioRequest> _request;
        private static IEnumerable<GremioRequest> _originalData; 

        private Establish context = () =>
        {

            _originalData = getGremiosRequests();
            var gremios = getGremios(_originalData);
            var repositorio = Mock.Of<IGremioRepositoryReadOnly>();
            Mock.Get(repositorio).Setup(x => x.getAll()).Returns(gremios);

            _browser = new Browser(
                x =>
                {
                    x.Module<GremioModuleQuery>();
                    x.Dependencies(repositorio);
                }
                );


        };

        #region Helper Methods
        private static IEnumerable<Gremio> getGremios(IEnumerable<GremioRequest> listaGremioRequests)
        {
            var listaGremios = listaGremioRequests.Select(convertToGremio);
            return listaGremios;


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


        private static IEnumerable<GremioRequest> getGremiosRequests()
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

            var representante2 = getRepresentanteLegal("0801198511111", "representante");
            var direccion2 = getDireccion("02", "02", "02", "Barrio Abajo");
            var gremio2 = new GremioRequest()
            {
                direccionRequest = direccion2,
                nombre = "Maquiladores",
                representanteLegalRequest = representante2,
                rtnRequest = new RTNRequest() { RTN = "08011985123960" }
            };
            return new List<GremioRequest>() { gremio1, gremio2 };

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
        #endregion

        private Because of = () =>
        {
            _request = _browser.GetSecureJson("/enterprise/gremio").Body.DeserializeJson<IEnumerable<GremioRequest>>();
        };

        It should_return_all_gremios = () => _request.ShouldBeEquivalentTo(_originalData);
    }
}