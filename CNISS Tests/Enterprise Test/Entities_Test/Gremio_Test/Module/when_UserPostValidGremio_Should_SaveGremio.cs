using System;
using CNISS.CommonDomain.Application;
using CNISS.CommonDomain.Ports.Input.REST.Modules.GremioModule.GremioCommand;
using CNISS.CommonDomain.Ports.Input.REST.Request.AuditoriaRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.GremioRequest;
using CNISS.EnterpriseDomain.Domain.Entities;
using FizzWare.NBuilder;
using Machine.Specifications;
using Moq;
using Nancy.Testing;
using It = Machine.Specifications.It;

namespace CNISS_Tests.Enterprise_Test.Entities_Test.Gremio_Test.Module
{
    [Subject(typeof (GremioModuleInsert))]
    public class when_UserPostValidGremio_Should_SaveGremio
    {
        static Browser _browser;
        static BrowserResponse _response;
        static ICommandInsertIdentity<Gremio> _commandInsert;
        static GremioRequest _request;


         Establish context = () =>
         {

             var direccion = getDireccion();
             var representanteLegal = getRepresentanteLegal();
             var rtn = new RTNRequest() {RTN = "08011985123960"};
             _request = new GremioRequest()
             {
                 direccionRequest = direccion,
                 nombre = "gremio",
                 representanteLegalRequest = representanteLegal,
                 rtnRequest = rtn,
                 auditoriaRequest = getAuditoriaRequest()

             };
             _commandInsert = Mock.Of<ICommandInsertIdentity<Gremio>>();
             Mock.Get(_commandInsert).Setup(x => x.isExecutable(Moq.It.IsAny<Gremio>())).Returns(true);
             _browser = new Browser(
                 x =>
                 {
                     x.Module<GremioModuleInsert>();
                     x.Dependencies(_commandInsert);
                 }

                 );
         };


        private static RepresentanteLegalRequest getRepresentanteLegal()
        {
            var identidad = new IdentidadRequest() {identidad = "0801198512396"};
            return new RepresentanteLegalRequest(){identidadRequest = identidad,nombre = "representante"};
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
        private static AuditoriaRequest getAuditoriaRequest()
        {
            return new AuditoriaRequest()
            {
                fechaCreo = DateTime.Now,
                fechaModifico = DateTime.Now,
                usuarioCreo = "",
                usuarioModifico = ""

            };
        }

        private Because of = () => _browser.PostSecureJson("enterprise/gremio",_request);

        It should_save_gremio = () => Mock.Get(_commandInsert).Verify( x=> x.execute(Moq.It.Is<Gremio>(z => z.Id.rtn == _request.rtnRequest.RTN)));
    }
}