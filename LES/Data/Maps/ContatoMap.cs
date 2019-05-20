using LES.Models;
using System.Data.Entity.ModelConfiguration;

namespace LES.Data.Maps
{
	public class ContatoMap : EntityTypeConfiguration<Contato>
	{
		public ContatoMap()
		{
			ToTable("Contatos");

			HasKey(x => x.Id);
			
			Property(x => x.Email)
				.IsRequired();

			Property(x => x.Valor)
				.IsRequired()
				.HasMaxLength(250);

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