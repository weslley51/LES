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
	public class GeneroController : Controller<Genero>
	{
		public GeneroController(IBusiness<Genero> Bussiness) : base(Bussiness)
		{
		}

		[HttpPost, Route("api/Genero/Filter")]
		public override Result<IEnumerable<Genero>> Filter(params Filter[] Filters)
		{
			return base.Filter(Filters);
		}
	}
}