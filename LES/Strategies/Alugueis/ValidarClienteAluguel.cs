using LES.Models;
using LES.Structure;
using LES.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;

namespace LES.Strategies.Alugueis
{
	[CommandType(Command.Insert)]
	public class ValidarClienteAluguel : PreStrategy<Aluguel>
	{
		public ValidarClienteAluguel(DbContext Context) : base(Context)
		{
		}

		public override List<Message> Process(Aluguel Entity)
		{
			var Mensagens = new List<Message>();

			if (Entity?.Cliente == null || Entity?.Cliente.Id == 0)
				Mensagens.Add(new Message("Não foi informada o Cliente do Aluguel!"));
			else
			{
				Entity.Cliente = Context.Set<Cliente>().Find(Entity.Cliente.Id);

				if (Entity.Cliente == null)
					Mensagens.Add(new Message("O Cliente associada ao Aluguel não foi localizado!"));
				else
				{
					if (Entity.Cliente.Saldo != null && Entity.Cliente.Saldo > 0)
						Mensagens.Add(new Message("O Cliente associado ao Aluguel contém pendências!"));
					
					if (Context.Set<Aluguel>().Any(x => x.Cliente.Id == Entity.Cliente.Id && x.DataDevolucao == null && x.DataPrevistaDevolucao.Value < DateTime.Now))
						Mensagens.Add(new Message("O Cliente deve primeiro devolver os livros atrasados!"));

					if (Context.Set<Aluguel>().Count(x => x.Cliente.Id == Entity.Cliente.Id && x.DataDevolucao == null) > 5)
						Mensagens.Add(new Message("O Cliente já atingiu o máximo de aluguéis simultâneos!"));
				}
			}

			return Mensagens;
		}
	}
}