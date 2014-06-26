using System;

namespace CNISS.EnterpriseDomain.Domain.ValueObjects
{
    public class Hora
    {
        protected Hora()
        {
            
        }

        public Hora(int hora, int minutos, IParteDia parteDia)
        {
            if (parteDia == null) throw new ArgumentNullException("parteDia");
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
            this.hora = hora;
            this.minutos = minutos;
            this.parteDia = parteDia;
        }

        public virtual int hora { get; protected set; }
        public virtual int minutos { get; protected set; }
        public virtual IParteDia parteDia { get; protected set; }

    }
}