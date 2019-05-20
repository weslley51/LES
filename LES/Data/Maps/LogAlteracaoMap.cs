using LES.Models;
using LES.Models;
using System.Data.Entity.ModelConfiguration;

namespace LES.Data.Maps
{
	public class LogAlteracaoMap : EntityTypeConfiguration<LogAlteracao>
	{
		public LogAlteracaoMap()
		{
			ToTable("LogsAlteracao");

			HasKey(x => x.Id);

			Ignore(x => x.Ativo);

			Property(x => x.NomeEntidade)
				.IsRequired()
				.HasMaxLength(100);

			Property(x => x.ObjetoAlterado)
				.IsRequired()
				.IsMaxLength();

			Property(x => x.ObjetoPrincipal)
				.IsRequired()
				.IsMaxLength();

			Property(x => x.DataCadastro)
				.IsRequired();
			
			HasRequired(x => x.UsuarioCadastro)
				.WithMany()
				.Map(x => x.MapKey("UsuarioCadastroId"));
		}
	}
}