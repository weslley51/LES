using LES.Structure;
using LES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Http;
using LES.Utils;

namespace LES.Controllers
{
	public class ClienteController : Controller<Cliente>
	{
		public ClienteController(IBusiness<Cliente> Bussiness) : base(Bussiness)
		{
		}

		[HttpPost, Route("api/Cliente/Filter")]
		public override Result<IEnumerable<Cliente>> Filter(params Filter[] Filters)
		{
			return base.Filter(Filters);
		}

		[HttpPost, Route("api/Cliente/AdicionarContato/{IdCliente}")]
		public Result<IEnumerable<Cliente>> AdicionarContato(int IdCliente, params Contato[] Contatos)
		{
			try
			{
				var Result = Business.ExecuteCommand(Command.Select, new Filter { Id = IdCliente });

				if (Result.Data == null)
					return Result;

				var Cliente = Result.Data.FirstOrDefault();
				var ListaContatos = Cliente?.Contatos?.ToList() ?? new List<Contato>();
				ListaContatos.AddRange(Contatos);
				Cliente.Contatos = ListaContatos;
				return Business.ExecuteCommand(Command.Update, Cliente);
			}
			catch (Exception ex)
			{
				return (Business as Business<Cliente>).SaveException<IEnumerable<Cliente>>(ex);
			}
		}
	}
}