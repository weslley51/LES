using LES.Models;
using System.Data.Entity.ModelConfiguration;

namespace LES.Data.Maps
{
	public class LivroMap : EntityTypeConfiguration<Livro>
	{
		public LivroMap()
		{
			ToTable("Livros");

			HasKey(x => x.Id);
						
			Property(x => x.CodigoBiblioteca)
				.IsOptional()
				.HasMaxLength(100);

			Property(x => x.Observacoes)
				.IsOptional()
				.HasMaxLength(300);

			Property(x => x.DataCadastro)
				.IsRequired();

			Property(x => x.Ativo)
				.IsRequired();

			HasRequired(x => x.Titulo)
				.WithMany()
				.Map(x => x.MapKey("TituloId"));

			HasOptional(x => x.Unidade)
				.WithMany()
				.Map(x => x.MapKey("UnidadeId"));

			HasRequired(x => x.UsuarioCadastro)
				.WithMany()
				.Map(x => x.MapKey("UsuarioCadastroId"));
		}
	}
}