using LES.Data;
using LES.Models;
using LES.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Web.Http;

namespace LES.Structure
{
	public abstract class Controller<T> : ApiController where T : Dominio, new()
	{
		protected IBusiness<T> Business { get; set; }

		public Controller(IBusiness<T> Bussiness)
		{
			this.Business = Bussiness;
		}

		[HttpGet]
		public virtual Result<IEnumerable<T>> Get(T Entity)
		{
			if (Entity == null)
				return Business.ExecuteCommand(Command.Select, new T[] { new T() });
			else
				return Business.ExecuteCommand(Command.Select, new T[] { Entity });
		}

		[HttpGet]
		public virtual Result<T> Get(int id)
		{
			var Result = Business.ExecuteCommand(Command.Select, new Filter[] { new Filter { Id = id } });
			return new Result<T>(Result.Data?.FirstOrDefault(), Result.Messages?.ToArray());
		}

		[HttpPost, Route("api/{controller}/Filter")]
		public virtual Result<IEnumerable<T>> Filter(params Filter[] Filters)
		{
			if (Filters == null || Filters.Length <= 0)
				return Business.ExecuteCommand(Command.Select, new T[] { new T() });
			else
				return Business.ExecuteCommand(Command.Select, Filters);
		}

		[HttpPost]
		public virtual Result<IEnumerable<T>> Post(params T[] Entities)
		{
			return Business.ExecuteCommand(Command.Insert, Entities);
		}

		[HttpPut]
		public virtual Result<IEnumerable<T>> Put(params T[] Entities)
		{
			return Business.ExecuteCommand(Command.Update, Entities);
		}

		[HttpDelete]
		public virtual Result<IEnumerable<T>> Delete(int Id)
		{
			var Result = Business.ExecuteCommand(Command.Select, new Filter { Id = Id });

			if (Result.Data == null)
				return Result;

			return Business.ExecuteCommand(Command.Delete, Result.Data.FirstOrDefault());
		}

		[HttpDelete]
		public virtual Result<IEnumerable<T>> Delete([FromUri]params long[] Ids)
		{
			var Result = Ids.Select(x => Business.ExecuteCommand(Command.Select, new Filter { Id = x }));

			if (Result.Any(x => x.Data == null))
				return new Result<IEnumerable<T>>(null, Result.SelectMany(x => x.Messages).ToArray());

			return Business.ExecuteCommand(Command.Delete, Result.SelectMany(x => x.Data).ToArray());
		}
	}
}
