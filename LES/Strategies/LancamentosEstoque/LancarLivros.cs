using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LES.Models;
using LES.Strategies.Enderecos;
using LES.Utils;
using LES.Structure;
using System.Data.Entity;
using LES.Utils;
using LES;

namespace LES.Strategies.LancamentosEstoque
{
	[CommandType(Command.Insert)]
	public class LancarLivros : PostStrategy<Estoque>
	{
		public LancarLivros(DbContext Context) : base(Context)
		{
		}

		public override List<Message> Process(Estoque Entity)
		{
			var Livros = new List<Livro>();
			var Mensagens = new List<Message>();

			for (int i = 0; i < Entity.Quantidade; i++)
				Livros.Add(new Livro
				{
					Ativo = false,
					Titulo = Entity.Titulo,
					DataCadastro = DateTime.Now,					
					UsuarioCadastro = Entity.UsuarioCadastro,
					Observacoes = $"Via Lançamento de estoque Nº{Entity.Id}"
				});

			Context.Set<Livro>().AddRange(Livros);
			Context.SaveChanges();

			return Mensagens;
		}
	}
}