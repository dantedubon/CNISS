using System;

namespace CNISS.EnterpriseDomain.Domain.ValueObjects
{
    public class Hora
    {
        protected Hora()
        {
            
        }

        public Hora(int horaEntera, int minutos,string parte)
        {
          
            if (horaEntera > 12 || horaEntera < 1)
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
            this.HoraEntera = horaEntera;
            this.Minutos = minutos;
            this.Parte = parte;

        }

        public virtual int HoraEntera { get; protected set; }
        public virtual int Minutos { get; protected set; }
        public virtual string Parte { get; set; }
      

    }
}