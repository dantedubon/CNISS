namespace CNISS.EnterpriseDomain.Domain.ValueObjects
{
    public class Nombre
    {
        public virtual string Nombres { get; set; }
      
        public virtual string PrimerApellido { get; set; }
        public virtual string  SegundoApellido { get; set; }

        public Nombre(string nombres, string primerApellido, string segundoApellido)
        {
            this.Nombres = nombres;
            
            this.PrimerApellido = primerApellido;
            this.SegundoApellido = segundoApellido;
        }

        protected Nombre()
        {
            
        }
    }
}