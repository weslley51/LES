using LES.Models;
using LES.Strategies.Contatos;
using LES.Strategies.Dominios;
using LES.Structure;
using LES.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LES.Strategies.Clientes
{
	[CommandType(Command.Update)]
	public class ValidarMulta : PostStrategy<Cliente>
	{
		public ValidarMulta(DbContext Context) : base(Context)
		{
		}

		public override List<Message> Process(Cliente Entity)
		{
			var Mensagens = new List<Message>();
			var NomeEntidade = "Cliente";

			var Log = Context.Set<LogAlteracao>()
								.Where(x => x.EntidadeId == Entity.Id && x.NomeEntidade == NomeEntidade)
								.OrderByDescending(x => x.DataCadastro)
								.FirstOrDefault();

			var LogObject = JsonConvert.DeserializeObject<Cliente>(Log.ObjetoAlterado);

			if (Entity.Saldo != LogObject.Saldo)
			{
				Context.Set<Multa>().Add(new Multa
				{
					Ativo = true,
					DataCadastro = DateTime.Now,					
					TipoMulta = TipoMulta.Pagamento,
					Cliente = Context.Set<Cliente>().Find(Entity.Id),
					Valor = (decimal)(LogObject.Saldo - Entity.Saldo),
					UsuarioCadastro = Context.Set<Usuario>().Find(LogObject.UsuarioCadastro.Id)					
				});

				Context.SaveChanges();
			}

			return Mensagens;
		}
	}
}