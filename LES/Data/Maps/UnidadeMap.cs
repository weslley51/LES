using LES.Models;
using System.Data.Entity.ModelConfiguration;

namespace LES.Data.Maps
{
	public class UnidadeMap : EntityTypeConfiguration<Unidade>
	{
		public UnidadeMap()
		{
			ToTable("Unidades");

			HasKey(x => x.Id);
						
			Property(x => x.Nome)
				.IsRequired()
				.HasMaxLength(100);

			Property(x => x.DataCadastro)
				.IsRequired();

			Property(x => x.Ativo)
				.IsRequired();

			HasRequired(x => x.Endereco)
				.WithOptional()
				.Map(x => x.MapKey("EnderecoId"));

			HasRequired(x => x.UsuarioCadastro)
				.WithMany()
				.Map(x => x.MapKey("UsuarioCadastroId"));

			HasMany(x => x.Contatos)
				.WithMany()
					.Map(c => c.ToTable("ContatosUnidade")
						.MapRightKey("ContatoId")
						.MapLeftKey("UnidadeId")
				);
		}
	}
}