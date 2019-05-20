using LES.Models;
using LES.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LES.Models
{
	public class Endereco : Dominio
	{
		public string Cep { get; set; }
		public string Logradouro { get; set; }
		public string Numero { get; set; }
		public string Bairro { get; set; }
		public string Municipio { get; set; }
		public string Complemento { get; set; }
		public string Observacao { get; set; }
		public Estado Estado { get; set; }
		public string Formatado
		{
			get
			{
				return string.Concat(
					Logradouro,
					string.IsNullOrEmpty(Numero) ? "" : string.Concat(", ", Numero),
					" - ", Bairro, ", ", Municipio, " - ", Estado?.UF,
					string.IsNullOrEmpty(Cep) ? "" : string.Concat(" - ", Cep));
			}
		}

		public override bool Equals(object obj)
		{
			if (!(obj is Endereco))
				return false;

			var Endereco = obj as Endereco;

			if (!Cep.EqualsNormalized(Endereco.Cep) || !Logradouro.EqualsNormalized(Endereco.Logradouro) ||
				!Numero.EqualsNormalized(Endereco.Numero) || !Bairro.EqualsNormalized(Endereco.Bairro) ||
				!Municipio.EqualsNormalized(Endereco.Municipio) || !Complemento.EqualsNormalized(Endereco.Complemento) ||
				!Observacao.EqualsNormalized(Endereco.Observacao) || !Estado.Equals(Endereco.Estado))
				return false;

			return true;
		}
	}
}