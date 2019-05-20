using System.Collections.Generic;
using LES.Models;
using LES.Utils;
using LES.Structure;
using System.Data.Entity;

namespace LES.Strategies.Autores
{
	[CommandType(Command.Insert, Command.Update)]
	public class ValidarAutor : PreStrategy<Autor>
	{
		public ValidarAutor(DbContext Context) : base(Context)
		{
		}

		public override List<Message> Process(Autor Entity)
		{
			var Mensagens = new List<Message>();

			if (Entity == null)
				Mensagens.Add(new Message("Informações do autor não foram preenchidas!"));
			else
			{
				Entity.TrimAllStrings();

				if (string.IsNullOrWhiteSpace(Entity.Nome) || Entity.Nome.Length < 3)
					Mensagens.Add(new Message("Nome do autor não preenchido ou inválido !"));

			}
			return Mensagens;
		}
	}
}