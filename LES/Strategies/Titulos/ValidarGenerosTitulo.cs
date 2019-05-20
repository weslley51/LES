using LES.Models;
using LES.Strategies.Generos;
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
	public class ValidarGenerosTitulo : PreStrategy<Titulo>
	{
		public ValidarGenerosTitulo(DbContext Context) : base(Context)
		{
		}

		public override List<Message> Process(Titulo Entity)
		{
			var Mensagens = new List<Message>();

			if (Entity?.Generos == null)
				Mensagens.Add(new Message("Não foram informados os gêneros do livro!"));
			else
			{
				if (Entity.Generos.Any(x => x.Id == 0))
					Mensagens.Add(new Message("Um ou mais gêneros associados ao Titulo não foram localizados!"));
				else
				{
					var RepositorioGenero = new Repository<Genero>(Context);
					Entity.Generos = Entity.Generos.Select(x => RepositorioGenero.Get(new Filter { Id = x.Id }).FirstOrDefault()).ToList();
				}

				if (Entity.Generos.Any(x => x == null))
					Mensagens.Add(new Message("Um ou mais gêneros associados ao Titulo não foram localizados!"));
			}

			return Mensagens;
		}
	}
}