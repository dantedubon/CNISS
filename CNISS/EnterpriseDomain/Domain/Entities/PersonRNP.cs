using System;
using CNISS.CommonDomain.Domain;

namespace CNISS.EnterpriseDomain.Domain.Entities
{
    public class PersonRNP:Entity<string>
    {
        public virtual  string Names { get; set; }
        public virtual string FirstSurname { get; set; }
        public virtual string SecondSurname { get; set; }
        public virtual DateTime DateBirth { get; set; }
        
    }
}