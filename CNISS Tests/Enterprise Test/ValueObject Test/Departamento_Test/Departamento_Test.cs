using System.Collections.Generic;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using FizzWare.NBuilder;
using NUnit.Framework;

namespace CNISS_Tests.Enterprise_Test.ValueObject_Test
{
    [TestFixture]
    public class Departamento_Test
    {
        [Test]
        public void isMuncipioFromDepartamento_MunicipioValido_ReturnTrue()
        {
            var idMunicipio = "municipio1";
            var idDepartamento = "departamento1";
            
            var municipio = Builder<Municipio>.CreateNew()
                .With(x => x.Id = idMunicipio)
                .With(x => x.DepartamentoId = idDepartamento)
                .Build();
            var departamento = Builder<Departamento>.CreateNew()
                .With(x => x.Id = idDepartamento)
                .With(x => x.Municipios = new List<Municipio>
                 {
                     municipio
                 })
                .Build();


            bool respuesta = departamento.isMunicipioFromDepartamento(municipio);

            Assert.IsTrue(respuesta);
        }

        [Test]
        public void isMunicipioFromDepartamento_MunicipioInvalido_ReturnFalse()
        {
            var idMunicipio = "municipio1";
            var idDepartamento = "departamento1";

            var municipio = Builder<Municipio>.CreateNew()
                .With(x => x.Id = idMunicipio)
                .With(x => x.DepartamentoId = idDepartamento)
                .Build();
            var departamento = Builder<Departamento>.CreateNew()
                .With(x => x.Id = idDepartamento)
                .With(x => x.Municipios = new List<Municipio>
                 {
                     municipio
                 })
                .Build();

            var municipioInvalido = Builder<Municipio>.CreateNew().Build();

            bool respuesta = departamento.isMunicipioFromDepartamento(municipioInvalido);

            Assert.IsFalse(respuesta);
        }
    }
}