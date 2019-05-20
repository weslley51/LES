using LES.Models;
using System.Data.Entity.ModelConfiguration;

namespace LES.Data.Maps
{
	public class EnderecoMap : EntityTypeConfiguration<Endereco>
	{
		public EnderecoMap()
		{
			ToTable("Enderecos");

			HasKey(x => x.Id);

			Ignore(x => x.Formatado);

			Property(x => x.Cep)
				.IsRequired()
				.HasMaxLength(8);

			Property(x => x.Logradouro)
				.IsRequired()
				.HasMaxLength(150);

			Property(x => x.Numero)
				.IsRequired()
				.HasMaxLength(10);

			Property(x => x.Bairro)
				.IsRequired()
				.HasMaxLength(100);

			Property(x => x.Municipio)
				.IsRequired()
				.HasMaxLength(70);

			Property(x => x.Complemento)
				.IsOptional()
				.HasMaxLength(50);

			Property(x => x.Observacao)
				.IsOptional()
				.HasMaxLength(250);

			Property(x => x.DataCadastro)
				.IsRequired();

			Property(x => x.Ativo)
				.IsRequired();

			HasRequired(x => x.Estado)
				.WithMany()
				.Map(x => x.MapKey("EstadoId"));

			HasRequired(x => x.UsuarioCadastro)
				.WithMany()
				.Map(x => x.MapKey("UsuarioCadastroId"));
		}
	}
}