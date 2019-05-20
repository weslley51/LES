using LES.Structure;
using LES.Utils;
using LES.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using LES.Utils;
using System.Linq;
using System.Web;
using LES;

namespace LES.Strategies.Estados
{
	[CommandType(Command.Insert, Command.Update)]
	public class ValidarEstado : PreStrategy<Estado>
	{
		public ValidarEstado(DbContext Context) : base(Context)
		{
		}

		public override List<Message> Process(Estado Entity)
		{
			var Mensagens = new List<Message>();

			if (Entity == null)
				Mensagens.Add(new Message("Informações do estado não foram preenchidas!"));
			else
			{
				Entity.TrimAllStrings();

				if (Entity.Id != 0 && Repository.Get(new Filter { Id = Entity.Id })?.Count() > 0)
					return null;

				Mensagens.Add(new Message("Estado não preenchido ou informado inválido !"));
			}

			return Mensagens;
		}
	}
}