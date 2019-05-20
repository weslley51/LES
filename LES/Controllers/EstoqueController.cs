using LES.Structure;
using LES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LES.Utils;
using System.Web.Http;

namespace LES.Controllers
{
	public class EstoqueController : Controller<Estoque>
	{
		public EstoqueController(IBusiness<Estoque> Bussiness) : base(Bussiness)
		{
		}

		[HttpPost, Route("api/Estoque/Filter")]
		public override Result<IEnumerable<Estoque>> Filter(params Filter[] Filters)
		{
			return base.Filter(Filters);
		}
	}
}