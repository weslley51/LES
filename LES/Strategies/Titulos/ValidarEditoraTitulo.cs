using LES.Models;
using LES.Strategies.Editoras;
using LES.Strategies.Dominios;
using LES.Structure;
using LES.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LES.Strategies.Titulos
{
	[CommandType(Command.Insert, Command.Update)]
	public class ValidarEditoraTitulo : PreStrategy<Titulo>
	{
		public ValidarEditoraTitulo(DbContext Context) : base(Context)
		{
		}

		public override List<Message> Process(Titulo Entity)
		{
			var Mensagens = new List<Message>();

			if (Entity?.Editora == null || Entity?.Editora.Id == 0)
				Mensagens.Add(new Message("Não foi informada a editora do livro!"));
			else
			{
				Entity.Editora = Context.Set<Editora>().Find(Entity.Editora.Id);

				if (Entity.Editora == null)
					Mensagens.Add(new Message("A editora associada ao título não foi localizada!"));
			}
			return Mensagens;
		}
	}
}