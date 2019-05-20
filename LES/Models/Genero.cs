using LES.Models;
using LES.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LES.Models
{
	public class Genero : Dominio
	{
		public string Nome { get; set; }

		public override bool Equals(object obj)
		{
			if (!(obj is Genero))
				return false;

			var Genero = obj as Genero;

			if (!Nome.EqualsNormalized(Genero.Nome))
				return false;

			return true;
		}
	}
}