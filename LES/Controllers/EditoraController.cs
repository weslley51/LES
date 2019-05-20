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
	public class EditoraController : Controller<Editora>
	{
		public EditoraController(IBusiness<Editora> Bussiness) : base(Bussiness)
		{
		}

		[HttpPost, Route("api/Editora/Filter")]
		public override Result<IEnumerable<Editora>> Filter(params Filter[] Filters)
		{
			return base.Filter(Filters);
		}
	}
}