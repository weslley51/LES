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
	public class PerfilController : Controller<Perfil>
	{
		public PerfilController(IBusiness<Perfil> Bussiness) : base(Bussiness)
		{
		}

		[HttpPost, Route("api/Perfil/Filter")]
		public override Result<IEnumerable<Perfil>> Filter(params Filter[] Filters)
		{
			return base.Filter(Filters);
		}
	}
}