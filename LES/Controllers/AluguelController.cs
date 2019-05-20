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
	public class AluguelController : Controller<Aluguel>
	{
		public AluguelController(IBusiness<Aluguel> Bussiness) : base(Bussiness)
		{
		}

		[HttpPost, Route("api/Aluguel/Filter")]
		public override Result<IEnumerable<Aluguel>> Filter(params Filter[] Filters)
		{
			return base.Filter(Filters);
		}
	}
}