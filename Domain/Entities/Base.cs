using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;

namespace Domain.Entities
{
    public abstract class Base
    {

        public long Id { get; set; }

        internal List<string> _errors;  
        public IReadOnlyCollection<string> Errors => _errors;
        
        public abstract bool Validate();
    }
}