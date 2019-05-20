using LES.Models;
using LES.Structure;
using LES.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace LES.Strategies.Alugueis
{
	[CommandType(Command.Update)]
	public class ValidarAlteracaoAluguel : PreStrategy<Aluguel>
	{
		public ValidarAlteracaoAluguel(DbContext Context) : base(Context)
		{
		}

		public override List<Message> Process(Aluguel Entity)
		{
			var Mensagens = new List<Message>();

			if (Entity == null || Entity?.Id == 0)
				Mensagens.Add(new Message("Não foi informada o Aluguel a ser dado baixa!"));
			else
			{
				var Aluguel = Repository.Get(new Filter(Entity))?.FirstOrDefault();

				if (Aluguel == null)
					Mensagens.Add(new Message("O Aluguel não foi localizado!"));
				else
				{
					Entity.Multa = Aluguel.Multa;
					Entity.DataDevolucao = DateTime.Now;
					Entity.DataCadastro = Aluguel.DataCadastro;					
					Entity.DataPrevistaDevolucao = Aluguel.DataPrevistaDevolucao;
				}				
			}
			return Mensagens;
		}
	}
}