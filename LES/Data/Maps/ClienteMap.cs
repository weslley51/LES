using LES.Models;
using System.Data.Entity.ModelConfiguration;

namespace LES.Data.Maps
{
	public class ClienteMap : EntityTypeConfiguration<Cliente>
	{
		public ClienteMap()
		{
			ToTable("Clientes");

			HasKey(x => x.Id);

			Property(x => x.NomeCompleto)
				.IsRequired()
				.HasMaxLength(300);
			
			Property(x => x.CPF)
				.IsRequired()
				.HasMaxLength(11);

			Property(x => x.RG)
				.IsRequired()				
				.HasMaxLength(9);

			Property(x => x.Sexo)
				.IsRequired();

			Property(x => x.Saldo)
				.IsOptional();

			Property(x => x.DataCadastro)
				.IsRequired();

			Property(x => x.Ativo)
				.IsRequired();

			HasRequired(x => x.Endereco)
				.WithOptional()
				.Map(x => x.MapKey("EnderecoId"));

			HasMany(x => x.Contatos)
				.WithMany()
					.Map(c => c.ToTable("ContatosCliente")
						.MapRightKey("ContatoId")
						.MapLeftKey("ClienteId")
				);

			HasRequired(x => x.UsuarioCadastro)
				.WithMany()
				.Map(x => x.MapKey("UsuarioCadastroId"));
		}
	}
}