using LES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LES.Models
{
	public class Unidade : Dominio
	{
		public string Nome { get; set; }
		public Endereco Endereco { get; set; }
		public ICollection<Contato> Contatos { get; set; }
	}
}