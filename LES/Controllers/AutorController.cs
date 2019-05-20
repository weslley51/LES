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
	public class AutorController : Controller<Autor>
	{
		public AutorController(IBusiness<Autor> Bussiness) : base(Bussiness)
		{
		}

		[HttpPost, Route("api/Autor/Filter")]
		public override Result<IEnumerable<Autor>> Filter(params Filter[] Filters)
		{
			return base.Filter(Filters);
		}
	}
}