using LES.Models;
using System.Data.Entity.ModelConfiguration;

namespace LES.Data.Maps
{
	public class EstadoMap : EntityTypeConfiguration<Estado>
	{
		public EstadoMap()
		{
			ToTable("Estados");

			HasKey(x => x.Id);

			Property(x => x.Nome)
				.IsRequired()
				.HasMaxLength(50);

			Property(x => x.UF)
				.IsRequired()
				.HasMaxLength(2);

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