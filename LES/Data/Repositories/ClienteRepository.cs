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
	public class ClienteRepository : Repository<Cliente>
	{
		public ClienteRepository(DbContext Context, Usuario Usuario = null) : base(Context, Usuario)
		{
		}

		protected override Cliente GetById(long id)
		{
			return Set.Include(x => x.Endereco).Include(x => x.Endereco.Estado).Include(x => x.Contatos).FirstOrDefault(x => x.Id == id);
		}

		protected override IEnumerable<Cliente> GetAll()
		{
			return Set.Include(x => x.Endereco).Include(x => x.Endereco.Estado);
		}

		protected override IEnumerable<Cliente> GetWithParameters(params Filter[] Filters)
		{
			var Clientes = Set.Include(x => x.Endereco).Include(x => x.Endereco.Estado);
			var Normalizados = Filters.ToList();
			Normalizados.ForEach(x => { x.TrimAllStrings(); x.UpperCaseAll(); });

			if (!Filters.Any(x => x.Property == "ATIVO"))
				Clientes = Clientes.Where(x => x.Ativo == true);
			else
			{
				var Ativo = Convert.ToBoolean(Filters.FirstOrDefault(f => f.Property == "ATIVO").Value);
				Clientes = Clientes.Where(x => x.Ativo == Ativo);
			}

			if (Filters.Any(x => x.Property == "UF"))
			{
				var Estado = Convert.ToInt32(Filters.FirstOrDefault(f => f.Property == "UF").Value);
				Clientes = Clientes.Where(x => x.Endereco.Estado.Id == Estado);
			}

			if (Filters.Any(x => x.Property == "CPF"))
			{
				var Cpf = Filters.FirstOrDefault(f => f.Property == "CPF").Value.ToString();
				Clientes = Clientes.Where(x => x.CPF == Cpf);
			}
			
			if (Filters.Any(x => x.Property == "NOME"))
			{
				var Nome = Filters.FirstOrDefault(f => f.Property == "NOME").Value.ToString();
				Clientes = Clientes.Where(x => x.NomeCompleto.Contains(Nome));
			}

			return Clientes;
		}

		public override IEnumerable<Cliente> Update(params Cliente[] Entities)
		{
			return Process(() =>
			{
				foreach (var Entity in Entities)
				{
					var Original = GetById(Entity.Id);
					SaveUpdatedObject(Original, Entity);
					Context.Entry(Original).CurrentValues.SetValues(Entity);
					Context.Entry(Original.Endereco).CurrentValues.SetValues(Entity.Endereco);

					Original.Contatos.ToList().ForEach(x =>
					{
						Context.Entry(x).CurrentValues.SetValues(Entity.Contatos.FirstOrDefault(y => y.Id == x.Id));
					});
				}

				Save();
				return Entities;
			});
		}

		private void SaveUpdatedObject(Cliente Original, Cliente Updated)
		{
			var Principal = SerializeObject(Original);

			if (!Original.Equals(Updated))
			{
				var Entity = "Cliente";
				SaveChanges(Principal, Usuario, Original.Id, EntityName: Entity);
			}

			if (!Original.Endereco.Equals(Updated.Endereco))
			{
				var Entity = "Cliente.Endereço";
				var Alterado = SerializeObject(Original.Endereco);
				SaveChanges(Principal, Usuario, Original.Id, Alterado, Entity);
			}

			if (!Original.Endereco.Equals(Updated.Endereco))
			{
				var Entity = "Cliente.Contatos";
				var Alterado = SerializeObject(Original.Contatos);
				SaveChanges(Principal, Usuario, Original.Id, Alterado, Entity);
			}
		}
	}
}