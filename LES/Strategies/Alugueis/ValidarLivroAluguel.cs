using LES.Models;
using LES.Structure;
using LES.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace LES.Strategies.Alugueis
{
	[CommandType(Command.Insert)]
	public class ValidarLivroAluguel : PreStrategy<Aluguel>
	{
		public ValidarLivroAluguel(DbContext Context) : base(Context)
		{
		}

		public override List<Message> Process(Aluguel Entity)
		{
			var Mensagens = new List<Message>();

			if (Entity?.Livro == null || Entity?.Livro.Id == 0)
				Mensagens.Add(new Message("Não foi informado o Livro do Aluguel!"));
			else
			{
				Entity.Livro = Context.Set<Livro>().Include(x => x.Titulo).FirstOrDefault(x => x.Id == Entity.Livro.Id);

				if (Entity.Livro == null)
					Mensagens.Add(new Message("O Livro associado ao Aluguel não foi localizado!"));
				else
				{
					if (Context.Set<Aluguel>().Any(x => x.Livro.Id == Entity.Livro.Id && x.DataDevolucao == null))
						Mensagens.Add(new Message($"O Livro {Entity.Livro.CodigoBiblioteca} já está alugado!"));
					else
						Entity.DataPrevistaDevolucao = DateTime.Now.AddDays(Entity.Livro.Titulo.PrazoDevolucao);
				}
			}
			return Mensagens;
		}
	}
}