using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Domain;

namespace CNISS.EnterpriseDomain.Domain.Entities
{
    public class PersonRNP:Entity<string>
    {
        public virtual  string names { get; set; }
        public virtual string firstSurname { get; set; }
        public virtual string secondSurname { get; set; }
        public virtual DateTime dateBirth { get; set; }
        
    }
}