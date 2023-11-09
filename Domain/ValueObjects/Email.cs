using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Domain.ValueObjects
{
    public partial class Email
    {
        public Email()
        {}

        public Email(string adress)
        {
            if (string.IsNullOrEmpty(adress))
                throw new Exception("E-mail invalido"); 
            
            Adress = adress.Trim().ToLower();

            if (Adress.Length < 5)
                throw new Exception("E-mail invalido");
        }

        public string Adress { get;}

        public static implicit operator String(Email email) 
            => email.ToString();

        public static implicit operator Email(string adress)
            => new Email(adress);
        public override string ToString()
            => Adress.Trim().ToLower();

    }
}