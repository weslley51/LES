using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LES.Models;
using LES.Strategies.Enderecos;
using LES.Utils;
using LES.Structure;
using System.Data.Entity;
using LES.Utils;
using LES;
using Newtonsoft.Json;

namespace LES.Strategies.Alugueis
{
	[CommandType(Command.Update)]
	public class LancarAlteracaoEstoque : PostStrategy<Livro>
	{
		public LancarAlteracaoEstoque(DbContext Context) : base(Context)
		{
		}

		public override List<Message> Process(Livro Entity)
		{
			var Mensagens = new List<Message>();
			var NomeEntidade = "Livro.Unidade";

			var Log = Context.Set<LogAlteracao>()
								.Where(x => x.EntidadeId == Entity.Id && x.NomeEntidade == NomeEntidade)
								.OrderByDescending(x => x.DataCadastro)
								.FirstOrDefault();

			var LogObject = JsonConvert.DeserializeObject<Unidade>(Log.ObjetoAlterado);

			if (Entity.Unidade != null && LogObject != null && Entity.Unidade.Id != LogObject.Id)
			{
				Context.Set<Estoque>().Add(new Estoque
				{
					Ativo = true,
					DataCadastro = DateTime.Now,
					Quantidade = 1,
					Titulo = Entity.Titulo,
					TipoMovimentacao = TipoMovimentacao.Transferencia,
					Unidade = LogObject != null ? Context.Set<Unidade>().Find(LogObject.Id) : null,
					UsuarioCadastro = Context.Set<Usuario>().Find(LogObject.UsuarioCadastro.Id),
					Descricao = $"Transferência para a unidade {Entity.Unidade.Nome}!"
				});

				Context.SaveChanges();
			}
						
			return Mensagens;
		}
	}
}