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
    [Subject(typeof(GremioModuleInsert))]
    public class when_UserPutGremioWithInvalidRepresentante_Should_ReturnError
    {
        static Browser _browser;
        static BrowserResponse _response;
        static ICommandUpdateGremioRepresentante _commandUpdate;
        static GremioRequest _request;


        Establish context = () =>
        {


            var representanteLegal = getRepresentanteLegal();
            var rtn = new RTNRequest() { RTN = "08011985123960" };
            _request = new GremioRequest()
            {
                direccionRequest = getDireccion(),
                nombre = "gremio",
                representanteLegalRequest = new RepresentanteLegalRequest(),
                rtnRequest = rtn


            };
            _commandUpdate = Mock.Of<ICommandUpdateGremioRepresentante>();
            Mock.Get(_commandUpdate).Setup(x => x.isExecutable(Moq.It.IsAny<Gremio>())).Returns(false);
            _browser = new Browser(
                x =>
                {
                    x.Module<GremioModuleUpdateRepresentante>();
                    x.Dependencies(_commandUpdate);
                }

                );
        };


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

        private Because of = () => _response = _browser.PutSecureJson("enterprise/gremio/representante", _request);

        private It should_return_error = () => _response.StatusCode.ShouldEqual(
            HttpStatusCode.BadRequest);
    }

}