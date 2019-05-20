using LES.Models;
using System.Data.Entity.ModelConfiguration;

namespace LES.Data.Maps
{
	public class AluguelMap : EntityTypeConfiguration<Aluguel>
	{
		public AluguelMap()
		{
			ToTable("Alugueis");

			HasKey(x => x.Id);
						
			Property(x => x.DataCadastro)
				.IsRequired();

			Property(x => x.DataPrevistaDevolucao)
				.IsRequired();

			Property(x => x.DataDevolucao)
				.IsOptional();
			
			Property(x => x.Ativo)
				.IsRequired();

			HasRequired(x => x.Livro)
				.WithMany()
				.Map(x => x.MapKey("LivroId"));

			HasOptional(x => x.Multa)
				.WithOptionalDependent(x => x.Aluguel)
				.Map(x => x.MapKey("MultaId"));

			HasRequired(x => x.Cliente)
				.WithMany()
				.Map(x => x.MapKey("ClienteId"));

			HasRequired(x => x.Unidade)
				.WithMany()
				.Map(x => x.MapKey("UnidadeId"));

			HasRequired(x => x.UsuarioCadastro)
				.WithMany()
				.Map(x => x.MapKey("UsuarioCadastroId"));
		}
	}
}