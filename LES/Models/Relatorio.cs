using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LES.Models
{
	public class Relatorio : Dominio
	{
		public List<string> Titulos { get; set; }
		public Dictionary<string, List<double>> Valores { get; set; }
	}
}