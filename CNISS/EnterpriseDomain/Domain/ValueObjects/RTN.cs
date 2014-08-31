using System;
using System.Text;
using CNISS.CommonDomain.Domain;

namespace CNISS.EnterpriseDomain.Domain
{
    public class RTN:IDomainObjectNotIdentified, IEquatable<RTN>
    {
        public virtual string Rtn { get; set; }

        protected RTN()
        {

        }
      

        public bool Equals(RTN other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Rtn, other.Rtn);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((RTN) obj);
        }

        public override int GetHashCode()
        {
            return (Rtn != null ? Rtn.GetHashCode() : 0);
        }

        public RTN(string rtn)
        {
            this.Rtn = rtn;
        }

        public bool isRTNValid()
        {
           
            if (string.IsNullOrEmpty(Rtn)) throw new ArgumentNullException("rtn");
            if (Rtn == @"00000000000000") return false;
            if (Rtn.Length != 14) return false;
            var rucArray = new StringBuilder(Rtn);
            if (!Char.IsDigit(rucArray[0]))
                return false;
            var verificadorOriginal = Convert.ToUInt32(rucArray[13].ToString());
            return verificadorOriginal == getCheckDigit(rucArray); 
        }


        private  int getCheckDigit(StringBuilder rtn)
        {
            if (rtn == null) throw new ArgumentNullException("rtn");

            var acum = 0;
            for (var i = 1; i <= 13; i++)
            {
                var digito = rtn[i - 1].ToString();
                var aux = Convert.ToInt32(digito);

                if (i == 1 || i == 5 || i == 9 || i == 13)
                    acum = acum + (aux * 1);

                if (i == 4 || i == 8 || i == 12)
                    acum = acum + (aux * 3);

                if (i == 3 || i == 7 || i == 11)
                    acum = acum + (aux * 5);

                if (i == 2 || i == 6 || i == 10)
                    acum = acum + (aux * 7);
            }
            var verificador = acum % 11;
            if (verificador == 10)
            {
                verificador = 0;
            }
            return verificador;
        }

    }

    public class RTNNull : RTN
    {
        public virtual string rtn { get { return string.Empty; } }
    }
}