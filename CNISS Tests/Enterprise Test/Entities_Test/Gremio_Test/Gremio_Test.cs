using System;
using CNISS.EnterpriseDomain.Domain;
using CNISS.EnterpriseDomain.Domain.Entities;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using FizzWare.NBuilder;
using NUnit.Framework;

namespace CNISS_Tests.Enterprise_Test.Entities_Test.Gremio_Test
{
    [TestFixture]
    public class Gremio_Test
    {
        [Test]
        public void constructor_RTNInvalido_LanzaExcepcion()
        {
            String nombreGremio = "Camara Comercio";
            RTN rtn = new RTN("0801198812121121212");
            var representanteLegal = this.representanteLegal();
            var direccion = this.direccion();

            Assert.Throws<ArgumentException>(
                ()=> new Gremio(rtn,representanteLegal,direccion,nombreGremio), "rtn Invalido"
                );



        }


        [Test]
        public void constructor_RepresentanteLegalNulo_LanzaExcepcion()
        {
            String nombreGremio = "Camara Comercio";
            RTN rtn = new RTN("08011985123960");
          
            var direccion = this.direccion();


            Assert.Throws<ArgumentNullException>(
                () => new Gremio(rtn,null,direccion,nombreGremio), "Representante Legal Nulo"
                );

        }

        [Test]
        public void constructor_DireccionNula_LanzaExcepcion()
        {
            String nombreGremio = "Camara Comercio";
            RTN rtn = new RTN("08011985123960");
            var representanteLegal = this.representanteLegal();
            Assert.Throws<ArgumentNullException>(
                () => new Gremio(rtn, representanteLegal, null, nombreGremio), "Direccion Nulo"
                );

        }

        [Test]
        public void constructor_NombreNulo_LanzaExcepcion()
        {
            String nombreGremio = string.Empty;
            RTN rtn = new RTN("08011985123960");
            var representanteLegal = this.representanteLegal();
            var direccion = this.direccion();
            Assert.Throws<ArgumentNullException>(
                () => new Gremio(rtn, representanteLegal, direccion, nombreGremio), "Nombre Nulo"
                );
        }

        private Direccion direccion()
        {
            Departamento departamento = Builder<Departamento>.CreateNew().Build();
            Municipio municipioGremio = Builder<Municipio>.CreateNew().Build();
            Direccion direccion = new Direccion(departamento,municipioGremio, "B Abajo");
            return direccion;
        }

        private RepresentanteLegal representanteLegal()
        {
            Identidad identidadRepresentante = new Identidad("0801198512396");
            RepresentanteLegal representanteLegal = new RepresentanteLegal(identidadRepresentante, "Juan Perez");
            return representanteLegal;
        }
    }
}