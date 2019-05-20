using LES.Models;
using LES.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LES.Models
{
	public class Multa : Dominio
	{
		public decimal Valor { get; set; }
		public TipoMulta TipoMulta { get; set; }
		public Cliente Cliente { get; set; }
		public Aluguel Aluguel { get; set; }
	}
}