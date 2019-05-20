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

namespace LES.Strategies.Generos
{
	[CommandType(Command.Insert, Command.Update)]
	public class ValidarGenero : PreStrategy<Genero>
	{
		public ValidarGenero(DbContext Context) : base(Context)
		{
		}

		public override List<Message> Process(Genero Entity)
		{
			var Mensagens = new List<Message>();

			if (Entity == null)
				Mensagens.Add(new Message("Informações do gênero não foram preenchidas!"));
			else
			{
				Entity.TrimAllStrings();

				if (string.IsNullOrWhiteSpace(Entity.Nome) || Entity.Nome.Length < 3)
					Mensagens.Add(new Message("Nome do gênero não preenchido ou inválido !"));
			}

			return Mensagens;
		}
	}
}