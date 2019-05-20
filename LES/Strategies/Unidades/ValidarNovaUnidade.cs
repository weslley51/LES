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

namespace LES.Strategies.Unidades
{

	[CommandType(Command.Insert)]
	public class ValidarNovaUnidade : PreStrategy<Unidade>
	{
		public ValidarNovaUnidade(DbContext Context) : base(Context)
		{
		}

		public override List<Message> Process(Unidade Entity)
		{
			var Mensagens = new List<Message>();

			if (Entity != null)				
			{
				var ValidacoesRelacionadas = new ValidarEndereco(Context).Process(Entity.Endereco);

				if (ValidacoesRelacionadas != null && ValidacoesRelacionadas.Count > 0)
					Mensagens.AddRange(ValidacoesRelacionadas);
				else
				{
					var InformacoesRegistro = new InformacoesRegistro(Context).Process(Entity.Endereco);

					if (InformacoesRegistro != null)
						Mensagens.AddRange(InformacoesRegistro);
				}
			}

			return Mensagens;
		}
	}
}