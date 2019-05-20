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

namespace LES.Strategies.LancamentosEstoque
{
	[CommandType(Command.Insert)]
	public class ValidarEstoque : PreStrategy<Estoque>
	{
		public ValidarEstoque(DbContext Context) : base(Context)
		{
		}

		public override List<Message> Process(Estoque Entity)
		{
			var Mensagens = new List<Message>();

			if (Entity == null)
				Mensagens.Add(new Message("Informações do estoque não foram preenchidas!"));
			else
			{
				Entity.TrimAllStrings();

				if (Entity.Quantidade <= 0)
					Mensagens.Add(new Message("Quantidade não preenchida ou inválida !"));

				if (Entity.TipoMovimentacao == 0)
					Mensagens.Add(new Message("Tipo de movimentação não preenchida ou inválida !"));
			}

			return Mensagens;
		}
	}
}