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
	public class TituloController : Controller<Titulo>
	{
		public TituloController(IBusiness<Titulo> Bussiness) : base(Bussiness)
		{
		}

		[HttpPost, Route("api/Titulo/Filter")]
		public override Result<IEnumerable<Titulo>> Filter(params Filter[] Filters)
		{
			return base.Filter(Filters);
		}

		[HttpPost, Route("api/Titulo/AdicionarLivro/{IdTitulo}")]
		public Result<IEnumerable<Titulo>> AdicionarLivro(int IdTitulo, params Livro[] Livros)
		{
			try
			{
				var Result = Business.ExecuteCommand(Command.Select, new Filter { Id = IdTitulo });

				if (Result.Data == null)
					return Result;

				var Titulo = Result.Data.FirstOrDefault();

				Livros.ToList().ForEach(x => x.Titulo = Titulo);
				return Business.ExecuteCommand(Command.Insert, Livros);
			}
			catch (Exception ex)
			{
				return (Business as Business<Titulo>).SaveException<IEnumerable<Titulo>>(ex);
			}
		}
	}
}