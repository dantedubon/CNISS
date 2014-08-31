using System;
using System.Linq;
using CNISS.AutenticationDomain.Domain.Entities;
using CNISS.CommonDomain.Domain;
using CNISS.CommonDomain.Ports.Input.REST.Modules.EmpresaModule.Query;
using CNISS.CommonDomain.Ports.Input.REST.Request.AuditoriaRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.EmpresaRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.GremioRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.UserRequest;
using CNISS.EnterpriseDomain.Domain;
using CNISS.EnterpriseDomain.Domain.Entities;
using CNISS.EnterpriseDomain.Domain.Repositories;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using FizzWare.NBuilder;
using FluentAssertions;
using Machine.Specifications;
using Moq;
using Nancy;
using Nancy.Testing;
using Should;
using It = Machine.Specifications.It;

namespace CNISS_Tests.Enterprise_Test.Entities_Test.Empresas_Test.Module
{
    [Subject(typeof(EmpresaModuleQuery))]
    public class when_UserGetEmpresaByValidID_Should_ReturnEmpresa
    {

        static Browser _browser;
        static BrowserResponse _response;
        private static IEmpresaRepositoryReadOnly _repositoryRead;
        private static RTNRequest request;
        private static EmpresaRequest _expectedResponse;
        private static EmpresaRequest _responsEmpresaRequest;



        private Establish context = () =>
        {
            request = new RTNRequest() { RTN = "08011985123960" };

            var empresa = Builder<Empresa>.CreateNew().WithConstructor(
               () =>
                   new Empresa(Builder<RTN>.CreateNew().Build(), "nombre", new DateTime(2014, 3, 1),
                       Builder<Gremio>.CreateNew().WithConstructor(() => new Gremio(
                          new RTN("08011985123960"), Builder<RepresentanteLegal>.CreateNew().Build(),
                           Builder<Direccion>.CreateNew().Build(), "gremio")).Build())
               ).With(x => x.Auditoria = Builder<Auditoria>.CreateNew().Build()).Build();

            empresa.ActividadesEconomicas = Builder<ActividadEconomica>.CreateListOfSize(3).Build();
            empresa.Sucursales = Builder<Sucursal>.CreateListOfSize(3).All().WithConstructor(
                ()=> new Sucursal("Sucursal",Builder<Direccion>.CreateNew().WithConstructor(
                    ()=> new Direccion(Builder<Departamento>.CreateNew().Build(),
                        Builder<Municipio>.CreateNew().Build(),"referencia")
                    ).Build()
                    
                    
                    ,Builder<FirmaAutorizada>.CreateNew().WithConstructor(
                    ()=> new FirmaAutorizada(Builder<User>.CreateNew().Build(),new DateTime(2014,3,1))
                    
                    ).Build())
                ).With(x => x.Auditoria = Builder<Auditoria>.CreateNew().Build()).Build();




          
            _repositoryRead = Mock.Of<IEmpresaRepositoryReadOnly>();
            Mock.Get(_repositoryRead).Setup(x => x.get(Moq.It.IsAny<RTN>())).Returns(empresa);

            _expectedResponse = getEmpresaRequest(empresa);
            var _repositorioGremio = Mock.Of<IGremioRepositoryReadOnly>();
            _browser = new Browser(
                x =>
                {
                    x.Module<EmpresaModuleQuery>();
                    x.Dependencies(_repositoryRead, _repositorioGremio);
                }

                );

        };

        private Because of = () => { _responsEmpresaRequest = _browser.GetSecureJson("/enterprise/id=", request).Body.DeserializeJson<EmpresaRequest>(); };

        private It should_return_empresa = () => _responsEmpresaRequest.ShouldBeEquivalentTo(_expectedResponse);


        private static EmpresaRequest getEmpresaRequest(Empresa empresa)
        {
            var empresaRequest = new EmpresaRequest()
            {
                nombre = empresa.Nombre,
                actividadEconomicaRequests = empresa.ActividadesEconomicas.Select(x => new ActividadEconomicaRequest()
                {
                    descripcion = x.Descripcion,
                    guid = x.Id
                }),
                contentFile = "",
                empleadosTotales = empresa.EmpleadosTotales,
                fechaIngreso = empresa.FechaIngreso,
                gremioRequest = new GremioRequest()
                {
                    nombre = empresa.Gremial.Nombre,
                    rtnRequest = new RTNRequest()
                    {
                        RTN = empresa.Gremial.Id.Rtn
                    }
                },
                programaPiloto = empresa.ProyectoPiloto,
                rtnRequest = new RTNRequest() { RTN = empresa.Id.Rtn},
                sucursalRequests = empresa.Sucursales.Select(x => new SucursalRequest()
                {
                    guid = x.Id,
                    nombre = x.Nombre,
                    direccionRequest = new DireccionRequest()
                    {
                        departamentoRequest = new DepartamentoRequest()
                        {
                            idDepartamento = x.Direccion.Departamento.Id,
                            nombre = x.Direccion.Departamento.Nombre
                        },
                        descripcion = x.Direccion.ReferenciaDireccion,
                        IdGuid = x.Id,
                        municipioRequest = new MunicipioRequest()
                        {
                            idDepartamento = x.Direccion.Municipio.DepartamentoId,
                            idMunicipio = x.Direccion.Municipio.Id,
                            nombre = x.Direccion.Municipio.Nombre
                        }

                    },
                    userFirmaRequest = new UserRequest()
                    {
                        Id = x.Firma.User.Id
                    },
                    auditoriaRequest = new AuditoriaRequest()
                    {
                        fechaCreo = x.Auditoria.FechaCreacion,
                        fechaModifico = x.Auditoria.FechaActualizacion,
                        usuarioCreo = x.Auditoria.CreadoPor,
                        usuarioModifico = x.Auditoria.ActualizadoPor
                    }
                }),
                auditoriaRequest = new AuditoriaRequest()
                    {
                        fechaCreo = empresa.Auditoria.FechaCreacion,
                        fechaModifico = empresa.Auditoria.FechaActualizacion,
                        usuarioCreo = empresa.Auditoria.CreadoPor,
                        usuarioModifico = empresa.Auditoria.ActualizadoPor
                    }
            };
            return empresaRequest;
        }
        
    }
}