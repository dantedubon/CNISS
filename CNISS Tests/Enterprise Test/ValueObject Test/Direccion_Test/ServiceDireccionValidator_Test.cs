using System.Collections.Generic;
using CNISS.EnterpriseDomain.Application;
using CNISS.EnterpriseDomain.Domain.Repositories;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using FizzWare.NBuilder;
using Moq;
using NUnit.Framework;

namespace CNISS_Tests.Enterprise_Test.ValueObject_Test.DireccionTest
{
    [TestFixture]
    public class ServiceDireccionValidator_Test
    {
        public object[] badRequestForPost;

        public ServiceDireccionValidator_Test()
        {
            badRequestForPost = new object[]
            {
                new object[]{getDireccion("02","66","02","Barrio Abajo")},
                new object[]{getDireccion("66","66","02","Barrio Abajo")}

            };
        }

        [TestCaseSource("badRequestForPost")]
        public void isValidDireccion_dataInvalid_returnFalse(Direccion direccion)
        {
            
            var municipiosDepartamento = new List<Municipio>() {new Municipio("01", "02", "La Merced")};
            var departamento = new Departamento() {Id = "02", Municipios = municipiosDepartamento, Nombre = "Departamento"};
            var repositorio = Mock.Of<IDepartamentRepositoryReadOnly>();
            Mock.Get(repositorio).Setup(x => x.get("02")).Returns(departamento);

            var servicio = new ServiceDireccionValidator(repositorio);

            Assert.IsFalse(servicio.isValidDireccion(direccion));
            
        }

        private Direccion getDireccion(string idDepartamentoMunicipio, string idMunicipio, string idDepartamento, string descripcion)
        {
            var municipio = Builder<Municipio>.CreateNew().Build();
            municipio.DepartamentoId = idDepartamentoMunicipio;
            municipio.Id = idMunicipio;
            municipio.Nombre = "municipio";
            var departamento = Builder<Departamento>.CreateNew().Build();
            departamento.Id = idDepartamento;
            departamento.Nombre = "departamento";


            return new Direccion(departamento, municipio, descripcion);

        }
    }
}