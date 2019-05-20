using LES.Models;
using LES.Structure;
using LES.Utils;
using System.Collections.Generic;
using System.Data.Entity;

namespace LES.Strategies.LancamentosEstoque
{
	[CommandType(Command.Insert)]
	public class ValidarUnidadeEstoque : PreStrategy<Estoque>
	{
		public ValidarUnidadeEstoque(DbContext Context) : base(Context)
		{
		}

		public override List<Message> Process(Estoque Entity)
		{
			var Mensagens = new List<Message>();

			if (Entity?.Unidade == null || Entity?.Unidade.Id == 0)
				Mensagens.Add(new Message("Não foi informada a Unidade do estoque!"));
			else
			{
				Entity.Unidade = Context.Set<Unidade>().Find(Entity.Unidade.Id);

				if (Entity.Unidade == null)
					Mensagens.Add(new Message("A Unidade associada ao estoque não foi localizada!"));
			}
			return Mensagens;
		}
	}
}