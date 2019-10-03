using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Treinamento.Models;

namespace Treinamento.Mapping
{
    class ProdutoMap : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            // Table & Column Mappings
            builder.ToTable("tbProduto");
            builder.Property(x => x.Id).HasColumnName("tbp_ID");
            builder.Property(x => x.Descricao).HasColumnName("tbp_Descricao").IsRequired();
            builder.Property(x => x.Ativo).HasColumnName("tbp_Ativo").IsRequired();

        }
    }
}
