using LES.Models;
using LES.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace LES.Data.Maps
{
	public class UsuarioMap : EntityTypeConfiguration<Usuario>
	{
		public UsuarioMap()
		{
			ToTable("Usuarios");

			HasKey(x => x.Id);
			
			Property(x => x.Login)
				.IsRequired()
				.HasMaxLength(20);

			Property(x => x.Nome)
				.IsRequired()
				.HasMaxLength(200);

			Property(x => x.Email)
				.IsRequired()
				.HasMaxLength(150);

			Property(x => x.Senha)
				.IsRequired()
				.HasMaxLength(32);

			Property(x => x.DataCadastro)
				.IsRequired();

			Property(x => x.Ativo)
				.IsRequired();

			HasRequired(x => x.UsuarioCadastro)
				.WithMany()
				.Map(x => x.MapKey("UsuarioCadastroId"));

			HasOptional(x => x.Perfil)
				.WithMany()
				.Map(x => x.MapKey("PerfilId"));
						
			HasOptional(x => x.Unidade)
				.WithMany()
				.Map(x => x.MapKey("UnidadeId"));			
		}
	}
}