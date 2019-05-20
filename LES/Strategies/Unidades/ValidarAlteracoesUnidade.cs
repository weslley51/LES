using LES.Data.Repositories;
using LES.Models;
using LES.Strategies.Enderecos;
using LES.Structure;
using LES.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LES.Strategies.Unidades
{
	[CommandType(Command.Update)]
	public class ValidarAlteracoesUnidade : PreStrategy<Unidade>
	{
		public ValidarAlteracoesUnidade(DbContext Context) : base(Context)
		{
		}

		public override List<Message> Process(Unidade Entity)
		{
			var Mensagens = new List<Message>();

			if (Entity != null)				
			{
				var Unidade = Repository.Get(new Filter { Id = Entity.Id })?.FirstOrDefault();

				if (Unidade == null)
					Mensagens.Add(new Message("Unidade não localizado!"));
				else
				{
					var ValidacoesRelacionadas = new List<Message>();

					if (Entity.Endereco == null)
						Entity.Endereco = Unidade.Endereco;
					else
					{
						ValidacoesRelacionadas = new ValidarEndereco(Context).Process(Entity.Endereco);

						if (ValidacoesRelacionadas != null && ValidacoesRelacionadas.Count > 0)
							Mensagens.AddRange(ValidacoesRelacionadas);
					}
				}
			}

			return Mensagens;
		}
	}
}