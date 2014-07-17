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
               ).With(x => x.auditoria = Builder<Auditoria>.CreateNew().Build()).Build();

            empresa.actividadesEconomicas = Builder<ActividadEconomica>.CreateListOfSize(3).Build();
            empresa.sucursales = Builder<Sucursal>.CreateListOfSize(3).All().WithConstructor(
                ()=> new Sucursal("Sucursal",Builder<Direccion>.CreateNew().WithConstructor(
                    ()=> new Direccion(Builder<Departamento>.CreateNew().Build(),
                        Builder<Municipio>.CreateNew().Build(),"referencia")
                    ).Build()
                    
                    
                    ,Builder<FirmaAutorizada>.CreateNew().WithConstructor(
                    ()=> new FirmaAutorizada(Builder<User>.CreateNew().Build(),new DateTime(2014,3,1))
                    
                    ).Build())
                ).With(x => x.auditoria = Builder<Auditoria>.CreateNew().Build()).Build();




          
            _repositoryRead = Mock.Of<IEmpresaRepositoryReadOnly>();
            Mock.Get(_repositoryRead).Setup(x => x.get(Moq.It.IsAny<RTN>())).Returns(empresa);

            _expectedResponse = getEmpresaRequest(empresa);

            _browser = new Browser(
                x =>
                {
                    x.Module<EmpresaModuleQuery>();
                    x.Dependencies(_repositoryRead);
                }

                );

        };

        private Because of = () => { _responsEmpresaRequest = _browser.GetSecureJson("/enterprise/id=", request).Body.DeserializeJson<EmpresaRequest>(); };

        private It should_return_empresa = () => _responsEmpresaRequest.ShouldBeEquivalentTo(_expectedResponse);


        private static EmpresaRequest getEmpresaRequest(Empresa empresa)
        {
            var empresaRequest = new EmpresaRequest()
            {
                nombre = empresa.nombre,
                actividadEconomicaRequests = empresa.actividadesEconomicas.Select(x => new ActividadEconomicaRequest()
                {
                    descripcion = x.descripcion,
                    guid = x.Id
                }),
                contentFile = "",
                empleadosTotales = empresa.empleadosTotales,
                fechaIngreso = empresa.fechaIngreso,
                gremioRequest = new GremioRequest()
                {
                    nombre = empresa.gremial.nombre,
                    rtnRequest = new RTNRequest()
                    {
                        RTN = empresa.gremial.Id.rtn
                    }
                },
                programaPiloto = empresa.proyectoPiloto,
                rtnRequest = new RTNRequest() { RTN = empresa.Id.rtn},
                sucursalRequests = empresa.sucursales.Select(x => new SucursalRequest()
                {
                    guid = x.Id,
                    nombre = x.nombre,
                    direccionRequest = new DireccionRequest()
                    {
                        departamentoRequest = new DepartamentoRequest()
                        {
                            idDepartamento = x.direccion.departamento.Id,
                            nombre = x.direccion.departamento.nombre
                        },
                        descripcion = x.direccion.referenciaDireccion,
                        IdGuid = x.Id,
                        municipioRequest = new MunicipioRequest()
                        {
                            idDepartamento = x.direccion.municipio.departamentoId,
                            idMunicipio = x.direccion.municipio.Id,
                            nombre = x.direccion.municipio.nombre
                        }

                    },
                    userFirmaRequest = new UserRequest()
                    {
                        Id = x.firma.user.Id
                    },
                    auditoriaRequest = new AuditoriaRequest()
                    {
                        fechaCreo = x.auditoria.fechaCreo,
                        fechaModifico = x.auditoria.fechaModifico,
                        usuarioCreo = x.auditoria.usuarioCreo,
                        usuarioModifico = x.auditoria.usuarioModifico
                    }
                }),
                auditoriaRequest = new AuditoriaRequest()
                    {
                        fechaCreo = empresa.auditoria.fechaCreo,
                        fechaModifico = empresa.auditoria.fechaModifico,
                        usuarioCreo = empresa.auditoria.usuarioCreo,
                        usuarioModifico = empresa.auditoria.usuarioModifico
                    }
            };
            return empresaRequest;
        }
        
    }
}