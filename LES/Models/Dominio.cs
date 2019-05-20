using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LES.Models
{
	public class Dominio
	{
		public long Id { get; set; }
		public DateTime? DataCadastro { get; set; }
		public Usuario UsuarioCadastro { get; set; }
		public bool? Ativo { get; set; }
	}
}