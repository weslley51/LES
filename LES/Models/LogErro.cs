using LES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LES.Models
{
	public class LogErro : Dominio
	{
		public string MensagemPrincipal { get; set; }
		public string OutrasMensagens { get; set; }
	}
}