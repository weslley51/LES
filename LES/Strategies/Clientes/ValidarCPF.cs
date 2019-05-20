using LES.Structure;
using LES.Utils;
using LES.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LES.Strategies.Clientes
{
	[CommandType(Command.Insert, Command.Update)]
	public class ValidarCPF : PreStrategy<Cliente>
	{
		public ValidarCPF(DbContext Context) : base(Context)
		{
		}

		public override List<Message> Process(Cliente Entity)
		{
			if (Entity != null)
			{
				var CPF = Entity.CPF ?? string.Empty;
				var Mensagens = new List<Message>();

				CPF = CPF.Trim().Replace(".", "").Replace("-", "");

				if (CPF.Length != 11)
					return new List<Message> { new Message("O CPF deve conter 11 caracteres!") };

				if (CPF[9] != CalcularDigito(CPF.Substring(0, 10), new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 }) ||
					CPF[10] != CalcularDigito(CPF, new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 }))
					return new List<Message> { new Message("O CPF informado é inválido!") };

				return null;
			}
			return null;
		}

		private char CalcularDigito(string CPF, params int[] Multiplicadores)
		{
			var Soma = 0;
			var Resto = 0;

			for (int i = 0; i < CPF.Length - 1; i++)
				Soma += int.Parse(CPF[i].ToString()) * Multiplicadores[i];

			Resto = Soma % 11;

			if (Resto < 2)
				return '0';
			else
				return (11 - Resto).ToString().ToCharArray()[0];
		}
	}
}