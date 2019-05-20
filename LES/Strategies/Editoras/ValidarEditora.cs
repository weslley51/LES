using LES.Structure;
using LES.Utils;
using LES.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using LES.Strategies.Estados;
using System.Linq;
using LES;
using LES.Strategies.Dominios;
using LES.Models;

namespace LES.Strategies.Editoras
{
	[CommandType(Command.Insert, Command.Update)]
	public class ValidarEditora : PreStrategy<Editora>
	{
		public ValidarEditora(DbContext Context) : base(Context)
		{
		}

		public override List<Message> Process(Editora Entity)
		{
			var Mensagens = new List<Message>();

			if (Entity == null)
				Mensagens.Add(new Message("Informações da editora não foram preenchidas!"));
			else
			{
				Entity.TrimAllStrings();

				if (string.IsNullOrWhiteSpace(Entity.Nome) || Entity.Nome.Length < 5)
					Mensagens.Add(new Message("Nome da editora não preenchido ou inválido !"));
			}

			return Mensagens;
		}
	}
}