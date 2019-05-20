﻿using LES.Structure;
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
	public class UnidadeController : Controller<Unidade>
	{
		public UnidadeController(IBusiness<Unidade> Bussiness) : base(Bussiness)
		{
		}

		[HttpPost, Route("api/Unidade/Filter")]
		public override Result<IEnumerable<Unidade>> Filter(params Filter[] Filters)
		{
			return base.Filter(Filters);
		}
	}
}