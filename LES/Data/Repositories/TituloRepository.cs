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
	public class TituloRepository : Repository<Titulo>
	{
		public TituloRepository(DbContext Context, Usuario Usuario = null) : base(Context, Usuario)
		{
		}

		protected override Titulo GetById(long id)
		{
			return Set.Include(x => x.Autores).Include(x => x.Editora).Include(x => x.Generos).FirstOrDefault(x => x.Id == id);
		}

		protected override IEnumerable<Titulo> GetAll()
		{
			return Set.Include(x => x.Autores).Include(x => x.Editora).Include(x => x.Generos);
		}

		protected override IEnumerable<Titulo> GetWithParameters(params Filter[] Filters)
		{
			var Titulos = Set.Include(x => x.Autores).Include(x => x.Editora).Include(x => x.Generos);

			var Normalizados = Filters.ToList();
			Normalizados.ForEach(x => { x.TrimAllStrings(); x.UpperCaseAll(); });

			if (Filters.Any(x => x.Property == "AUTOR"))
			{
				var Autor = Convert.ToInt32(Filters.FirstOrDefault(f => f.Property == "AUTOR").Value);
				Titulos = Titulos.Where(x => x.Autores.Any(y => y.Id == Autor));
			}

			if (Filters.Any(x => x.Property == "GENERO"))
			{
				var Genero = Convert.ToInt32(Filters.FirstOrDefault(f => f.Property == "GENERO").Value);
				Titulos = Titulos.Where(x => x.Generos.Any(y => y.Id == Genero));
			}

			if (Filters.Any(x => x.Property == "EDITORA"))
			{
				var Editora = Convert.ToInt32(Filters.FirstOrDefault(f => f.Property == "EDITORA").Value);
				Titulos = Titulos.Where(x => x.Editora.Id == Editora);
			}

			if (Filters.Any(x => x.Property == "NOME"))
			{
				var Nome = Filters.FirstOrDefault(f => f.Property == "NOME").Value.ToString().ToUpper();
				Titulos = Titulos.Where(x => x.Nome.ToUpper().Contains(Nome));
			}

			return Titulos;
		}

		public override IEnumerable<Titulo> Update(params Titulo[] Entities)
		{
			return Process(() =>
			{
				foreach (var Entity in Entities)
				{
					var Original = GetById(Entity.Id);
					SaveUpdatedObject(Original, Entity);
					Context.Entry(Original).CurrentValues.SetValues(Entity);
					Original.Editora = Entity.Editora;
					Original.Autores = new List<Autor>(Entity.Autores);
					Original.Generos = new List<Genero>(Entity.Generos);
				}

				Save();
				return Entities;
			});
		}
		
		private void SaveUpdatedObject(Titulo Original, Titulo Updated)
		{
			var Principal = SerializeObject(Original);


			if (!Original.Equals(Updated))
			{
				var Entity = "Titulo";
				SaveChanges(Principal, Usuario, Original.Id, EntityName: Entity);
			}

			if (!Original.Generos.Equals(Updated.Generos))
			{
				var Entity = "Titulo.Gêneros";
				var Alterado = SerializeObject(Original.Generos);
				SaveChanges(Principal, Usuario, Original.Id, Alterado, Entity);
			}

			if (!Original.Autores.Equals(Updated.Autores))
			{
				var Entity = "Titulo.Autores";
				var Alterado = SerializeObject(Original.Autores);
				SaveChanges(Principal, Usuario, Original.Id, Alterado, Entity);
			}
		}
	}
}