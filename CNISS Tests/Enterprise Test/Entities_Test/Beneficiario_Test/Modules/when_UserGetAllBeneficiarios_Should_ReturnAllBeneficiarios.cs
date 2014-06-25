using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Ports.Input.REST.Modules.BeneficiarioModule.Query;
using CNISS.CommonDomain.Ports.Input.REST.Request.BeneficiarioRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.GremioRequest;
using CNISS.EnterpriseDomain.Domain.Entities;
using CNISS.EnterpriseDomain.Domain.Repositories;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using FizzWare.NBuilder;
using FluentAssertions;
using Machine.Specifications;
using Moq;
using Nancy.Testing;
using It = Machine.Specifications.It;

namespace CNISS_Tests.Enterprise_Test.Entities_Test.Beneficiario_Test.Modules
{
    [Subject(typeof (BeneficiarioModuleQuery))]
    public class when_UserGetAllBeneficiarios_Should_ReturnAllBeneficiarios
    {
        static Browser _browser;
        static BrowserResponse _response;
        private static IEnumerable<BeneficiarioRequest> _expectedResponse;
        private static IEnumerable<BeneficiarioRequest> _responseBeneficiarios; 
        private Establish context = () =>
        {
            var beneficiarios =
                Builder<Beneficiario>.CreateListOfSize(10).All().WithConstructor(() =>
                    new Beneficiario(Builder<Identidad>.CreateNew().Build(), Builder<Nombre>.CreateNew().Build(),
                        new DateTime(1984,8,2))
                    ).All().Build();
                    
                   
            _expectedResponse = getRequests(beneficiarios);
            var repository = Mock.Of<IBeneficiarioRepositoryReadOnly>();

            Mock.Get(repository).Setup(x => x.getAll()).Returns(beneficiarios);

            _browser = new Browser(
                x =>
                {
                    x.Module<BeneficiarioModuleQuery>();
                    x.Dependencies(repository);
                }
                
                );
        };

        private Because of = () =>
        {
            _responseBeneficiarios =
                _browser.GetSecureJson("/enterprise/beneficiarios")
                    .Body.DeserializeJson<IEnumerable<BeneficiarioRequest>>();
        };

        It should_return_all_beneficiarios = () => _responseBeneficiarios.ShouldBeEquivalentTo(_expectedResponse);

        private static IEnumerable<BeneficiarioRequest> getRequests(IEnumerable<Beneficiario> beneficiarios)
        {
            return beneficiarios.Select(x=> new BeneficiarioRequest()
            {
                identidadRequest = new IdentidadRequest() { identidad = x.Id.identidad},
                fechaNacimiento = x.fechaNacimiento,
                nombreRequest = new NombreRequest()
                {
                    nombres = x.nombre.nombres,
                    primerApellido = x.nombre.primerApellido,
                    segundoApellido = x.nombre.segundoApellido
                },
                dependienteRequests = new List<DependienteRequest>()
                
            }
                
                
                );
               
            
        }
    }
}