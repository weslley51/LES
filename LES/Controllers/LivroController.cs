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
	public class LivroController : Controller<Livro>
	{
		public LivroController(IBusiness<Livro> Bussiness) : base(Bussiness)
		{
		}

		[HttpPost, Route("api/Livro/Filter")]
		public override Result<IEnumerable<Livro>> Filter(params Filter[] Filters)
		{
			return base.Filter(Filters);
		}
	}
}