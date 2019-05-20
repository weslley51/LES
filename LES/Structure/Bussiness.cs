using LES.Models;
using LES.Structure;
using LES.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LES.Structure
{
	public interface IBusiness<T> where T : Dominio
	{
		Result<U> Process<U>(Func<U> Function);
		Result<IEnumerable<T>> ExecuteCommand(Command Command, params Dominio[] Entities);
		IEnumerable<Message> ExecuteStrategies<U>(Command Command, T Entity) where U : IStrategy<T>;
		IEnumerable<Message> ExecuteDomainStrategies<U>(Command Command, T Entity) where U : IStrategy<Dominio>;
	}

	/// <summary>
	/// Objeto que realiza as operações de CRUD baseadas na regras de negócio
	/// </summary>
	public class Business<T> : IBusiness<T> where T : Dominio, new()
	{
		protected IRepository<T> Repository { get; set; }
		protected IEnumerable<IStrategy<T>> Strategies { get; set; }
		protected IEnumerable<IStrategy<Dominio>> DomainStrategies { get; set; }

		public Business(IRepository<T> Repository, IEnumerable<IStrategy<T>> Strategies, IEnumerable<IStrategy<Dominio>> DomainStrategies)
		{
			this.Repository = Repository;
			this.Strategies = Strategies;
			this.DomainStrategies = DomainStrategies;
		}

		public virtual Result<IEnumerable<T>> ExecuteCommand(Command Command, params Dominio[] Entities)
		{
			var Return = Process(() =>
			{
				var ValidEntities = new List<T>();
				var Result = new Result<IEnumerable<T>>();

				if (Command == Command.Select)
					Result.Data = Repository.Get(Entities)?.ToArray();				
				else
				{
					var ConvertedEntities = Entities.Cast<T>();
					foreach (var Entity in ConvertedEntities)
					{
						Result.AddError(ExecuteDomainStrategies<PreStrategy<Dominio>>(Command, Entity)?.ToArray());
						Result.AddError(ExecuteStrategies<PreStrategy<T>>(Command, Entity).ToArray());

						if (Result.ContainsError())
							continue;

						ValidEntities.Add(Entity);
					}

					if (ValidEntities.Count > 0)
					{
						switch (Command)
						{
							case Command.Delete:
								Result.Data = Repository.Update(ValidEntities.ToArray());
								break;
							case Command.Insert:
								Result.Data = Repository.Insert(ValidEntities.ToArray());
								break;
							case Command.Update:
								Result.Data = Repository.Update(ValidEntities.ToArray());
								break;
							default:
								break;
						}

						foreach (var Entity in ConvertedEntities)
						{
							Result.AddError(ExecuteDomainStrategies<PostStrategy<Dominio>>(Command, Entity).ToArray());
							Result.AddError(ExecuteStrategies<PostStrategy<T>>(Command, Entity).ToArray());
						}
					}
				}
				Result.Messages = Result.Messages?.Count > 0 ? Result.Messages : null;
				return Result;
			});

			return Return.Data ?? new Result<IEnumerable<T>>(Return.Messages.ToArray());
		}

		public virtual Result<U> Process<U>(Func<U> Function)
		{
			try
			{
				return new Result<U>(Function());
			}
			catch (DbEntityValidationException ex)
			{
				var ErrorMessages = new List<Message> { new Message("Erro ao tentar validar as informações!") };

				foreach (var EntityError in ex.EntityValidationErrors)
				{
					foreach (var Error in EntityError.ValidationErrors)
					{
						ErrorMessages.Add(new Message($"Propriedade: {Error.PropertyName} | Erro: {Error.ErrorMessage}"));
					}
				}

				return new Result<U>(ErrorMessages.ToArray());
			}
			catch (Exception ex)
			{
				return SaveException<U>(ex);
			}
		}

		public virtual IEnumerable<Message> ExecuteStrategies<U>(Command Command, T Entity) where U : IStrategy<T>
		{
			var Messages = new List<Message>();

			foreach (var Strategy in Strategies?.OfType<U>().Where(x => (x.GetType().GetCustomAttributes(typeof(CommandType), false).First() as CommandType).Commands.Contains(Command)))
			{
				var Return = Strategy.Process(Entity);

				if (Return != null &&  Return.Count > 0)
					Messages.AddRange(Return);
			}

			return Messages;
		}

		public virtual IEnumerable<Message> ExecuteDomainStrategies<U>(Command Command, T Entity) where U : IStrategy<Dominio>
		{
			var Messages = new List<Message>();

			foreach (var Strategy in DomainStrategies?.OfType<U>().Where(x => (x.GetType().GetCustomAttributes(typeof(CommandType), false).First() as CommandType).Commands.Contains(Command)))
			{
				Dominio Dominio = Entity;
				var Return = Strategy.Process(Dominio);

				if (Return != null && Return.Count > 0)
					Messages.AddRange(Return);
			}

			return Messages;
		}

		public virtual Result<U> SaveException<U>(Exception ex)
		{
			var Message = (Repository as Repository<T>).SaveException(ex);
			return new Result<U>(new Message(Message));
		}
	}
}