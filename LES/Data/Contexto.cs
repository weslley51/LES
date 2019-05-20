using LES.Models;
using LES.Models;
using LES.Data.Maps;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace LES.Data
{
	public class Contexto : DbContext
	{
		public virtual DbSet<Aluguel> Alugueis { get; set; }
		public virtual DbSet<Autor> Autores { get; set; }
		public virtual DbSet<Cliente> Clientes { get; set; }
		public virtual DbSet<Contato> Contatos { get; set; }
		public virtual DbSet<Editora> Editoras { get; set; }
		public virtual DbSet<Endereco> Enderecos { get; set; }
		public virtual DbSet<Estado> Estados { get; set; }
		public virtual DbSet<Estoque> Estoques { get; set; }		
		public virtual DbSet<Genero> Generos { get; set; }
		public virtual DbSet<Livro> Livros { get; set; }
		public virtual DbSet<LogAlteracao> LogsAlteracao { get; set; }
		public virtual DbSet<LogErro> LogsErro { get; set; }
		public virtual DbSet<Multa> Multas { get; set; }
		public virtual DbSet<Perfil> Perfis { get; set; }
		public virtual DbSet<Titulo> Titulos { get; set; }
		public virtual DbSet<Unidade> Unidades { get; set; }
		public virtual DbSet<Usuario> Usuarios { get; set; }

		public Contexto() : base("name=Contexto")
		{
			Configuration.LazyLoadingEnabled = true;
			Configuration.ProxyCreationEnabled = false;
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			// Desabilitando a pluralização das tabelas (Inglês)
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

			// Desabilitando a exclusão em cascata em relacionamentos 1:N evitando ter registros filhos sem registros pai
			modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

			// Desabilitando a exclusão em cascata em relacionamentosN:N
			modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

			// Configura para os Id's serem gerados automaticamente
			modelBuilder.Conventions.Add<StoreGeneratedIdentityKeyConvention>();

			// Configura para que todas as propriedade do tipo string na entidade POCO seja configurado como VARCHAR no SQL Server 			
			modelBuilder.Properties<string>()
				.Configure(p => p.HasColumnType("nvarchar"));

			// Configura para que caso não sejam definidos os campos string terão tamanho máximo de 100 caracteres
			modelBuilder.Properties<string>()
				.Configure(x => x.HasMaxLength(100));

			// Configurando precisão de tipos Single para 3 casas decimais
			modelBuilder.Properties<decimal>()
				.Configure(x => x.HasPrecision(28, 3));

			// Configurando para que todas as Propriedades Id sejam Identity
			modelBuilder.Properties<long>()
						.Where(x => x.Name.Equals("Id"))
						.Configure(p => p.HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity));

			// Configurando DateTime para DateTime2 com precisão de 2 no Sql Server
			modelBuilder.Properties<DateTime>()
						.Configure(p => p
							.HasColumnType("datetime2")
							.HasPrecision(2)
						);

			modelBuilder.Configurations.Add(new AutorMap());
			modelBuilder.Configurations.Add(new AluguelMap());
			modelBuilder.Configurations.Add(new ClienteMap());
			modelBuilder.Configurations.Add(new ContatoMap());
			modelBuilder.Configurations.Add(new EditoraMap());
			modelBuilder.Configurations.Add(new EnderecoMap());
			modelBuilder.Configurations.Add(new EstadoMap());
			modelBuilder.Configurations.Add(new EstoqueMap());
			modelBuilder.Configurations.Add(new GeneroMap());
			modelBuilder.Configurations.Add(new LivroMap());
			modelBuilder.Configurations.Add(new LogErroMap());
			modelBuilder.Configurations.Add(new LogAlteracaoMap());
			modelBuilder.Configurations.Add(new MultaMap());
			modelBuilder.Configurations.Add(new PerfilMap());
			modelBuilder.Configurations.Add(new TituloMap());
			modelBuilder.Configurations.Add(new UnidadeMap());
			modelBuilder.Configurations.Add(new UsuarioMap());
		}
	}
}