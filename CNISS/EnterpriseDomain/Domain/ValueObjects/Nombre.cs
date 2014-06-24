namespace CNISS.EnterpriseDomain.Domain.ValueObjects
{
    public class Nombre
    {
        public virtual string primerNombre { get; set; }
        public virtual  string segundoNombre { get; set; }
        public virtual string primerApellido { get; set; }
        public virtual string  segundoApellido { get; set; }

        public Nombre(string primerNombre, string segundoNombre, string primerApellido, string segundoApellido)
        {
            this.primerNombre = primerNombre;
            this.segundoNombre = segundoNombre;
            this.primerApellido = primerApellido;
            this.segundoApellido = segundoApellido;
        }

        protected Nombre()
        {
            
        }
    }
}