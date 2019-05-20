using LES.Structure;
using LES.Utils;
using LES.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using LES.Strategies.Estados;
using System.Linq;
using LES;
using LES.Strategies.Dominios;
using LES.Models;

namespace LES.Strategies.Enderecos
{
	[CommandType(Command.Insert, Command.Update)]
	public class ValidarEndereco : PreStrategy<Endereco>
	{
		public ValidarEndereco(DbContext Context) : base(Context)
		{
		}

		public override List<Message> Process(Endereco Entity)
		{
			var Mensagens = new List<Message>();

			if (Entity == null)
				Mensagens.Add(new Message("Informações do endereço não foram preenchidas!"));
			else
			{
				Entity.TrimAllStrings(new string[] { "Formatado" });

				if (string.IsNullOrWhiteSpace(Entity.Logradouro) || Entity.Logradouro.Length < 5)
					Mensagens.Add(new Message("Logradouro não preenchido ou inválido !"));

				if (string.IsNullOrWhiteSpace(Entity.Numero))
					Entity.Numero = "S/N";

				if (string.IsNullOrWhiteSpace(Entity.Bairro) || Entity.Bairro.Length < 3)
					Mensagens.Add(new Message("Bairro não preenchido ou inválido !"));

				if (string.IsNullOrWhiteSpace(Entity.Municipio) || Entity.Municipio.Length < 3)
					Mensagens.Add(new Message("Município não preenchido ou inválido !"));

				if (string.IsNullOrWhiteSpace(Entity.Cep))
					Mensagens.Add(new Message("CEP não preenchido !"));
				else
				{
					Entity.Cep = new String(Entity.Cep.Where(x => char.IsNumber(x)).ToArray());

					if (Entity.Cep.Length != 8)
						Mensagens.Add(new Message("CEP inválido !"));
				}

				if (Entity.Estado == null)
					Mensagens.Add(new Message("Estado não preenchido !"));
				else
				{
					var ValidarEstado = new ValidarEstado(Context).Process(Entity.Estado);

					if (ValidarEstado != null)
						Mensagens.AddRange(ValidarEstado);
					else
						Entity.Estado = Context.Set<Estado>().Find(Entity.Estado.Id);
				}
			}

			return Mensagens;
		}
	}
}