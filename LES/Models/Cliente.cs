using LES.Models;
using LES.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LES.Models
{
	public class Cliente : Dominio
	{
		public string NomeCompleto { get; set; }
		public string RG { get; set; }
		public string CPF { get; set; }
		public decimal? Saldo { get; set; }
		public Sexo Sexo { get; set; }		
		public Endereco Endereco { get; set; }

		public ICollection<Contato> Contatos { get; set; }

		public override bool Equals(object obj)
		{
			if (!(obj is Cliente))
				return false;

			var Cliente = obj as Cliente;

			if (!NomeCompleto.EqualsNormalized(Cliente.NomeCompleto) || !RG.EqualsNormalized(Cliente.RG) ||
				!CPF.EqualsNormalized(Cliente.CPF) || Saldo != Cliente.Saldo || Sexo != Cliente.Sexo || Ativo != Cliente.Ativo)
				return false;

			return true;
		}
	}
}