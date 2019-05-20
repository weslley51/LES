using LES.Structure;
using LES.Utils;
using LES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Security;
using System.Data.Entity;
using LES.Business;

namespace LES.Controllers
{
	public class RelatorioController : Controller<Relatorio>
	{
		public RelatorioController(IBusiness<Relatorio> Bussiness) : base(Bussiness)
		{
		}

		[HttpPost, Route("api/Relatorio/GenerosPorMes")]
		public Result<IEnumerable<Relatorio>> GenerosPorMes(params Filter[] Filters)
		{
			return Business.Process(() =>
			{
				return (Business as RelatorioBusiness).GenerosPorMes(Filters);
			});
		}

		[HttpPost, Route("api/Relatorio/Filter")]
		public override Result<IEnumerable<Relatorio>> Filter(params Filter[] Filters)
		{
			return base.Filter(Filters);
		}		
	}
}