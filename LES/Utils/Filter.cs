using LES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LES.Utils
{
	public class Filter : Dominio
	{		
		public string Property { get; set; }
		public object Value { get; set; }

		public Filter()
		{

		}

		public Filter(Dominio Entity)
		{
			Id = Entity.Id;
		}
	}
}