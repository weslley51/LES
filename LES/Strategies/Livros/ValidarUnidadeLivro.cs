using LES.Models;
using LES.Structure;
using LES.Utils;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace LES.Strategies.Livros
{
	[CommandType(Command.Insert, Command.Update)]
	public class ValidarUnidadeLivro : PreStrategy<Livro>
	{
		public ValidarUnidadeLivro(DbContext Context) : base(Context)
		{
		}

		public override List<Message> Process(Livro Entity)
		{
			var Mensagens = new List<Message>();

			if (Entity?.Unidade == null || Entity?.Unidade.Id == 0)
				Mensagens.Add(new Message("Não foi informada a Unidade do Livro!"));
			else
			{
				Entity.Unidade = Context.Set<Unidade>().Include(x => x.Endereco).Include(x => x.UsuarioCadastro).Where(x => x.Id == Entity.Unidade.Id).FirstOrDefault();

				if (Entity.Unidade == null)
					Mensagens.Add(new Message("A Unidade associada ao Livro não foi localizada!"));
			}
			return Mensagens;
		}
	}
}