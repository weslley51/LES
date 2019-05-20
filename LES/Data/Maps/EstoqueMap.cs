using LES.Models;
using System.Data.Entity.ModelConfiguration;

namespace LES.Data.Maps
{
	public class EstoqueMap : EntityTypeConfiguration<Estoque>
	{
		public EstoqueMap()
		{
			ToTable("Estoque");

			HasKey(x => x.Id);

			Property(x => x.TipoMovimentacao)
				.IsRequired();

			Property(x => x.Descricao)
				.IsRequired()
				.HasMaxLength(250);

			Property(x => x.Quantidade)
				.IsRequired();

			Property(x => x.DataCadastro)
				.IsRequired();

			Property(x => x.Ativo)
				.IsRequired();

			HasRequired(x => x.Titulo)
				.WithMany()
				.Map(x => x.MapKey("TituloId"));

			HasRequired(x => x.Unidade)
				.WithMany()
				.Map(x => x.MapKey("UnidadeId"));

			HasRequired(x => x.UsuarioCadastro)
				.WithMany()
				.Map(x => x.MapKey("UsuarioCadastroId"));
		}
	}
}