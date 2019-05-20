using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LES.Models
{
	public class LogAlteracao : Dominio
	{
		public long EntidadeId { get; set; }
		public string NomeEntidade { get; set; }
		public string ObjetoPrincipal { get; set; }
		public string ObjetoAlterado { get; set; }
	}
}