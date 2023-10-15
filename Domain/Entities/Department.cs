using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Exceptions;
using Domain.Validators;

namespace Domain.Entities
{
    public class Department : Base
    {

        protected Department() { }

        public Department(string departmentName, int employeeLimit)
        {
            DepartmentName = departmentName;
            EmployeeLimit = employeeLimit;

            _errors = new List<string>();
            Validate();
        }

        public string DepartmentName { get; private set; }
        public int EmployeeLimit { get; private set; }
        public List<Employee> Employees { get; set; }

        public bool AtingiuLimiteFuncionarios()
        {
            return Employees.Count >= EmployeeLimit;
        }

        public override bool Validate()
        {
            var validator = new DepartmentValidator();
            var validation = validator.Validate(this);

            if (!validation.IsValid)
            {
                foreach (var error in validation.Errors)
                    _errors.Add(error.ErrorMessage);

                throw new DomainExceptions("Alguns campos estão inválidos, por favor corrija-os!", _errors);
            }

            return true;
        }

        public string ErrorsToString()
        {
            var builder = new StringBuilder();

            foreach (var error in _errors)
                builder.AppendLine(error);

            return builder.ToString();
        }
    }
}