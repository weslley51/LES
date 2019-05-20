using LES.Models;
using LES.Structure;
using LES.Utils;
using LES.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LES.Strategies.Dominios
{
	[CommandType(Command.Delete)]
	public class InativarRegistro : PreStrategy<Dominio>
	{
		public InativarRegistro(DbContext Context) : base(Context)
		{
		}

		public override List<Message> Process(Dominio Entity)
		{
			var Mensagens = new List<Message>();

			if (Entity == null)
				Mensagens.Add(new Message("Informações da entidade não foram preenchidas!"));
			else			
				Entity.Ativo = false;
			
			return Mensagens;
		}
	}
}