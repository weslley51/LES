using LES.Models;
using System.Data.Entity.ModelConfiguration;

namespace LES.Data.Maps
{
	public class AutorMap : EntityTypeConfiguration<Autor>
	{
		public AutorMap()
		{
			ToTable("Autores");

			HasKey(x => x.Id);

			Property(x => x.Nome)
				.IsRequired()
				.HasMaxLength(100);

			Property(x => x.DataCadastro)
				.IsRequired();

			Property(x => x.Ativo)
				.IsRequired();

			HasRequired(x => x.UsuarioCadastro)
				.WithMany()
				.Map(x => x.MapKey("UsuarioCadastroId"));
		}
	}
}