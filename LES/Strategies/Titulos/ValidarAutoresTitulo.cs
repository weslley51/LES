using LES.Models;
using LES.Strategies.Autores;
using LES.Strategies.Dominios;
using LES.Structure;
using LES.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LES.Strategies.Titulos
{
	[CommandType(Command.Insert, Command.Update)]
	public class ValidarAutoresTitulo : PreStrategy<Titulo>
	{
		public ValidarAutoresTitulo(DbContext Context) : base(Context)
		{
		}

		public override List<Message> Process(Titulo Entity)
		{
			var Mensagens = new List<Message>();

			if (Entity?.Autores == null)
				Mensagens.Add(new Message("Não foram informados os autores do livro!"));
			else
			{
				if (Entity.Autores.Any(x => x.Id == 0))
					Mensagens.Add(new Message("Um ou mais autores associados ao Titulo não foram localizados!"));
				else
				{
					var RepositorioAutor = new Repository<Autor>(Context);
					Entity.Autores = Entity.Autores.Select(x => RepositorioAutor.Get(new Filter { Id = x.Id }).FirstOrDefault()).ToList();

					if (Entity.Autores.Any(x => x == null))
						Mensagens.Add(new Message("Um ou mais autores associados ao Titulo não foram localizados!"));
				}
			}

			return Mensagens;
		}
	}
}