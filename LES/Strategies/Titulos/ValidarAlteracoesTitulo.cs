using LES.Data.Repositories;
using LES.Models;
using LES.Strategies.Editoras;
using LES.Structure;
using LES.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LES.Strategies.Titulos
{
	[CommandType(Command.Update)]
	public class ValidarAlteracoesTitulo : PreStrategy<Titulo>
	{
		public ValidarAlteracoesTitulo(DbContext Context) : base(Context)
		{
		}

		public override List<Message> Process(Titulo Entity)
		{
			var Mensagens = new List<Message>();

			if (Entity != null)
			{
				var Titulo = Repository.Get(new Filter { Id = Entity.Id })?.FirstOrDefault();

				if (Titulo == null)
					Mensagens.Add(new Message("Titulo não localizado!"));				
			}

			return Mensagens;
		}
	}
}