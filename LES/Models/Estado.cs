using LES.Models;
using LES.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LES.Models
{
	public class Estado : Dominio
	{
		public string UF { get; set; }
		public string Nome { get; set; }

		public override bool Equals(object obj)
		{
			if (!(obj is Estado))
				return false;

			var Estado = obj as Estado;

			if (Id != Estado.Id || !UF.EqualsNormalized(Estado.UF))
				return false;

			return true;
		}
	}
}