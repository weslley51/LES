using LES.Models;
using System.Data.Entity.ModelConfiguration;

namespace LES.Data.Maps
{
	public class MultaMap : EntityTypeConfiguration<Multa>
	{
		public MultaMap()
		{
			ToTable("Multas");

			HasKey(x => x.Id);

			Property(x => x.Valor)
				.IsRequired();

			Property(x => x.TipoMulta)
				.IsRequired();

			Property(x => x.DataCadastro)
				.IsRequired();

			Property(x => x.Ativo)
				.IsRequired();

			HasRequired(x => x.Cliente)
				.WithMany()
				.Map(x => x.MapKey("ClienteId"));

			HasRequired(x => x.UsuarioCadastro)
				.WithMany()
				.Map(x => x.MapKey("UsuarioCadastroId"));
		}
	}
}