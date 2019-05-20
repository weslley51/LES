CREATE DATABASE LES;
GO

USE [LES]
GO
CREATE TABLE [dbo].[Alugueis](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[DataDevolucao] [datetime2](2) NULL,
	[DataPrevistaDevolucao] [datetime2](2) NOT NULL,
	[DataCadastro] [datetime2](2) NOT NULL,
	[Ativo] [bit] NOT NULL,
	[ClienteId] [bigint] NOT NULL,
	[LivroId] [bigint] NOT NULL,
	[MultaId] [bigint] NULL,
	[UnidadeId] [bigint] NOT NULL,
	[UsuarioCadastroId] [bigint] NOT NULL,
 CONSTRAINT [PK_Alugueis] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE TABLE [dbo].[Autores](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](100) NOT NULL,
	[DataCadastro] [datetime2](2) NOT NULL,
	[Ativo] [bit] NOT NULL,
	[UsuarioCadastroId] [bigint] NOT NULL,
 CONSTRAINT [PK_Autores] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE TABLE [dbo].[AutoresTitulo](
	[TituloId] [bigint] NOT NULL,
	[AutorId] [bigint] NOT NULL,
 CONSTRAINT [PK_AutoresTitulo] PRIMARY KEY CLUSTERED 
(
	[TituloId] ASC,
	[AutorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE TABLE [dbo].[Clientes](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[NomeCompleto] [nvarchar](300) NOT NULL,
	[RG] [nvarchar](9) NOT NULL,
	[CPF] [nvarchar](11) NOT NULL,
	[Saldo] [decimal](28, 3) NULL,
	[Sexo] [int] NOT NULL,
	[DataCadastro] [datetime2](2) NOT NULL,
	[Ativo] [bit] NOT NULL,
	[EnderecoId] [bigint] NOT NULL,
	[UsuarioCadastroId] [bigint] NOT NULL,
 CONSTRAINT [PK_Clientes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE TABLE [dbo].[Contatos](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Email] [bit] NOT NULL,
	[Valor] [nvarchar](250) NOT NULL,
	[Observacoes] [nvarchar](100) NULL,
	[DataCadastro] [datetime2](2) NOT NULL,
	[Ativo] [bit] NOT NULL,
	[UsuarioCadastroId] [bigint] NOT NULL,
 CONSTRAINT [PK_Contatos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE TABLE [dbo].[ContatosCliente](
	[ClienteId] [bigint] NOT NULL,
	[ContatoId] [bigint] NOT NULL,
 CONSTRAINT [PK_ContatosCliente] PRIMARY KEY CLUSTERED 
(
	[ClienteId] ASC,
	[ContatoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE TABLE [dbo].[ContatosUnidade](
	[UnidadeId] [bigint] NOT NULL,
	[ContatoId] [bigint] NOT NULL,
 CONSTRAINT [PK_ContatosUnidade] PRIMARY KEY CLUSTERED 
(
	[UnidadeId] ASC,
	[ContatoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE TABLE [dbo].[Editoras](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](100) NOT NULL,
	[DataCadastro] [datetime2](2) NOT NULL,
	[Ativo] [bit] NOT NULL,
	[UsuarioCadastroId] [bigint] NOT NULL,
 CONSTRAINT [PK_Editoras] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE TABLE [dbo].[Enderecos](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Cep] [nvarchar](8) NOT NULL,
	[Logradouro] [nvarchar](150) NOT NULL,
	[Numero] [nvarchar](10) NOT NULL,
	[Bairro] [nvarchar](100) NOT NULL,
	[Municipio] [nvarchar](70) NOT NULL,
	[Complemento] [nvarchar](50) NULL,
	[Observacao] [nvarchar](250) NULL,
	[DataCadastro] [datetime2](2) NOT NULL,
	[Ativo] [bit] NOT NULL,
	[EstadoId] [bigint] NOT NULL,
	[UsuarioCadastroId] [bigint] NOT NULL,
 CONSTRAINT [PK_Enderecos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE TABLE [dbo].[Estados](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[UF] [nvarchar](2) NOT NULL,
	[Nome] [nvarchar](50) NOT NULL,
	[DataCadastro] [datetime2](2) NOT NULL,
	[Ativo] [bit] NOT NULL,
	[UsuarioCadastroId] [bigint] NOT NULL,
 CONSTRAINT [PK_Estados] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE TABLE [dbo].[Estoque](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Descricao] [nvarchar](500) NULL,
	[TipoMovimentacao] [int] NOT NULL,
	[DataCadastro] [datetime2](2) NOT NULL,
	[Ativo] [bit] NOT NULL,
	[TituloId] [bigint] NOT NULL,
	[UnidadeId] [bigint] NOT NULL,
	[UsuarioCadastroId] [bigint] NOT NULL,
	[Quantidade] [int] NOT NULL,
 CONSTRAINT [PK_Estoque] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE TABLE [dbo].[Generos](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](100) NOT NULL,
	[DataCadastro] [datetime2](2) NOT NULL,
	[Ativo] [bit] NOT NULL,
	[UsuarioCadastroId] [bigint] NOT NULL,
 CONSTRAINT [PK_Generos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE TABLE [dbo].[GenerosTitulo](
	[TituloId] [bigint] NOT NULL,
	[GeneroId] [bigint] NOT NULL,
 CONSTRAINT [PK_GenerosTitulo] PRIMARY KEY CLUSTERED 
(
	[TituloId] ASC,
	[GeneroId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE TABLE [dbo].[Livros](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[CodigoBiblioteca] [nvarchar](100) NULL,
	[Observacoes] [nvarchar](300) NULL,
	[DataCadastro] [datetime2](2) NOT NULL,
	[Ativo] [bit] NOT NULL,
	[UsuarioCadastroId] [bigint] NOT NULL,
	[TituloId] [bigint] NOT NULL,
	[UnidadeId] [bigint] NULL,
 CONSTRAINT [PK_Livros] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE TABLE [dbo].[LogsAlteracao](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[NomeEntidade] [nvarchar](100) NOT NULL,
	[ObjetoAlterado] [nvarchar](max) NOT NULL,
	[DataCadastro] [datetime2](2) NOT NULL,
	[UsuarioCadastroId] [bigint] NOT NULL,
	[ObjetoPrincipal] [nvarchar](max) NULL,
	[EntidadeId] [bigint] NULL,
 CONSTRAINT [PK_LogsAlteracao] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
CREATE TABLE [dbo].[LogsErro](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[MensagemPrincipal] [nvarchar](200) NOT NULL,
	[OutrasMensagens] [nvarchar](2000) NOT NULL,
	[DataCadastro] [datetime2](2) NOT NULL,
	[UsuarioCadastroId] [bigint] NOT NULL,
 CONSTRAINT [PK_LogsErro] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE TABLE [dbo].[Multas](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Valor] [decimal](28, 3) NOT NULL,
	[DataCadastro] [datetime2](2) NOT NULL,
	[Ativo] [bit] NOT NULL,
	[UsuarioCadastroId] [bigint] NOT NULL,
	[ClienteId] [bigint] NOT NULL,
 CONSTRAINT [PK_Multas] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE TABLE [dbo].[Perfis](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](100) NOT NULL,
	[DataCadastro] [datetime2](2) NOT NULL,
	[Ativo] [bit] NOT NULL,
	[UsuarioCadastroId] [bigint] NOT NULL,
 CONSTRAINT [PK_Perfis] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE TABLE [dbo].[Titulos](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[PrazoDevolucao] [smallint] NOT NULL,
	[AnoPublicacao] [smallint] NOT NULL,
	[QuantiaFolhas] [smallint] NOT NULL,
	[Nome] [nvarchar](300) NOT NULL,
	[Descricao] [nvarchar](200) NULL,
	[Sinopse] [nvarchar](500) NULL,
	[DataCadastro] [datetime2](2) NOT NULL,
	[Ativo] [bit] NOT NULL,
	[EditoraId] [bigint] NOT NULL,
	[UsuarioCadastroId] [bigint] NOT NULL,
 CONSTRAINT [PK_Titulos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE TABLE [dbo].[Unidades](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](100) NOT NULL,
	[DataCadastro] [datetime2](2) NOT NULL,
	[Ativo] [bit] NOT NULL,
	[EnderecoId] [bigint] NOT NULL,
	[UsuarioCadastroId] [bigint] NOT NULL,
 CONSTRAINT [PK_Unidades] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE TABLE [dbo].[Usuarios](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](150) NOT NULL,
	[Login] [nvarchar](20) NOT NULL,
	[Nome] [nvarchar](200) NOT NULL,
	[Senha] [nvarchar](32) NOT NULL,
	[DataCadastro] [datetime2](2) NOT NULL,
	[Ativo] [bit] NOT NULL,
	[PerfilId] [bigint] NULL,
	[UsuarioCadastroId] [bigint] NOT NULL,
	[UnidadeId] [bigint] NULL,
 CONSTRAINT [PK_Usuarios] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Alugueis] ADD  DEFAULT (getdate()) FOR [DataCadastro]
GO
ALTER TABLE [dbo].[Alugueis] ADD  DEFAULT ((1)) FOR [Ativo]
GO
ALTER TABLE [dbo].[Autores] ADD  DEFAULT (getdate()) FOR [DataCadastro]
GO
ALTER TABLE [dbo].[Autores] ADD  DEFAULT ((1)) FOR [Ativo]
GO
ALTER TABLE [dbo].[Clientes] ADD  CONSTRAINT [DF_Saldo]  DEFAULT ((0)) FOR [Saldo]
GO
ALTER TABLE [dbo].[Clientes] ADD  DEFAULT (getdate()) FOR [DataCadastro]
GO
ALTER TABLE [dbo].[Clientes] ADD  DEFAULT ((1)) FOR [Ativo]
GO
ALTER TABLE [dbo].[Contatos] ADD  DEFAULT (getdate()) FOR [DataCadastro]
GO
ALTER TABLE [dbo].[Contatos] ADD  DEFAULT ((1)) FOR [Ativo]
GO
ALTER TABLE [dbo].[Editoras] ADD  DEFAULT (getdate()) FOR [DataCadastro]
GO
ALTER TABLE [dbo].[Editoras] ADD  DEFAULT ((1)) FOR [Ativo]
GO
ALTER TABLE [dbo].[Enderecos] ADD  DEFAULT (getdate()) FOR [DataCadastro]
GO
ALTER TABLE [dbo].[Enderecos] ADD  DEFAULT ((1)) FOR [Ativo]
GO
ALTER TABLE [dbo].[Estados] ADD  DEFAULT (getdate()) FOR [DataCadastro]
GO
ALTER TABLE [dbo].[Estados] ADD  DEFAULT ((1)) FOR [Ativo]
GO
ALTER TABLE [dbo].[Estoque] ADD  DEFAULT (getdate()) FOR [DataCadastro]
GO
ALTER TABLE [dbo].[Estoque] ADD  DEFAULT ((1)) FOR [Ativo]
GO
ALTER TABLE [dbo].[Generos] ADD  DEFAULT (getdate()) FOR [DataCadastro]
GO
ALTER TABLE [dbo].[Generos] ADD  DEFAULT ((1)) FOR [Ativo]
GO
ALTER TABLE [dbo].[Livros] ADD  DEFAULT (getdate()) FOR [DataCadastro]
GO
ALTER TABLE [dbo].[Livros] ADD  DEFAULT ((1)) FOR [Ativo]
GO
ALTER TABLE [dbo].[LogsAlteracao] ADD  DEFAULT (getdate()) FOR [DataCadastro]
GO
ALTER TABLE [dbo].[LogsErro] ADD  DEFAULT (getdate()) FOR [DataCadastro]
GO
ALTER TABLE [dbo].[Multas] ADD  DEFAULT (getdate()) FOR [DataCadastro]
GO
ALTER TABLE [dbo].[Multas] ADD  DEFAULT ((1)) FOR [Ativo]
GO
ALTER TABLE [dbo].[Perfis] ADD  DEFAULT (getdate()) FOR [DataCadastro]
GO
ALTER TABLE [dbo].[Perfis] ADD  DEFAULT ((1)) FOR [Ativo]
GO
ALTER TABLE [dbo].[Titulos] ADD  DEFAULT (getdate()) FOR [DataCadastro]
GO
ALTER TABLE [dbo].[Titulos] ADD  DEFAULT ((1)) FOR [Ativo]
GO
ALTER TABLE [dbo].[Unidades] ADD  DEFAULT (getdate()) FOR [DataCadastro]
GO
ALTER TABLE [dbo].[Unidades] ADD  DEFAULT ((1)) FOR [Ativo]
GO
ALTER TABLE [dbo].[Usuarios] ADD  DEFAULT (getdate()) FOR [DataCadastro]
GO
ALTER TABLE [dbo].[Usuarios] ADD  DEFAULT ((1)) FOR [Ativo]
GO
ALTER TABLE [dbo].[Alugueis]  WITH CHECK ADD  CONSTRAINT [FK_Alugueis_Clientes_ClienteId] FOREIGN KEY([ClienteId])
REFERENCES [dbo].[Clientes] ([Id])
GO
ALTER TABLE [dbo].[Alugueis] CHECK CONSTRAINT [FK_Alugueis_Clientes_ClienteId]
GO
ALTER TABLE [dbo].[Alugueis]  WITH CHECK ADD  CONSTRAINT [FK_Alugueis_Livros_LivroId] FOREIGN KEY([LivroId])
REFERENCES [dbo].[Livros] ([Id])
GO
ALTER TABLE [dbo].[Alugueis] CHECK CONSTRAINT [FK_Alugueis_Livros_LivroId]
GO
ALTER TABLE [dbo].[Alugueis]  WITH CHECK ADD  CONSTRAINT [FK_Alugueis_Multas_MultaId] FOREIGN KEY([MultaId])
REFERENCES [dbo].[Multas] ([Id])
GO
ALTER TABLE [dbo].[Alugueis] CHECK CONSTRAINT [FK_Alugueis_Multas_MultaId]
GO
ALTER TABLE [dbo].[Alugueis]  WITH CHECK ADD  CONSTRAINT [FK_Alugueis_Unidades_UnidadeId] FOREIGN KEY([UnidadeId])
REFERENCES [dbo].[Unidades] ([Id])
GO
ALTER TABLE [dbo].[Alugueis] CHECK CONSTRAINT [FK_Alugueis_Unidades_UnidadeId]
GO
ALTER TABLE [dbo].[Alugueis]  WITH CHECK ADD  CONSTRAINT [FK_Alugueis_Usuarios_UsuarioCadastroId] FOREIGN KEY([UsuarioCadastroId])
REFERENCES [dbo].[Usuarios] ([Id])
GO
ALTER TABLE [dbo].[Alugueis] CHECK CONSTRAINT [FK_Alugueis_Usuarios_UsuarioCadastroId]
GO
ALTER TABLE [dbo].[Autores]  WITH CHECK ADD  CONSTRAINT [FK_Autores_Usuarios_UsuarioCadastroId] FOREIGN KEY([UsuarioCadastroId])
REFERENCES [dbo].[Usuarios] ([Id])
GO
ALTER TABLE [dbo].[Autores] CHECK CONSTRAINT [FK_Autores_Usuarios_UsuarioCadastroId]
GO
ALTER TABLE [dbo].[AutoresTitulo]  WITH CHECK ADD  CONSTRAINT [FK_AutoresTitulo_Autores_AutorId] FOREIGN KEY([AutorId])
REFERENCES [dbo].[Autores] ([Id])
GO
ALTER TABLE [dbo].[AutoresTitulo] CHECK CONSTRAINT [FK_AutoresTitulo_Autores_AutorId]
GO
ALTER TABLE [dbo].[AutoresTitulo]  WITH CHECK ADD  CONSTRAINT [FK_AutoresTitulo_Titulos_TituloId] FOREIGN KEY([TituloId])
REFERENCES [dbo].[Titulos] ([Id])
GO
ALTER TABLE [dbo].[AutoresTitulo] CHECK CONSTRAINT [FK_AutoresTitulo_Titulos_TituloId]
GO
ALTER TABLE [dbo].[Clientes]  WITH CHECK ADD  CONSTRAINT [FK_Clientes_Enderecos_EnderecoId] FOREIGN KEY([EnderecoId])
REFERENCES [dbo].[Enderecos] ([Id])
GO
ALTER TABLE [dbo].[Clientes] CHECK CONSTRAINT [FK_Clientes_Enderecos_EnderecoId]
GO
ALTER TABLE [dbo].[Clientes]  WITH CHECK ADD  CONSTRAINT [FK_Clientes_Usuarios_UsuarioCadastroId] FOREIGN KEY([UsuarioCadastroId])
REFERENCES [dbo].[Usuarios] ([Id])
GO
ALTER TABLE [dbo].[Clientes] CHECK CONSTRAINT [FK_Clientes_Usuarios_UsuarioCadastroId]
GO
ALTER TABLE [dbo].[Contatos]  WITH CHECK ADD  CONSTRAINT [FK_Contatos_Usuarios_UsuarioCadastroId] FOREIGN KEY([UsuarioCadastroId])
REFERENCES [dbo].[Usuarios] ([Id])
GO
ALTER TABLE [dbo].[Contatos] CHECK CONSTRAINT [FK_Contatos_Usuarios_UsuarioCadastroId]
GO
ALTER TABLE [dbo].[ContatosCliente]  WITH CHECK ADD  CONSTRAINT [FK_ContatosCliente_Clientes_ClienteId] FOREIGN KEY([ClienteId])
REFERENCES [dbo].[Clientes] ([Id])
GO
ALTER TABLE [dbo].[ContatosCliente] CHECK CONSTRAINT [FK_ContatosCliente_Clientes_ClienteId]
GO
ALTER TABLE [dbo].[ContatosCliente]  WITH CHECK ADD  CONSTRAINT [FK_ContatosCliente_Contatos_ContatoId] FOREIGN KEY([ContatoId])
REFERENCES [dbo].[Contatos] ([Id])
GO
ALTER TABLE [dbo].[ContatosCliente] CHECK CONSTRAINT [FK_ContatosCliente_Contatos_ContatoId]
GO
ALTER TABLE [dbo].[ContatosUnidade]  WITH CHECK ADD  CONSTRAINT [FK_ContatosUnidade_Contatos_ContatoId] FOREIGN KEY([ContatoId])
REFERENCES [dbo].[Contatos] ([Id])
GO
ALTER TABLE [dbo].[ContatosUnidade] CHECK CONSTRAINT [FK_ContatosUnidade_Contatos_ContatoId]
GO
ALTER TABLE [dbo].[ContatosUnidade]  WITH CHECK ADD  CONSTRAINT [FK_ContatosUnidade_Unidades_UnidadeId] FOREIGN KEY([UnidadeId])
REFERENCES [dbo].[Unidades] ([Id])
GO
ALTER TABLE [dbo].[ContatosUnidade] CHECK CONSTRAINT [FK_ContatosUnidade_Unidades_UnidadeId]
GO
ALTER TABLE [dbo].[Editoras]  WITH CHECK ADD  CONSTRAINT [FK_Editoras_Usuarios_UsuarioCadastroId] FOREIGN KEY([UsuarioCadastroId])
REFERENCES [dbo].[Usuarios] ([Id])
GO
ALTER TABLE [dbo].[Editoras] CHECK CONSTRAINT [FK_Editoras_Usuarios_UsuarioCadastroId]
GO
ALTER TABLE [dbo].[Enderecos]  WITH CHECK ADD  CONSTRAINT [FK_Enderecos_Estados_EstadoId] FOREIGN KEY([EstadoId])
REFERENCES [dbo].[Estados] ([Id])
GO
ALTER TABLE [dbo].[Enderecos] CHECK CONSTRAINT [FK_Enderecos_Estados_EstadoId]
GO
ALTER TABLE [dbo].[Enderecos]  WITH CHECK ADD  CONSTRAINT [FK_Enderecos_Usuarios_UsuarioCadastroId] FOREIGN KEY([UsuarioCadastroId])
REFERENCES [dbo].[Usuarios] ([Id])
GO
ALTER TABLE [dbo].[Enderecos] CHECK CONSTRAINT [FK_Enderecos_Usuarios_UsuarioCadastroId]
GO
ALTER TABLE [dbo].[Estados]  WITH CHECK ADD  CONSTRAINT [FK_Estados_Usuarios_UsuarioCadastroId] FOREIGN KEY([UsuarioCadastroId])
REFERENCES [dbo].[Usuarios] ([Id])
GO
ALTER TABLE [dbo].[Estados] CHECK CONSTRAINT [FK_Estados_Usuarios_UsuarioCadastroId]
GO
ALTER TABLE [dbo].[Estoque]  WITH CHECK ADD  CONSTRAINT [FK_Estoque_Titulos_TituloId] FOREIGN KEY([TituloId])
REFERENCES [dbo].[Titulos] ([Id])
GO
ALTER TABLE [dbo].[Estoque] CHECK CONSTRAINT [FK_Estoque_Titulos_TituloId]
GO
ALTER TABLE [dbo].[Estoque]  WITH CHECK ADD  CONSTRAINT [FK_Estoque_Unidades_UnidadeId] FOREIGN KEY([UnidadeId])
REFERENCES [dbo].[Unidades] ([Id])
GO
ALTER TABLE [dbo].[Estoque] CHECK CONSTRAINT [FK_Estoque_Unidades_UnidadeId]
GO
ALTER TABLE [dbo].[Estoque]  WITH CHECK ADD  CONSTRAINT [FK_Estoque_Usuarios_UsuarioCadastroId] FOREIGN KEY([UsuarioCadastroId])
REFERENCES [dbo].[Usuarios] ([Id])
GO
ALTER TABLE [dbo].[Estoque] CHECK CONSTRAINT [FK_Estoque_Usuarios_UsuarioCadastroId]
GO
ALTER TABLE [dbo].[Generos]  WITH CHECK ADD  CONSTRAINT [FK_Generos_Usuarios_UsuarioCadastroId] FOREIGN KEY([UsuarioCadastroId])
REFERENCES [dbo].[Usuarios] ([Id])
GO
ALTER TABLE [dbo].[Generos] CHECK CONSTRAINT [FK_Generos_Usuarios_UsuarioCadastroId]
GO
ALTER TABLE [dbo].[GenerosTitulo]  WITH CHECK ADD  CONSTRAINT [FK_GenerosTitulo_Generos_GeneroId] FOREIGN KEY([GeneroId])
REFERENCES [dbo].[Generos] ([Id])
GO
ALTER TABLE [dbo].[GenerosTitulo] CHECK CONSTRAINT [FK_GenerosTitulo_Generos_GeneroId]
GO
ALTER TABLE [dbo].[GenerosTitulo]  WITH CHECK ADD  CONSTRAINT [FK_GenerosTitulo_Titulos_TituloId] FOREIGN KEY([TituloId])
REFERENCES [dbo].[Titulos] ([Id])
GO
ALTER TABLE [dbo].[GenerosTitulo] CHECK CONSTRAINT [FK_GenerosTitulo_Titulos_TituloId]
GO
ALTER TABLE [dbo].[Livros]  WITH CHECK ADD  CONSTRAINT [FK_Livros_Titulos_TituloId] FOREIGN KEY([TituloId])
REFERENCES [dbo].[Titulos] ([Id])
GO
ALTER TABLE [dbo].[Livros] CHECK CONSTRAINT [FK_Livros_Titulos_TituloId]
GO
ALTER TABLE [dbo].[Livros]  WITH CHECK ADD  CONSTRAINT [FK_Livros_Unidades_UnidadeId] FOREIGN KEY([UnidadeId])
REFERENCES [dbo].[Unidades] ([Id])
GO
ALTER TABLE [dbo].[Livros] CHECK CONSTRAINT [FK_Livros_Unidades_UnidadeId]
GO
ALTER TABLE [dbo].[Livros]  WITH CHECK ADD  CONSTRAINT [FK_Livros_Usuarios_UsuarioCadastroId] FOREIGN KEY([UsuarioCadastroId])
REFERENCES [dbo].[Usuarios] ([Id])
GO
ALTER TABLE [dbo].[Livros] CHECK CONSTRAINT [FK_Livros_Usuarios_UsuarioCadastroId]
GO
ALTER TABLE [dbo].[LogsAlteracao]  WITH CHECK ADD  CONSTRAINT [FK_LogsAlteracao_Usuarios_UsuarioCadastroId] FOREIGN KEY([UsuarioCadastroId])
REFERENCES [dbo].[Usuarios] ([Id])
GO
ALTER TABLE [dbo].[LogsAlteracao] CHECK CONSTRAINT [FK_LogsAlteracao_Usuarios_UsuarioCadastroId]
GO
ALTER TABLE [dbo].[LogsErro]  WITH CHECK ADD  CONSTRAINT [FK_LogsErro_Usuarios_UsuarioCadastroId] FOREIGN KEY([UsuarioCadastroId])
REFERENCES [dbo].[Usuarios] ([Id])
GO
ALTER TABLE [dbo].[LogsErro] CHECK CONSTRAINT [FK_LogsErro_Usuarios_UsuarioCadastroId]
GO
ALTER TABLE [dbo].[Multas]  WITH CHECK ADD  CONSTRAINT [FK_Multas_Clientes_ClienteId] FOREIGN KEY([ClienteId])
REFERENCES [dbo].[Clientes] ([Id])
GO
ALTER TABLE [dbo].[Multas] CHECK CONSTRAINT [FK_Multas_Clientes_ClienteId]
GO
ALTER TABLE [dbo].[Multas]  WITH CHECK ADD  CONSTRAINT [FK_Multas_Usuarios_UsuarioCadastroId] FOREIGN KEY([UsuarioCadastroId])
REFERENCES [dbo].[Usuarios] ([Id])
GO
ALTER TABLE [dbo].[Multas] CHECK CONSTRAINT [FK_Multas_Usuarios_UsuarioCadastroId]
GO
ALTER TABLE [dbo].[Perfis]  WITH CHECK ADD  CONSTRAINT [FK_Perfis_Usuarios_UsuarioCadastroId] FOREIGN KEY([UsuarioCadastroId])
REFERENCES [dbo].[Usuarios] ([Id])
GO
ALTER TABLE [dbo].[Perfis] CHECK CONSTRAINT [FK_Perfis_Usuarios_UsuarioCadastroId]
GO
ALTER TABLE [dbo].[Titulos]  WITH CHECK ADD  CONSTRAINT [FK_Titulos_Editoras_EditoraId] FOREIGN KEY([EditoraId])
REFERENCES [dbo].[Editoras] ([Id])
GO
ALTER TABLE [dbo].[Titulos] CHECK CONSTRAINT [FK_Titulos_Editoras_EditoraId]
GO
ALTER TABLE [dbo].[Titulos]  WITH CHECK ADD  CONSTRAINT [FK_Titulos_Usuarios_UsuarioCadastroId] FOREIGN KEY([UsuarioCadastroId])
REFERENCES [dbo].[Usuarios] ([Id])
GO
ALTER TABLE [dbo].[Titulos] CHECK CONSTRAINT [FK_Titulos_Usuarios_UsuarioCadastroId]
GO
ALTER TABLE [dbo].[Unidades]  WITH CHECK ADD  CONSTRAINT [FK_Unidades_Enderecos_EnderecoId] FOREIGN KEY([EnderecoId])
REFERENCES [dbo].[Enderecos] ([Id])
GO
ALTER TABLE [dbo].[Unidades] CHECK CONSTRAINT [FK_Unidades_Enderecos_EnderecoId]
GO
ALTER TABLE [dbo].[Unidades]  WITH CHECK ADD  CONSTRAINT [FK_Unidades_Usuarios_UsuarioCadastroId] FOREIGN KEY([UsuarioCadastroId])
REFERENCES [dbo].[Usuarios] ([Id])
GO
ALTER TABLE [dbo].[Unidades] CHECK CONSTRAINT [FK_Unidades_Usuarios_UsuarioCadastroId]
GO
ALTER TABLE [dbo].[Usuarios]  WITH CHECK ADD  CONSTRAINT [FK_Usuarios_Perfis_PerfilId] FOREIGN KEY([PerfilId])
REFERENCES [dbo].[Perfis] ([Id])
GO
ALTER TABLE [dbo].[Usuarios] CHECK CONSTRAINT [FK_Usuarios_Perfis_PerfilId]
GO
ALTER TABLE [dbo].[Usuarios]  WITH CHECK ADD  CONSTRAINT [FK_Usuarios_Unidades_UnidadeId] FOREIGN KEY([UnidadeId])
REFERENCES [dbo].[Unidades] ([Id])
GO
ALTER TABLE [dbo].[Usuarios] CHECK CONSTRAINT [FK_Usuarios_Unidades_UnidadeId]
GO

ALTER TABLE [dbo].[Usuarios] DROP CONSTRAINT IF EXISTS [FK_Usuarios_Usuarios_UsuarioCadastroId]

INSERT INTO [dbo].[Usuarios]
(
	[Email]
  , [Login]
  , [Nome]
  , [Senha]
  , [PerfilId]
  , [UsuarioCadastroId]
)
VALUES
(
	'adm@les.com.br'
  , 'admin'
  , 'Administrador Geral'
  , '4297f44b13955235245b2497399d7a93'  
  , NULL
  , 1
)

ALTER TABLE [dbo].[Usuarios] ADD CONSTRAINT [FK_Usuarios_Usuarios_UsuarioCadastroId] FOREIGN KEY ([UsuarioCadastroId]) REFERENCES [dbo].[Usuarios] ([Id])

ALTER TABLE [dbo].[Usuarios]  WITH CHECK ADD  CONSTRAINT [FK_Usuarios_Usuarios_UsuarioCadastroId] FOREIGN KEY([UsuarioCadastroId])
REFERENCES [dbo].[Usuarios] ([Id])
GO
ALTER TABLE [dbo].[Usuarios] CHECK CONSTRAINT [FK_Usuarios_Usuarios_UsuarioCadastroId]
GO

INSERT INTO [dbo].[Perfis]
(
	[Nome]
  , [UsuarioCadastroId]
)
VALUES
	('Administrador', 1),
	('Bibliotecário', 1)

UPDATE [dbo].[Usuarios] SET [PerfilId] = 1

INSERT INTO [dbo].[Estados]
(
	[UF]
  , [Nome]
  , [UsuarioCadastroId]
)
VALUES
	('AC','Acre', 1) 
  , ('AL','Alagoas', 1)
  , ('AM','Amazonas', 1)
  , ('AP','Amapá', 1)
  , ('BA','Bahia', 1)
  , ('CE','Ceará', 1)
  , ('DF','Distrito Federal', 1)
  , ('ES','Espírito Santo', 1)
  , ('GO','Goiás', 1)
  , ('MA','Maranhão', 1)
  , ('MG','Minas Gerais', 1)
  , ('MS','Mato Grosso do Sul', 1)
  , ('MT','Mato Grosso', 1)
  , ('PA','Pará', 1)
  , ('PB','Paraíba', 1)
  , ('PE','Pernambuco', 1)
  , ('PI','Piauí', 1)
  , ('PR','Paraná', 1)
  , ('RJ','Rio de Janeiro', 1)
  , ('RN','Rio Grande do Norte', 1)
  , ('RO','Rondônia', 1)
  , ('RR','Roraima', 1)
  , ('RS','Rio Grande do Sul', 1)
  , ('SC','Santa Catarina', 1)
  , ('SE','Sergipe', 1)
  , ('SP','São Paulo', 1)
  , ('TO','Tocantins', 1)