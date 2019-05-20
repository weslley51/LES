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

namespace LES.Strategies.Contatos
{
	[CommandType(Command.Insert, Command.Update)]
	public class ValidarContato : PreStrategy<Contato>
	{
		public ValidarContato(DbContext Context) : base(Context)
		{
		}

		public override List<Message> Process(Contato Entity)
		{
			var Mensagens = new List<Message>();

			if (Entity == null)
				Mensagens.Add(new Message("Informações do contato não foram preenchidas!"));
			else
			{
				Entity.TrimAllStrings();

				if (!Entity.Email.HasValue)
					Mensagens.Add(new Message("Tipo de contato não preenchido !"));
				else
				{
					if (string.IsNullOrWhiteSpace(Entity.Valor))
						Mensagens.Add(new Message("Contato não preenchido !"));
					else
					{
						if (Entity.Email.Value)
						{
							if (Entity.Valor.Length < 5)
								Mensagens.Add(new Message("Email preenchido é inválido !"));
						}
						else
						{
							if (Entity.Valor.Length < 11 || Entity.Valor.Length > 12)
								Mensagens.Add(new Message("Telefone preenchido é inválido !"));
						}
					}
				}
			}
			return Mensagens;
		}
	}
}