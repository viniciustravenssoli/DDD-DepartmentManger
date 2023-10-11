using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Mappings
{
    public class PayrollValidator : IEntityTypeConfiguration<Payroll>
    {
        public void Configure(EntityTypeBuilder<Payroll> builder)
        {
            builder.ToTable("Payrolls");

            builder.Property(x => x.ImpostoFgts)
                .IsRequired()
                .HasMaxLength(10)
                .HasColumnName("ImpostoFgts");

            builder.Property(x => x.ImpostoInss)
                .IsRequired()
                .HasMaxLength(10)
                .HasColumnName("ImpostoInss");
            
            builder.Property(x => x.ImpostoIrrf)
                .IsRequired()
                .HasMaxLength(10)
                .HasColumnName("ImpostoIrrf");
            
            builder.Property(x => x.SalarioBruto)
                .IsRequired()
                .HasMaxLength(15)
                .HasColumnName("salarioBruto");
            
            builder.Property(x => x.SalarioLiquido)
                .IsRequired()
                .HasMaxLength(15)
                .HasColumnName("salarioLiquido");

            builder.Property(x => x.Valor)
                .IsRequired()
                .HasMaxLength(9)
                .HasColumnName("valorHora");

            builder.Property(x => x.Quantidade)
                .IsRequired()
                .HasMaxLength(2)
                .HasColumnName("quantidadeDeHoras");
        }
    }
}