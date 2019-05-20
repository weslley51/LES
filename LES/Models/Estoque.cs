using LES.Models;
using LES.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LES.Models
{
	public class Estoque : Dominio
	{
		public int Quantidade { get; set; }
		public string Descricao { get; set; }
		public Titulo Titulo { get; set; }
		public Unidade Unidade { get; set; }
		public TipoMovimentacao TipoMovimentacao { get; set; }
	}
}