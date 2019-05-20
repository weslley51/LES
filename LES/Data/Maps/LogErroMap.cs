using LES.Models;
using LES.Models;
using System.Data.Entity.ModelConfiguration;

namespace LES.Data.Maps
{
	public class LogErroMap : EntityTypeConfiguration<LogErro>
	{
		public LogErroMap()
		{
			ToTable("LogsErro");

			HasKey(x => x.Id);

			Ignore(x => x.Ativo);

			Property(x => x.MensagemPrincipal)
				.IsRequired()
				.HasMaxLength(200);

			Property(x => x.OutrasMensagens)
				.IsRequired()
				.HasMaxLength(2000);

			Property(x => x.DataCadastro)
				.IsRequired();
			
			HasRequired(x => x.UsuarioCadastro)
				.WithMany()
				.Map(x => x.MapKey("UsuarioCadastroId"));
		}
	}
}