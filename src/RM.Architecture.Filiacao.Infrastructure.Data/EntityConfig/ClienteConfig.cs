using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using RM.Architecture.Filiacao.Domain.Entities.Cliente;

namespace RM.Architecture.Filiacao.Infrastructure.Data.EntityConfig
{
    public class ClienteConfig : EntityTypeConfiguration<Cliente>
    {
        public ClienteConfig()
        {
            HasKey(c => c.CodCliente);

            Property(c => c.Nome)
                .IsRequired()
                .HasMaxLength(150);

            Property(c => c.Cpf.Numero)
                .HasColumnName("Cpf")
                .IsRequired()
                .HasMaxLength(11)
                .IsFixedLength()
                .HasColumnAnnotation("Index", new IndexAnnotation(
                    new IndexAttribute("IX_CPF") {IsUnique = true}));

            Property(c => c.Email.Endereco)
                .HasColumnName("Email")
                .IsRequired()
                .HasMaxLength(100);

            Property(c => c.DataNascimento)
                .IsRequired();

            Property(c => c.Ativo)
                .IsRequired();

            Ignore(c => c.ValidationResult);

            ToTable("Clientes");
        }
    }
}