using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Application;
using CNISS.CommonDomain.Ports.Input.REST;
using CNISS.CommonDomain.Ports.Input.REST.Modules.EmpresaModule.Commands;
using CNISS.CommonDomain.Ports.Input.REST.Request.EmpresaRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.GremioRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.RolModule;
using CNISS.CommonDomain.Ports.Input.REST.Request.UserRequest;
using CNISS.EnterpriseDomain.Domain.Entities;
using Machine.Specifications;
using Moq;
using Nancy;
using Nancy.Testing;
using Should;
using It = Machine.Specifications.It;

namespace CNISS_Tests.Enterprise_Test.Entities_Test.Empresas_Test.Module
{
    [Subject(typeof(EmpresaModuleInsert))]
    public class when_UserPostValidEmpresaNotEmptyContractFileFileNotExist_Should_ReturnError
    {
        static Browser _browser;
        static BrowserResponse _response;
        static ICommandInsertIdentity<Empresa> _commandInsert;
        static EmpresaRequest _request;
        private Establish context = () =>
        {
            _request = getEmpresaRequest();
            _request.contentFile = "archivo";
            _commandInsert = Mock.Of<ICommandInsertIdentity<Empresa>>();
            var fileGetter = Mock.Of<IFileGetter>();
            var dataFile = new byte[] { 0, 1, 1, 1, 0, 1 };
        
            Mock.Get(fileGetter)
                .Setup(x => x.existsFile(@"/EmpresasContratos",_request.contentFile,".pdf")).Returns(false);


            Mock.Get(_commandInsert)
                .Setup(x => x.isExecutable(Moq.It.IsAny<Empresa>())).Returns(true);

           

            _browser = new Browser(
                x =>
                {
                    x.Module<EmpresaModuleInsert>();
                    x.Dependencies(_commandInsert, fileGetter);
                }
                );


        };

        private Because of = () => _response = _browser.PostSecureJson("enterprise/", _request);

         It should_return_error = () => _response.StatusCode.ShouldEqual(HttpStatusCode.BadRequest);

        private static EmpresaRequest getEmpresaRequest()
        {
            var empresa = new EmpresaRequest()
            {
                actividadEconomicaRequests = getActividades(),
                contentFile = "",
                empleadosTotales = 0,
                gremioRequest = getGremio(),
                programaPiloto = true,
                rtnRequest = getValidRTN(),
                sucursalRequests = getGoodSucursales(),
                nombre = "Empresa"
            };
            return empresa;
        }

        private static IEnumerable<ActividadEconomicaRequest> getActividades()
        {
            return new List<ActividadEconomicaRequest>()
            {
                new ActividadEconomicaRequest() {descripcion = "Camarones", guid = Guid.NewGuid()}
            };
        }
        private static RTNRequest getValidRTN()
        {
            return new RTNRequest() { RTN = "08011985123960" };
        }

        private static IEnumerable<SucursalRequest> getGoodSucursales()
        {
            return new List<SucursalRequest>()
            {
                getSucursalGood(),
                getSucursalGood()
            };
        }




        private static SucursalRequest getSucursalGood()
        {
            return new SucursalRequest() { direccionRequest = getValidDireccion(), userFirmaRequest = getUserRequest(), nombre = "El centro" };
        }



        private static UserRequest getUserRequest()
        {
            var rol = new RolRequest() { idGuid = Guid.NewGuid() };
            var user = new UserRequest { firstName = "Dante", Id = "DRCD", userRol = rol, mail = "xx", password = "dd", secondName = "Castillo" };
            return user;
        }


        private static RepresentanteLegalRequest getValidRepresentanteLegal()
        {
            return new RepresentanteLegalRequest()
            {
                identidadRequest = new IdentidadRequest() { identidad = "0801198512396" },
                nombre = "Julio"
            };
        }

        private static DireccionRequest getValidDireccion()
        {
            return new DireccionRequest()
            {
                departamentoRequest = new DepartamentoRequest() { idDepartamento = "01", nombre = "departamento" },
                municipioRequest =
                    new MunicipioRequest() { idDepartamento = "01", idMunicipio = "01", nombre = "municipio" },
                descripcion = "Barrio Abajo"
            };
        }

        public static GremioRequest getGremio()
        {

            return new GremioRequest()
            {
                direccionRequest = getValidDireccion()
                ,
                nombre = "Camara",
                representanteLegalRequest = getValidRepresentanteLegal(),
                rtnRequest = getValidRTN()
            };

        }

    }

}