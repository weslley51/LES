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

namespace LES.Controllers
{
	public class UsuarioController : Controller<Usuario>
	{
		public UsuarioController(IBusiness<Usuario> Bussiness) : base(Bussiness)
		{
		}

		[HttpPost, Route("api/Usuario/Filter")]
		public override Result<IEnumerable<Usuario>> Filter(params Filter[] Filters)
		{
			return base.Filter(Filters);
		}

		[HttpPost, Route("api/Usuario/Logar")]
		public Result<Usuario> Logar(Usuario Usuario)
		{
			var MD5 = new MD5Manipulator();
			Usuario.Senha = MD5.Generate(Usuario.Senha);		

			var Retorno = Business.ExecuteCommand(Command.Select, new List<Filter>
			{
				new Filter { Property = "Login", Value = Usuario.Login },
				new Filter { Property = "Senha", Value = Usuario.Senha }
			}.ToArray());

			if (!Retorno.ContainsError() && Retorno.Data.Count() == 1)
				FormsAuthentication.SetAuthCookie(Usuario.Login, true);

			return new Result<Usuario>(Retorno.Data?.FirstOrDefault(), Retorno.Messages?.ToArray());
		}

		[HttpGet, Route("api/Usuario/Deslogar")]
		public void Deslogar()
		{
			FormsAuthentication.SignOut();
		}
	}
}