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

namespace LES.Strategies.Alugueis
{
	[CommandType(Command.Update)]
	public class CalcularMulta : PostStrategy<Aluguel>
	{
		public CalcularMulta(DbContext Context) : base(Context)
		{
		}

		public override List<Message> Process(Aluguel Entity)
		{
			var Mensagens = new List<Message>();

			if (Entity.DataDevolucao > Entity.DataPrevistaDevolucao)
			{
				var Diferenca = Entity.DataDevolucao - Entity.DataPrevistaDevolucao;
				decimal Multa = Diferenca?.Days == 0 ? 2 : Diferenca.Value.Days * 2;

				var Cliente = Context.Set<Cliente>().Find(Entity.Cliente.Id);
				Cliente.Saldo = Cliente.Saldo == null ? Multa : Cliente.Saldo + Multa;

				var Aluguel = Context.Set<Aluguel>().Find(Entity.Id);

				Context.Set<Multa>().Add(new Multa
				{
					Ativo = true,
					Valor = Multa,
					Aluguel = Aluguel,
					Cliente = Cliente,
					DataCadastro = DateTime.Now,
					TipoMulta = TipoMulta.Atraso,
					UsuarioCadastro = Context.Set<Usuario>().Find(1)
				});

				Context.SaveChanges();
			}
						
			return Mensagens;
		}
	}
}