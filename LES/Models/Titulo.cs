using LES.Models;
using LES.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LES.Models
{
	public class Titulo : Dominio
	{
		public short PrazoDevolucao { get; set; }
		public short AnoPublicacao { get; set; }
		public short QuantiaFolhas { get; set; }
		public string Nome { get; set; }
		public string Descricao { get; set; }
		public string Sinopse { get; set; }
		public Editora Editora { get; set; }

		public ICollection<Genero> Generos { get; set; }
		public ICollection<Autor> Autores { get; set; }

		public override bool Equals(object obj)
		{
			if (!(obj is Titulo))
				return false;

			var Titulo = obj as Titulo;

			if (!Nome.EqualsNormalized(Titulo.Nome) || !Descricao.EqualsNormalized(Titulo.Descricao) ||
				!Sinopse.EqualsNormalized(Titulo.Sinopse) || PrazoDevolucao != Titulo.PrazoDevolucao ||
				AnoPublicacao != Titulo.AnoPublicacao || QuantiaFolhas != Titulo.QuantiaFolhas || 
				Editora?.Id != Titulo.Editora?.Id || Ativo != Titulo.Ativo)
				return false;

			return true;
		}
	}
}