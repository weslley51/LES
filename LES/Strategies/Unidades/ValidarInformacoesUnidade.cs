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

namespace LES.Strategies.Unidades
{
	[CommandType(Command.Insert, Command.Update)]
	public class ValidarInformacoesUnidade : PreStrategy<Unidade>
	{
		public ValidarInformacoesUnidade(DbContext Context) : base(Context)
		{
		}

		public override List<Message> Process(Unidade Entity)
		{
			var Mensagens = new List<Message>();

			if (Entity == null)
				Mensagens.Add(new Message("Informações do Unidade não foram preenchidas!"));
			else
			{
				Entity.TrimAllStrings();

				if (string.IsNullOrWhiteSpace(Entity.Nome) || Entity.Nome.Length < 3)
					Mensagens.Add(new Message("Nome do Unidade não preenchido ou inválido !"));
			}

			return Mensagens;
		}
	}
}