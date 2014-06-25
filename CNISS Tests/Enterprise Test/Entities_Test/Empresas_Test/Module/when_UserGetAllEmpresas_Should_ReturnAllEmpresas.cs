using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Compilation;
using CNISS.CommonDomain.Ports.Input.REST.Modules.EmpresaModule.Query;
using CNISS.CommonDomain.Ports.Input.REST.Request.EmpresaRequest;
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
using NHibernate.Linq;
using It = Machine.Specifications.It;

namespace CNISS_Tests.Enterprise_Test.Entities_Test.Empresas_Test.Module
{
    [Subject(typeof (EmpresaModuleQuery))]
    public class when_UserGetAllEmpresas_Should_ReturnAllEmpresas
    {
        static Browser _browser;
        static BrowserResponse _response;
        private static IEmpresaRepositoryReadOnly _repositoryRead;

        private static IEnumerable<EmpresaRequest> _expectedResponse;

        private static IEnumerable<EmpresaRequest> _responseEmpresas; 
        private Establish context = () =>
        {
            var empresas = Builder<Empresa>.CreateListOfSize(10).All().WithConstructor(
                () =>
                    new Empresa(Builder<RTN>.CreateNew().Build(), "nombre", new DateTime(2014, 3, 1),
                        Builder<Gremio>.CreateNew().WithConstructor(() => new Gremio(
                           new RTN("08011985123960"), Builder<RepresentanteLegal>.CreateNew().Build(),
                            Builder<Direccion>.CreateNew().Build(), "gremio")).Build())
                ).Build();

           
           
        
            _expectedResponse = getEmpresaRequests(empresas);


            _repositoryRead = Mock.Of<IEmpresaRepositoryReadOnly>();
            Mock.Get(_repositoryRead).Setup(x => x.getAll()).Returns(empresas);


            _browser = new Browser(
                x =>
                {
                    x.Module<EmpresaModuleQuery>();
                    x.Dependencies(_repositoryRead);
                }

                );


        };

        private Because of = () =>
        {
            _responseEmpresas = _browser.GetSecureJson("/enterprise").Body.DeserializeJson<IEnumerable<EmpresaRequest>>();
        };

        It should_return_all_empresas = () => _responseEmpresas.ShouldBeEquivalentTo(_expectedResponse);

        private static IEnumerable<EmpresaRequest> getEmpresaRequests(IEnumerable<Empresa> empresas)
        {
            return empresas.Select(x => new EmpresaRequest()
            {
                
                contentFile = "",
                empleadosTotales = x.empleadosTotales,
                fechaIngreso = x.fechaIngreso,
                gremioRequest = new GremioRequest(),
                actividadEconomicaRequests = new List<ActividadEconomicaRequest>(),
                sucursalRequests = new List<SucursalRequest>(),
                nombre = x.nombre, 
                programaPiloto = x.proyectoPiloto,
                rtnRequest = new RTNRequest() { RTN = x.Id.rtn}
                
            }
                ).ToList();

        }

    }
}