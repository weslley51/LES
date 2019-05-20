using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LES.Models;
using LES.Data.Repositories;
using LES.Utils;
using LES.Models;
using LES.Structure;
using System.Data.Entity;
using Newtonsoft.Json;
using System.Data.Entity.Validation;

namespace LES.Data.Repositories
{
	public class LivroRepository : Repository<Livro>
	{
		public LivroRepository(DbContext Context, Usuario Usuario = null) : base(Context, Usuario)
		{
		}

		protected override IEnumerable<Livro> GetActiveOnly()
		{
			var Retorno = base.GetActiveOnly();

			if (Usuario.Unidade != null && Usuario.Unidade.Id != 0)
				return Retorno.Where(x => x.Unidade.Id == Usuario.Unidade.Id);

			return Retorno;
		}

		protected override Livro GetById(long id)
		{
			return Set.Include(x => x.Unidade).Include(x => x.Titulo).Include(x => x.Titulo.Autores).Include(x => x.Titulo.Editora).Include(x => x.Titulo.Generos).FirstOrDefault(x => x.Id == id);
		}

		protected override IEnumerable<Livro> GetAll()
		{
			var Retorno = Set.Include(x => x.Titulo).Include(x => x.Titulo.Autores).Include(x => x.Titulo.Editora).Include(x => x.Titulo.Generos);

			if (Usuario.Unidade != null && Usuario.Unidade.Id != 0)
				return Retorno.Where(x => x.Unidade.Id == Usuario.Unidade.Id);

			return Retorno;
		}

		protected override IEnumerable<Livro> GetWithParameters(params Filter[] Filters)
		{
			var Livros = Set.Include(x => x.Unidade).Include(x => x.Titulo).Include(x => x.Titulo.Autores).Include(x => x.Titulo.Editora).Include(x => x.Titulo.Generos);			

			var Normalizados = Filters.ToList();
			Normalizados.ForEach(x => { x.TrimAllStrings(); x.UpperCaseAll(); });

			if (Filters.Any(x => x.Property == "TITULO"))
			{
				var Titulo = Convert.ToInt32(Filters.FirstOrDefault(f => f.Property == "TITULO").Value);
				Livros = Livros.Where(x => x.Titulo.Id == Titulo);
			}

			if (Filters.Any(x => x.Property == "AUTOR"))
			{
				var Autor = Convert.ToInt32(Filters.FirstOrDefault(f => f.Property == "AUTOR").Value);
				Livros = Livros.Where(x => x.Titulo.Autores.Any(y => y.Id == Autor));
			}

			if (Filters.Any(x => x.Property == "GENERO"))
			{
				var Genero = Convert.ToInt32(Filters.FirstOrDefault(f => f.Property == "GENERO").Value);
				Livros = Livros.Where(x => x.Titulo.Generos.Any(y => y.Id == Genero));
			}

			if (Filters.Any(x => x.Property == "EDITORA"))
			{
				var Editora = Convert.ToInt32(Filters.FirstOrDefault(f => f.Property == "EDITORA").Value);
				Livros = Livros.Where(x => x.Titulo.Editora.Id == Editora);
			}

			if (Filters.Any(x => x.Property == "NOME"))
			{
				var Nome = Filters.FirstOrDefault(f => f.Property == "NOME").Value.ToString().ToUpper();
				Livros = Livros.Where(x => x.Titulo.Nome.ToUpper().Contains(Nome));
			}

			if (Filters.Any(x => x.Property == "CODIGO"))
			{
				var Nome = Filters.FirstOrDefault(f => f.Property == "CODIGO").Value.ToString();
				Livros = Livros.Where(x => x.CodigoBiblioteca.Contains(Nome));
			}

			if (Usuario.Unidade != null && Usuario.Unidade.Id != 0)
				Livros = Livros.Where(x => x.Unidade.Id == Usuario.Unidade.Id);

			return Livros;
		}

		public override IEnumerable<Livro> Update(params Livro[] Entities)
		{
			return Process(() =>
			{
				foreach (var Entity in Entities)
				{
					var Original = GetById(Entity.Id);
					SaveUpdatedObject(Original, Entity);
					Context.Entry(Original).CurrentValues.SetValues(Entity);
					Original.Titulo = Entity.Titulo;
					Original.Titulo.Editora = Entity.Titulo.Editora;
					Original.Unidade = Entity.Unidade;
				}

				Save();
				return Entities;
			});
		}

		private void SaveUpdatedObject(Livro Original, Livro Updated)
		{
			var Principal = SerializeObject(Original);

			if (!Original.Equals(Updated))
			{
				var Entity = "Livro";
				SaveChanges(Principal, Usuario, Original.Id, EntityName: Entity);
			}

			if (!Original.Titulo.Equals(Updated.Titulo))
			{
				var Entity = "Livro.Titulo";
				var Alterado = SerializeObject(Original.Titulo);
				SaveChanges(Principal, Usuario, Original.Id, Alterado, Entity);
			}

			if (Original.Unidade == null || !Original.Unidade.Equals(Updated?.Unidade))
			{
				var Entity = "Livro.Unidade";
				var Alterado = SerializeObject(Original?.Unidade);
				SaveChanges(Principal, Usuario, Original.Id, Alterado, EntityName: Entity);
			}
		}
	}
}