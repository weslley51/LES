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
	[CommandType(Command.Insert)]
	public class InformacoesRegistro : PreStrategy<Dominio>
	{
		public InformacoesRegistro(DbContext Context) : base(Context)
		{
		}

		public override List<Message> Process(Dominio Entity)
		{
			var Usuario = new Usuario();
			var Mensagens = new List<Message>();

			if (Entity == null)
				Mensagens.Add(new Message("Informações da entidade não foram preenchidas!"));
			else
			{
				if (HttpContext.Current?.Session?["UsuarioLogado"] != null)
					Usuario = HttpContext.Current.Session["UsuarioLogado"] as Usuario;
				else
					Usuario = new Repository<Usuario>(Context).Get(new Filter { Id = 1 }).FirstOrDefault();

				if (Usuario == null)
					return new List<Message> { new Message("Usuário não localizado !") };

				Entity.UsuarioCadastro = Usuario;
				Entity.DataCadastro = DateTime.Now;
				Entity.Ativo = true;
			}

			return null;
		}
	}
}