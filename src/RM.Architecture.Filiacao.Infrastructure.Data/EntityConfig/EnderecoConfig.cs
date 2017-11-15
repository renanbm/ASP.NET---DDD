using System.Data.Entity.ModelConfiguration;
using RM.Architecture.Filiacao.Domain.Entities.Endereco;

namespace RM.Architecture.Filiacao.Infrastructure.Data.EntityConfig
{
    public class EnderecoConfig : EntityTypeConfiguration<Endereco>
    {
        public EnderecoConfig()
        {
            HasKey(e => e.CodEndereco);

            Property(e => e.Logradouro)
                .IsRequired()
                .HasMaxLength(150);

            Property(e => e.Numero)
                .IsRequired()
                .HasMaxLength(20);

            Property(e => e.Bairro)
                .IsRequired()
                .HasMaxLength(50);

            Property(e => e.Cep)
                .IsRequired()
                .HasMaxLength(8)
                .IsFixedLength();

            Property(e => e.Complemento)
                .HasMaxLength(100);

            HasRequired(e => e.Cliente)
                .WithMany(c => c.Enderecos)
                .HasForeignKey(e => e.CodCliente);

            ToTable("Enderecos");
        }
    }
}