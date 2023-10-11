using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Mappings
{
    public class EmployeeMap : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employees");

            builder.Property(x => x.Cpf)
                .IsRequired()
                .HasMaxLength(11)
                .HasColumnName("Cpf");

            builder.HasIndex(x => x.Cpf)
                .IsUnique();

            builder.Property(x => x.Salario)
                .IsRequired()
                .HasMaxLength(20)
                .HasColumnName("Salario");

            builder.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(180)
                .HasColumnName("Email");

            builder.Property(x => x.Nome)
                .IsRequired()
                .HasMaxLength(120)
                .HasColumnName("Nome");

            builder.Property(x => x.DataDeEntrada)
                .IsRequired()
                .HasColumnName("DataDeEntrada");
            
            builder.Property(x => x.SalarioAnual)
                .IsRequired()
                .HasMaxLength(30)
                .HasColumnName("SalarioAnual");
        }
    }
}