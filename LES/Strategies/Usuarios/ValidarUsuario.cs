using LES.Models;
using LES.Strategies.Dominios;
using LES.Strategies.Enderecos;
using LES.Structure;
using LES.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LES.Strategies.Usuarios
{

	[CommandType(Command.Insert, Command.Update)]
	public class ValidarUsuario : PreStrategy<Usuario>
	{
		public ValidarUsuario(DbContext Context) : base(Context)
		{
		}

		public override List<Message> Process(Usuario Entity)
		{
			var Mensagens = new List<Message>();

			if (Entity == null)
				Mensagens.Add(new Message("Não foi informado o usuário!"));
			else
			{
				if (Entity.Perfil == null || Entity.Perfil.Id == 0)
					Mensagens.Add(new Message("Não foi informado o perfil do usuário!"));
				else
					Entity.Perfil = Context.Set<Perfil>().Find(Entity.Perfil.Id);
			}
			return Mensagens;
		}
	}
}