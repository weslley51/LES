using System;
using System.Collections.Generic;
using System.Linq;

namespace LES.Utils
{
	/// <summary>
	/// Objeto que contém o retorno das operações do Dominio, sejam elas erros e/ou uma lista de Entidades
	/// </summary>
	public class Result<T>
    {
        public T Data { get; set; }
		public List<Message> Messages { get; set; }
		
        public Result()
        {
            Messages = new List<Message>();
        }

		public Result(T Data)
		{
			this.Data = Data;
		}

		public Result(T Data, params Message[] Messages)
		{
			this.Data = Data;
			this.Messages = Messages?.ToList();
		}

		public Result(params Message[] Messages)
        {
			this.Messages = new List<Message>();

			if (Messages != null)
				this.Messages.AddRange(Messages);
		}

		public string ReturnMessages()
		{
			return string.Join(Environment.NewLine, Messages);
		}

		public void AddError(params Message[] Messages)
        {
            if (Messages == null || Messages.Length <= 0)
                return;

			if (this.Messages == null)
				this.Messages = new List<Message>();

            this.Messages.AddRange(Messages);
        }			

		public bool ContainsError()
		{
			return Messages != null && Messages.Count > 0;
		}
	}
}
