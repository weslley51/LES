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
	public class EstadoController : Controller<Estado>
	{
		public EstadoController(IBusiness<Estado> Bussiness) : base(Bussiness)
		{
		}

		[HttpPost, Route("api/Estado/Filter")]
		public override Result<IEnumerable<Estado>> Filter(params Filter[] Filters)
		{
			return base.Filter(Filters);
		}
	}
}