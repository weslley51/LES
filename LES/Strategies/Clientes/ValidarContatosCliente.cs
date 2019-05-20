using LES.Models;
using LES.Strategies.Contatos;
using LES.Strategies.Dominios;
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
	public class ValidarContatosCliente : PreStrategy<Cliente>
	{
		public ValidarContatosCliente(DbContext Context) : base(Context)
		{
		}

		public override List<Message> Process(Cliente Entity)
		{
			var Mensagens = new List<Message>();
			
			var ValidarContato = new ValidarContato(Context);
			var InformacoesRegistro = new InformacoesRegistro(Context);

			var ValidacoesRelacionadas = Entity.Contatos?.SelectMany(x => ValidarContato.Process(x) ?? new List<Message>())?.ToList();

			if (ValidacoesRelacionadas != null && ValidacoesRelacionadas.Count > 0 && ValidacoesRelacionadas.Count > 0)
				Mensagens.AddRange(ValidacoesRelacionadas);
			else
			{
				if (Entity.Contatos != null && Entity.Contatos.Any(x => x.Id == 0))
				{
					Entity.Contatos.Where(x => x.Id == 0).ToList().ForEach(x =>
					{
						InformacoesRegistro.Process(x);
					});
				}
			}

			return Mensagens;
		}
	}
}