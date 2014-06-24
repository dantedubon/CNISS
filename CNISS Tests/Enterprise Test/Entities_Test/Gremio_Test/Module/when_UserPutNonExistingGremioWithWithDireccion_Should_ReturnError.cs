using CNISS.CommonDomain.Ports.Input.REST.Modules.GremioModule.GremioCommand;
using CNISS.CommonDomain.Ports.Input.REST.Request.GremioRequest;
using CNISS.EnterpriseDomain.Application;
using CNISS.EnterpriseDomain.Domain.Entities;
using FizzWare.NBuilder;
using Machine.Specifications;
using Moq;
using Nancy;
using Nancy.Testing;
using Should;
using It = Machine.Specifications.It;

namespace CNISS_Tests.Enterprise_Test.Entities_Test.Gremio_Test.Module
{
    [Subject(typeof(GremioModuleUpdateDireccion))]
    public class when_UserPutNonExistingGremioWithWithDireccion_Should_ReturnError
    {
        static Browser _browser;
        static BrowserResponse _response;
        static ICommandUpdateGremioDireccion _commandUpdate;
        static GremioRequest _request;

        private Establish context = () =>
        {
            var representanteLegal = getRepresentanteLegal();
            var rtn = new RTNRequest() { RTN = "08011985123960" };
            _request = new GremioRequest()
            {
                direccionRequest = getDireccion(),
                nombre = "gremio",
                representanteLegalRequest = representanteLegal,
                rtnRequest = rtn


            };
            _commandUpdate = Mock.Of<ICommandUpdateGremioDireccion>();
            Mock.Get(_commandUpdate).Setup(x => x.isExecutable(Moq.It.IsAny<Gremio>())).Returns(false);
            _browser = new Browser(
                x =>
                {
                    x.Module<GremioModuleUpdateDireccion>();
                    x.Dependencies(_commandUpdate);
                }

                );

        };

        private Because of = () => { _response = _browser.PutSecureJson("enterprise/gremio/direccion", _request); };

        It should_return_error = () => _response.StatusCode.ShouldEqual(HttpStatusCode.BadRequest);

        private static RepresentanteLegalRequest getRepresentanteLegal()
        {
            var identidad = new IdentidadRequest() { identidad = "0801198512396" };
            return new RepresentanteLegalRequest() { identidadRequest = identidad, nombre = "representante" };
        }
        private static DireccionRequest getDireccion()
        {
            var municipio = Builder<MunicipioRequest>.CreateNew().Build();
            municipio.idDepartamento = "01";
            municipio.idMunicipio = "01";
            municipio.nombre = "municipio";
            var departamento = Builder<DepartamentoRequest>.CreateNew().Build();
            departamento.idDepartamento = "01";
            departamento.nombre = "departamento";


            return new DireccionRequest()
            {
                departamentoRequest = departamento,
                descripcion = "municipio",
                municipioRequest = municipio
            };
        }

    }
}