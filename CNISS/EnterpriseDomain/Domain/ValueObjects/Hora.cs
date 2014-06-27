using System;

namespace CNISS.EnterpriseDomain.Domain.ValueObjects
{
    public class Hora
    {
        protected Hora()
        {
            
        }

        public Hora(int hora, int minutos,string parte)
        {
          
            if (hora > 12 || hora < 1)
            {
                throw new ArgumentException();
            }

            if (minutos>59)
            {
                throw new ArgumentException();
            }

            if (minutos < 0)
            {
                throw new ArgumentException();
            }

            if (!(parte == "AM" || parte == "PM"))
            {
                throw new ArgumentException();
            }
            this.hora = hora;
            this.minutos = minutos;
            this.parte = parte;

        }

        public virtual int hora { get; protected set; }
        public virtual int minutos { get; protected set; }
        public virtual string parte { get; set; }
      

    }
}