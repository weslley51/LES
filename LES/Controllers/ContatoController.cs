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
	public class ContatoController : Controller<Contato>
	{
		public ContatoController(IBusiness<Contato> Bussiness) : base(Bussiness)
		{
		}

		public override Result<IEnumerable<Contato>> Post(params Contato[] Entities)
		{
			return new Result<IEnumerable<Contato>>(new Message("Contato não pode ser salvo diretamente !"));
		}

		[HttpPost, Route("api/Contato/Filter")]
		public override Result<IEnumerable<Contato>> Filter(params Filter[] Filters)
		{
			return base.Filter(Filters);
		}
	}
}