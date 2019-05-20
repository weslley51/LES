using LES.Structure;
using LES.Utils;
using LES.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LES.Strategies.Perfis
{
	[CommandType(Command.Insert, Command.Update)]
	public class ValidarPerfil : PreStrategy<Perfil>
	{
		public ValidarPerfil(DbContext Context) : base(Context)
		{
		}

		public override List<Message> Process(Perfil Entity)
		{
			var Mensagens =  new List<Message>();

			if (Entity == null)
				Mensagens.Add(new Message("Informações do perfil não foram preenchidas!"));
			else
			{
				Entity.TrimAllStrings();

				if (string.IsNullOrWhiteSpace(Entity.Nome) || Entity.Nome.Length < 3)
					Mensagens.Add(new Message("Nome de Perfil inválido !"));
			}

			return Mensagens;
		}
	}
}