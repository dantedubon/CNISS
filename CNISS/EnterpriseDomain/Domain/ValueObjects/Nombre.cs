namespace CNISS.EnterpriseDomain.Domain.ValueObjects
{
    public class Nombre
    {
        public virtual string nombres { get; set; }
      
        public virtual string primerApellido { get; set; }
        public virtual string  segundoApellido { get; set; }

        public Nombre(string nombres, string primerApellido, string segundoApellido)
        {
            this.nombres = nombres;
            
            this.primerApellido = primerApellido;
            this.segundoApellido = segundoApellido;
        }

        protected Nombre()
        {
            
        }
    }
}