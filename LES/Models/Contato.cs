using LES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LES.Models
{
	public class Contato : Dominio
	{
		public bool? Email { get; set; }
		public string Valor { get; set; }
		public string Observacoes { get; set; }
	}
}