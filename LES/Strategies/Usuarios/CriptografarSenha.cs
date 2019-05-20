using LES.Models;
using LES.Structure;
using LES.Utils;
using LES.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LES.Strategies.Usuarios
{
	[CommandType(Command.Insert, Command.Update)]
	public class CriptografarSenha : PreStrategy<Usuario>
	{
		public CriptografarSenha(DbContext Context) : base(Context)
		{
		}

		public override List<Message> Process(Usuario Entity)
		{
			if (Entity != null)
			{
				if (Entity.Id == 0)
					Entity.Senha = new MD5Manipulator().Generate("123123");
				else
				{
					var Usuario = Context.Set<Usuario>().Find(Entity.Id);

					if (string.IsNullOrEmpty(Entity.Senha) || Entity.Senha != Usuario.Senha)
						Entity.Senha = new MD5Manipulator().Generate("123123");
				}

			}

			return null;
		}
	}
}