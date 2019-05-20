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
	public class EstoqueRepository : Repository<Estoque>
	{
		public EstoqueRepository(DbContext Context, Usuario Usuario = null) : base(Context, Usuario)
		{
		}

		protected override IEnumerable<Estoque> GetActiveOnly()
		{
			var Retorno = base.GetActiveOnly();

			if (Usuario.Unidade != null && Usuario.Unidade.Id != 0)
				return Retorno.Where(x => x.Unidade.Id == Usuario.Unidade.Id);

			return Retorno;
		}

		protected override Estoque GetById(long id)
		{
			return Set.Include(x => x.Unidade).Include(x => x.Titulo).Include(x => x.Titulo.Autores).Include(x => x.Titulo.Editora).Include(x => x.Titulo.Generos).FirstOrDefault(x => x.Id == id);
		}

		protected override IEnumerable<Estoque> GetAll()
		{
			var Retorno = Set.Include(x => x.Titulo).Include(x => x.Titulo.Autores).Include(x => x.Titulo.Editora).Include(x => x.Titulo.Generos);

			if (Usuario.Unidade != null && Usuario.Unidade.Id != 0)
				return Retorno.Where(x => x.Unidade.Id == Usuario.Unidade.Id);

			return Retorno;
		}

		protected override IEnumerable<Estoque> GetWithParameters(params Filter[] Filters)
		{
			var Estoques = Set.Include(x => x.Unidade).Include(x => x.Titulo).Include(x => x.Titulo.Autores).Include(x => x.Titulo.Editora).Include(x => x.Titulo.Generos);			

			var Normalizados = Filters.ToList();
			Normalizados.ForEach(x => { x.TrimAllStrings(); x.UpperCaseAll(); });

			if (Filters.Any(x => x.Property == "TITULO"))
			{
				var Titulo = Convert.ToInt32(Filters.FirstOrDefault(f => f.Property == "TITULO").Value);
				Estoques = Estoques.Where(x => x.Titulo.Id == Titulo);
			}

			if (Filters.Any(x => x.Property == "UNIDADE"))
			{
				var Unidade = Convert.ToInt32(Filters.FirstOrDefault(f => f.Property == "UNIDADE").Value);
				Estoques = Estoques.Where(x => x.Unidade.Id == Unidade);
			}
			
			if (Filters.Any(x => x.Property == "TIPOSMOVIMENTACAO"))
			{
				var Tipo = Convert.ToChar(Filters.FirstOrDefault(f => f.Property == "TIPOSMOVIMENTACAO").Value);
				Estoques = Estoques.Where(x => x.TipoMovimentacao == (TipoMovimentacao)Tipo);
			}

			if (Filters.Any(x => x.Property == "DESCRICAO"))
			{
				var Descricao = Filters.FirstOrDefault(f => f.Property == "DESCRICAO").Value.ToString().ToUpper();
				Estoques = Estoques.Where(x => x.Descricao.ToUpper().Contains(Descricao));
			}

			if (Usuario.Unidade != null && Usuario.Unidade.Id != 0)
				Estoques = Estoques.Where(x => x.Unidade.Id == Usuario.Unidade.Id);

			return Estoques;
		}

		public override IEnumerable<Estoque> Update(params Estoque[] Entities)
		{
			return Process(() =>
			{
				foreach (var Entity in Entities)
				{
					var Original = GetById(Entity.Id);
					SaveUpdatedObject(Original, Entity);
					Context.Entry(Original).CurrentValues.SetValues(Entity);
					Original.Titulo = Entity.Titulo;
					Original.Unidade = Entity.Unidade;
				}

				Save();
				return Entities;
			});
		}

		private void SaveUpdatedObject(Estoque Original, Estoque Updated)
		{
			var Principal = SerializeObject(Original);

			if (!Original.Equals(Updated))
			{
				var Entity = "Estoque";
				SaveChanges(Principal, Usuario, Original.Id, EntityName: Entity);
			}

			if (!Original.Titulo.Equals(Updated.Titulo))
			{
				var Entity = "Estoque.Titulo";
				var Alterado = SerializeObject(Original.Titulo);
				SaveChanges(Principal, Usuario, Original.Id, Alterado, Entity);
			}

			if (!Original.Unidade.Equals(Updated.Unidade))
			{
				var Entity = "Estoque.Unidade";
				var Alterado = SerializeObject(Original.Unidade);
				SaveChanges(Principal, Usuario, Original.Id, Alterado, EntityName: Entity);
			}
		}
	}
}