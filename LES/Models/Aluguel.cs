using LES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LES.Models
{
	public class Aluguel : Dominio
	{
		public DateTime? DataDevolucao { get; set; }
		public DateTime? DataPrevistaDevolucao { get; set; }
		public Livro Livro { get; set; }
		public Multa Multa { get; set; }
		public Cliente Cliente { get; set; }
		public Unidade Unidade { get; set; }		
	}
}