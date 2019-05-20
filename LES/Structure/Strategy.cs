using LES.Models;
using LES.Utils;
using System.Data.Entity;
using System.Collections.Generic;

namespace LES.Structure
{
	/// <summary>
	/// Interface de uma Strategy do Dominio
	/// </summary>
	public interface IStrategy<T> where T : Dominio
	{
		/// <summary>
		/// Realiza o processamento da Strategy
		/// </summary>
		/// <param name="Entity">Entidade a qual será realizada a operação</param>        
		/// <returns>Resultado da Operação</returns>
		List<Message> Process(T Entity);
	}
		
	public abstract class Strategy<T> : IStrategy<T> where T : Dominio, new()
	{		
		protected DbContext Context { get; set; }
		protected IRepository<T> Repository { get; set; }

		public Strategy(DbContext Context)
		{
            this.Context = Context;
			Repository = new Repository<T>(Context);
		}

		public abstract List<Message> Process(T Entity);
	}

    public abstract class PreStrategy <T>: Strategy<T> where T : Dominio, new()
	{
        public PreStrategy(DbContext Context) : base(Context)
        {
        }
    }

    public abstract class PostStrategy<T> : Strategy<T> where T : Dominio, new()
	{
        public PostStrategy(DbContext Context) : base(Context)
        {
        }
    }
}
