using LES.Models;
using LES.Strategies.Titulos;
using LES.Strategies.Dominios;
using LES.Structure;
using LES.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LES.Strategies.Estoques
{
	[CommandType(Command.Insert, Command.Update)]
	public class ValidarTituloEstoque : PreStrategy<Estoque>
	{
		public ValidarTituloEstoque(DbContext Context) : base(Context)
		{
		}

		public override List<Message> Process(Estoque Entity)
		{
			var Mensagens = new List<Message>();

			if (Entity?.Titulo == null || Entity?.Titulo.Id == 0)
				Mensagens.Add(new Message("Não foi informado o Titulo do livro!"));
			else
			{
				Entity.Titulo = Context.Set<Titulo>().Find(Entity.Titulo.Id);

				if (Entity.Titulo == null)
					Mensagens.Add(new Message("O Titulo associado ao estoque não foi localizado!"));
			}
			return Mensagens;
		}
	}
}