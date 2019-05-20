using LES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LES.Models
{
	public class Livro : Dominio
	{
		public Titulo Titulo { get; set; }
		public Unidade Unidade { get; set; }
		public string CodigoBiblioteca { get; set; }
		public string Observacoes { get; set; }
	}
}