using System.Collections.Generic;
using System.Linq;
using LES.Models;
using System.Data.Entity;
using System;
using System.Data.SqlClient;
using Newtonsoft.Json;
using LES.Utils;
using System.Collections;

namespace LES.Structure
{
	public interface IRepository<T> where T : Dominio
	{
		IEnumerable<T> Insert(params T[] Entities);
		IEnumerable<T> Update(params T[] Entities);
		IEnumerable<T> Delete(params T[] Entities);
		IEnumerable<T> Get(params Dominio[] Parameters);
	}

	public class Repository<T> : IRepository<T> where T : Dominio, new()
	{
		protected DbSet<T> Set { get; set; }
		protected Usuario Usuario { get; set; }
		public DbContext Context { get; private set; }

		public Repository(DbContext Context, Usuario Usuario = null)
		{
			this.Context = Context;
			Set = Context.Set<T>();
			this.Context.Database.Log = (x => System.Diagnostics.Debug.WriteLine(x));
			this.Usuario = Usuario == null || Usuario.Id == 0 ? new Usuario { Id = 1 } : Usuario;
		}

		public virtual IEnumerable<T> Insert(params T[] Entities)
		{
			return Process(() =>
			{
				Set.AddRange(Entities);
				Save();

				return Entities;
			});
		}

		public virtual IEnumerable<T> Update(params T[] Entities)
		{
			return Process(() =>
			{
				foreach (var Entity in Entities)
				{
					var Original = GetById(Entity.Id);
					var Json = JsonConvert.SerializeObject(Original, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
					SaveChanges(Json, Usuario, Original.Id);
					Context.Entry(Original).CurrentValues.SetValues(Entity);
				}

				Save();
				return Entities;
			});
		}

		public virtual IEnumerable<T> Delete(params T[] Entities)
		{
			return Process(() =>
			{
				foreach (var Id in Entities.Select(x => x.Id))
				{
					var Entity = GetById(Id);
					Set.Remove(Entity);
					Save();
				}

				return new List<T>();
			});
		}

		public virtual IEnumerable<T> Get(params Dominio[] Parameters)
		{
			return Process(() =>
			{
				if (Parameters[0] is T)
				{
					return GetActiveOnly();
				}
				else
				{
					var Filters = Parameters.ToList().Cast<Filter>();

					if (Filters?.Count() <= 0 || !Filters.HasParameters())
						return new List<T>();
					else if (Filters.Any(x => x.Id != 0))
						return new List<T> { GetById(Filters.First(x => x.Id != 0).Id) };
					else
					{
						Filters = Filters.RemoveNonValuableParameters();
						return GetWithParameters(Filters.ToArray());
					}
				}
			});
		}

		protected virtual T GetById(long id)
		{
			return Set.Include(x => x.UsuarioCadastro).FirstOrDefault(x => x.Id == id);
		}

		protected virtual IEnumerable<T> GetAll()
		{
			return Set.Include(x => x.UsuarioCadastro);
		}

		protected virtual IEnumerable<T> GetActiveOnly()
		{
			return Set.Include(x => x.UsuarioCadastro).Where(x => x.Ativo == true);
		}

		protected virtual IEnumerable<T> GetWithParameters(params Filter[] Filters)
		{
			var Normalizados = Filters.ToList();
			Normalizados.ForEach(x => { x.TrimAllStrings(); x.UpperCaseAll(); });

			if (!Filters.Any(x => x.Property.EqualsNormalized("ATIVO")))
				return GetActiveOnly();
			else
			{
				var Ativo = Convert.ToBoolean(Filters.FirstOrDefault(f => f.Property == "ATIVO").Value);
				return Set.Include(x => x.UsuarioCadastro).Where(x => x.Ativo == Ativo);
			}
		}
		
		protected IEnumerable<T> EmptyList()
		{
			return Enumerable.Empty<T>();
		}

		protected virtual U Process<U>(Func<U> Function)
		{
			using (var Transaction = Context.Database.BeginTransaction())
			{
				try
				{
					var FunctionReturn = Function();
					Context.Database.CurrentTransaction.Commit();
					return FunctionReturn;
				}
				catch (Exception ex)
				{
					Context.Database.CurrentTransaction.Rollback();
					throw ex;
				}
			}
		}

		protected virtual void Save()
		{
			Context.SaveChanges();
		}

		protected virtual string SerializeObject(object Object)
		{
			return JsonConvert.SerializeObject(Object, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore, PreserveReferencesHandling = PreserveReferencesHandling.None });
		}

		public virtual void SaveChanges(string Principal, Usuario User, long EntidadeId, string Updated = null, string EntityName = null)
		{
			Context.Set<LogAlteracao>().Add(new LogAlteracao
			{
				EntidadeId = EntidadeId,
				Ativo = true,
				ObjetoAlterado = Updated ?? Principal,
				ObjetoPrincipal = Principal,
				DataCadastro = DateTime.Now,
				UsuarioCadastro = Context.Set<Usuario>().First(x => x.Id == User.Id),
				NomeEntidade = EntityName ?? typeof(T).Name,
			});
		}

		public string SaveException(Exception exception)
		{
			try
			{
				var Log = new LogErro
				{
					Ativo = true,
					DataCadastro = DateTime.Now,
					MensagemPrincipal = exception.Message,
					OutrasMensagens = exception.StackTrace,
					UsuarioCadastro = Context.Set<Usuario>().First(x => x.Id == Usuario.Id),
				};

				Log = new Repository<LogErro>(Context).Insert(Log).FirstOrDefault();

				return $"Não foi possivel processar a sua requisição. Por favor informe o código \"{Log.Id}\" ao administrador do sistema.";
			}
			catch (Exception ex)
			{
				return $"FALHA GERAL! Por favor informe ao administrador o mais rápido o possivel: \"{exception.Message} || {ex.Message}\"";
			}
		}
	}
}