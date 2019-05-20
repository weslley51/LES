using LES.Models;
using System.Data.Entity.ModelConfiguration;

namespace LES.Data.Maps
{
	public class TituloMap : EntityTypeConfiguration<Titulo>
	{
		public TituloMap()
		{
			ToTable("Titulos");

			HasKey(x => x.Id);
						
			Property(x => x.Nome)
				.IsRequired()
				.HasMaxLength(300);

			Property(x => x.Descricao)
				.IsOptional()
				.HasMaxLength(200);

			Property(x => x.Sinopse)
				.IsOptional()
				.HasMaxLength(500);

			Property(x => x.AnoPublicacao)
				.IsRequired();

			Property(x => x.QuantiaFolhas)
				.IsRequired();

			Property(x => x.PrazoDevolucao)
				.IsRequired();

			Property(x => x.DataCadastro)
				.IsRequired();

			Property(x => x.Ativo)
				.IsRequired();

			HasRequired(x => x.Editora)
				.WithMany()
				.Map(x => x.MapKey("EditoraId"));

			HasRequired(x => x.UsuarioCadastro)
				.WithMany()
				.Map(x => x.MapKey("UsuarioCadastroId"));

			HasMany(x => x.Generos)
				.WithMany()
				.Map(x =>
					{
						x.ToTable("GenerosTitulo");
						x.MapRightKey("GeneroId");
						x.MapLeftKey("TituloId");
					});

			HasMany(x => x.Autores)
				.WithMany()
				.Map(x =>
				{
					x.ToTable("AutoresTitulo");
					x.MapRightKey("AutorId");
					x.MapLeftKey("TituloId");
				});
		}
	}
}