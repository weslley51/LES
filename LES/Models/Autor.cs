using LES.Models;
using LES.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LES.Models
{
	public class Autor : Dominio
	{
		public string Nome { get; set; }

		public override bool Equals(object obj)
		{
			if (!(obj is Autor))
				return false;

			var Autor = obj as Autor;

			if (!Nome.EqualsNormalized(Autor.Nome))
				return false;

			return true;
		}
	}
}