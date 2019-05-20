using LES.Structure;
using LES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using LES.Utils;
using System.Web.Http;

namespace LES.Controllers
{
	public class MultaController : Controller<Multa>
	{
		public MultaController(IBusiness<Multa> Bussiness) : base(Bussiness)
		{
		}

		[HttpPost, Route("api/Multa/Filter")]
		public override Result<IEnumerable<Multa>> Filter(params Filter[] Filters)
		{
			return base.Filter(Filters);
		}
	}
}