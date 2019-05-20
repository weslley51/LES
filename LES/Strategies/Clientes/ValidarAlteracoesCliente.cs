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

namespace LES.Strategies.Clientes
{
	[CommandType(Command.Update)]
	public class ValidarAlteracoesCliente : PreStrategy<Cliente>
	{
		public ValidarAlteracoesCliente(DbContext Context) : base(Context)
		{
		}

		public override List<Message> Process(Cliente Entity)
		{
			var Mensagens = new List<Message>();

			if (Entity != null)				
			{
				var Cliente = Repository.Get(new Filter { Id = Entity.Id })?.FirstOrDefault();

				if (Cliente == null)
					Mensagens.Add(new Message("Cliente não localizado!"));
				else
				{
					var ValidacoesRelacionadas = new List<Message>();

					if (Entity.Endereco == null)
						Entity.Endereco = Cliente.Endereco;
					else
					{
						ValidacoesRelacionadas = new ValidarEndereco(Context).Process(Entity.Endereco);

						if (ValidacoesRelacionadas != null && ValidacoesRelacionadas.Count > 0)
							Mensagens.AddRange(ValidacoesRelacionadas);
					}

					if (Entity.Contatos == null)
						Entity.Contatos = Cliente.Contatos;
					else
					{
						ValidacoesRelacionadas = new ValidarContatosCliente(Context).Process(Entity);

						if (ValidacoesRelacionadas != null && ValidacoesRelacionadas.Count > 0)
							Mensagens.AddRange(ValidacoesRelacionadas);
					}
				}
			}

			return Mensagens;
		}
	}
}