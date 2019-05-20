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
using LES.Strategies.Contatos;
using LES.Strategies.Dominios;
using LES.Data.Repositories;

namespace LES.Strategies.Clientes
{
	[CommandType(Command.Insert, Command.Update)]
	public class ValidarInformacoesCliente : PreStrategy<Cliente>
	{
		public ValidarInformacoesCliente(DbContext Context) : base(Context)
		{
		}

		public override List<Message> Process(Cliente Entity)
		{
			var Mensagens = new List<Message>();

			if (Entity == null)
				Mensagens.Add(new Message("Informações do cliente não foram preenchidas!"));
			else
			{
				Entity.TrimAllStrings();

				if (string.IsNullOrWhiteSpace(Entity.NomeCompleto) || Entity.NomeCompleto.Length < 10)
					Mensagens.Add(new Message("Nome do Cliente não preenchido ou inválido !"));

				if (string.IsNullOrWhiteSpace(Entity.RG))
					Mensagens.Add(new Message("RG não preenchido !"));
				else
				{
					if (Entity.RG.Any(x => !char.IsNumber(x) && char.ToUpper(x) != 'X') || Entity.RG.Length != 9)
						Mensagens.Add(new Message("RG inválido !"));
				}
			}

			return Mensagens;
		}
	}
}