using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using CNISS.CommonDomain.Domain;
using FluentNHibernate.Conventions.AcceptanceCriteria;

namespace CNISS.EnterpriseDomain.Domain
{
    public class RTN:ValueObject<string>
    {
        protected RTN()
        {
            
        }

        public RTN(string rtn)
        {
            this.Id = rtn;
        }

        public bool isRTNValid()
        {
            var rtn = this.Id;
            if (string.IsNullOrEmpty(rtn)) throw new ArgumentNullException("rtn");
            if (rtn == @"00000000000000") return false;
            if (rtn.Length != 14) return false;
            var rucArray = new StringBuilder(rtn);
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
}