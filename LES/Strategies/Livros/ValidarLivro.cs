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

namespace LES.Strategies.Livros
{
	[CommandType(Command.Insert, Command.Update)]
	public class ValidarLivro : PreStrategy<Livro>
	{
		public ValidarLivro(DbContext Context) : base(Context)
		{
		}

		public override List<Message> Process(Livro Entity)
		{
			var Mensagens = new List<Message>();

			if (Entity == null)
				Mensagens.Add(new Message("Informações do livro não foram preenchidas!"));
			else
			{
				Entity.TrimAllStrings();

				if (string.IsNullOrWhiteSpace(Entity.CodigoBiblioteca))
					Entity.Ativo = false;
			}

			return Mensagens;
		}
	}
}