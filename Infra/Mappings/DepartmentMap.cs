using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Mappings
{
    public class DepartmentMap : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.ToTable("Departments");

            builder.Property(x => x.DepartmentName)
                .IsRequired()
                .HasMaxLength(80)
                .HasColumnName("departmentName");

            builder.Property(x => x.EmployeeLimit)
                .IsRequired()
                .HasMaxLength(5)
                .HasColumnName("employeeLimit");
        }
    }
}