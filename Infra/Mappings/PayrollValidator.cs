using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.EntitiesConstants;
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
                .HasMaxLength(PayrollConstants.ImpostoFgts)
                .HasColumnName("ImpostoFgts");

            builder.Property(x => x.ImpostoInss)
                .IsRequired()
                .HasMaxLength(PayrollConstants.ImpostoInss)
                .HasColumnName("ImpostoInss");
            
            builder.Property(x => x.ImpostoIrrf)
                .IsRequired()
                .HasMaxLength(PayrollConstants.ImpostoIrrf)
                .HasColumnName("ImpostoIrrf");
            
            builder.Property(x => x.SalarioBruto)
                .IsRequired()
                .HasMaxLength(PayrollConstants.SalarioBruto)
                .HasColumnName("salarioBruto");
            
            builder.Property(x => x.SalarioLiquido)
                .IsRequired()
                .HasMaxLength(PayrollConstants.SalarioLiquido)
                .HasColumnName("salarioLiquido");

            builder.Property(x => x.Valor)
                .IsRequired()
                .HasMaxLength(PayrollConstants.Valor)
                .HasColumnName("valorHora");

            builder.Property(x => x.Quantidade)
                .IsRequired()
                .HasMaxLength(PayrollConstants.Quantidade)
                .HasColumnName("quantidadeDeHoras");
        }
    }
}