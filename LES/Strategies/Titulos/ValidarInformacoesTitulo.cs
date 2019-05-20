using System.Collections.Generic;
using System.Linq;
using LES.Models;
using LES.Strategies.Enderecos;
using LES.Utils;
using LES.Structure;
using System.Data.Entity;
using LES.Strategies.Contatos;
using LES.Strategies.Dominios;
using System;

namespace LES.Strategies.Titulos
{
	[CommandType(Command.Insert, Command.Update)]
	public class ValidarInformacoesTitulo : PreStrategy<Titulo>
	{
		public ValidarInformacoesTitulo(DbContext Context) : base(Context)
		{
		}

		public override List<Message> Process(Titulo Entity)
		{
			var Mensagens = new List<Message>();

			if (Entity == null)
				Mensagens.Add(new Message("Informações do titulo não foram preenchidas!"));
			else
			{
				Entity.TrimAllStrings();

				if (Entity.AnoPublicacao <= 0 || Entity.AnoPublicacao > DateTime.Now.Year)
					Mensagens.Add(new Message("Ano de Publicação não preenchido ou inválido !"));

				if (Entity.PrazoDevolucao <= 0)
					Mensagens.Add(new Message("Prazo de devolução não preenchido ou inválido !"));

				if (Entity.QuantiaFolhas <= 0)
					Mensagens.Add(new Message("Quantidade de folhas não preenchida ou inválido !"));

				if (string.IsNullOrWhiteSpace(Entity.Nome) || Entity.Nome.Length < 3)
					Mensagens.Add(new Message("Título não preenchido ou inválido !"));
			}
			
			return Mensagens;
		}
	}
}