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
	public class UnidadeRepository : Repository<Unidade>
	{
		public UnidadeRepository(DbContext Context, Usuario Usuario = null) : base(Context, Usuario)
		{
		}

		protected override IEnumerable<Unidade> GetActiveOnly()
		{
			var Retorno = base.GetActiveOnly();

			if (Usuario.Unidade != null && Usuario.Unidade.Id != 0)
				return Retorno.Where(x => x.Id == Usuario.Unidade.Id);

			return Retorno;
		}

		public override IEnumerable<Unidade> Update(params Unidade[] Entities)
		{
			return Process(() =>
			{
				foreach (var Entity in Entities)
				{
					var Original = GetById(Entity.Id);
					SaveUpdatedObject(Original, Entity);					
					Context.Entry(Original).CurrentValues.SetValues(Entity);
					Context.Entry(Original.Endereco).CurrentValues.SetValues(Entity.Endereco);
				}

				Save();
				return Entities;
			});
		}
		
		private void SaveUpdatedObject(Unidade Original, Unidade Updated)
		{
			var Principal = SerializeObject(Original);


			if (!Original.Equals(Updated))
			{
				var Entity = "Unidade";
				SaveChanges(Principal, Usuario, Original.Id, EntityName: Entity);
			}

			if (!Original.Endereco.Equals(Updated.Endereco))
			{
				var Entity = "Unidade.Endereço";
				var Alterado = SerializeObject(Original.Endereco);
				SaveChanges(Principal, Usuario, Original.Id, Alterado, Entity);
			}
		}

		protected override IEnumerable<Unidade> GetWithParameters(params Filter[] Filters)
		{
			var Unidades = Set.Include(x => x.Endereco).Include(x => x.Endereco.Estado)
								.Include(x => x.UsuarioCadastro);

			var Normalizados = Filters.ToList();
			Normalizados.ForEach(x => { x.TrimAllStrings(); x.UpperCaseAll(); });

			if (Filters.Any(x => x.Property == "NOME"))
			{
				var Nome = Filters.FirstOrDefault(f => f.Property == "NOME").Value.ToString();
				Unidades = Unidades.Where(x => x.Nome.Contains(Nome));
			}

			if (Filters.Any(x => x.Property == "UF"))
			{
				var Estado = Convert.ToInt32(Filters.FirstOrDefault(f => f.Property == "UF").Value);
				Unidades = Unidades.Where(x => x.Endereco.Estado.Id == Estado);
			}

			return Unidades;
		}
		
		protected override Unidade GetById(long id)
		{
			return Set
					.Include(x => x.Endereco)
					.Include(x => x.Endereco.Estado)
					.Include(x => x.UsuarioCadastro)
					.FirstOrDefault(x => x.Id == id);
		}
	}
}