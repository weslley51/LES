using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LES.Models;
using LES.Data.Repositories;
using LES.Utils;
using LES.Models;
using LES.Structure;
using System.Data.Entity;
using Newtonsoft.Json;
using System.Data.Entity.Validation;

namespace LES.Data.Repositories
{
	public class UsuarioRepository : Repository<Usuario>
	{
		public UsuarioRepository(DbContext Context, Usuario Usuario) : base(Context, Usuario)
		{
		}

		protected override Usuario GetById(long id)
		{
			return Set.Include(x => x.Perfil).FirstOrDefault(x => x.Id == id);
		}

		protected override IEnumerable<Usuario> GetAll()
		{
			return Set.Include(x => x.Perfil);
		}

		protected override IEnumerable<Usuario> GetWithParameters(params Filter[] Filters)
		{
			var Usuarios = Set.Include(x => x.Perfil);
			var Normalizados = Filters.ToList();
			Normalizados.ForEach(x => { x.TrimAllStrings(); x.UpperCaseAll(); });

			if (Filters.Any(x => x.Property == "LOGADO"))
				Usuarios = Usuarios.Where(x => x.Login == HttpContext.Current.User.Identity.Name);
			else
			{
				if (Filters.Any(x => x.Property == "PERFIL"))
				{
					var Perfil = Convert.ToInt32(Filters.FirstOrDefault(f => f.Property == "PERFIL").Value);
					Usuarios = Usuarios.Where(x => x.Perfil.Id == Perfil);
				}

				if (Filters.Any(x => x.Property == "LOGIN"))
				{
					var Login = Filters.FirstOrDefault(f => f.Property == "LOGIN").Value.ToString();
					Usuarios = Usuarios.Where(x => x.Login == Login);
				}

				if (Filters.Any(x => x.Property == "SENHA"))
				{
					var Senha = Filters.FirstOrDefault(f => f.Property == "SENHA").Value.ToString();
					Usuarios = Usuarios.Where(x => x.Senha == Senha);
				}

				if (Filters.Any(x => x.Property == "NOME"))
				{
					var Nome = Filters.FirstOrDefault(f => f.Property == "NOME").Value.ToString();
					Usuarios = Usuarios.Where(x => x.Nome.Contains(Nome));
				}
			}

			return Usuarios;
		}

		public override IEnumerable<Usuario> Update(params Usuario[] Entities)
		{
			return Process(() =>
			{
				foreach (var Entity in Entities)
				{
					var Original = GetById(Entity.Id);
					var Json = JsonConvert.SerializeObject(Original, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
					SaveUpdatedObject(Original, Entity);
					Context.Entry(Original).CurrentValues.SetValues(Entity);
					Context.Entry(Original.Perfil).CurrentValues.SetValues(Entity.Perfil);
				}

				Save();
				return Entities;
			});
		}

		private void SaveUpdatedObject(Usuario Original, Usuario Updated)
		{
			var Entity = "Usuário";
			string Alterado = string.Empty;
			var Principal = JsonConvert.SerializeObject(Original, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore, PreserveReferencesHandling = PreserveReferencesHandling.None });

			if (!Original.Perfil.Equals(Updated.Perfil))
			{
				Entity += ".Perfil";
				Alterado = JsonConvert.SerializeObject(Original, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore, PreserveReferencesHandling = PreserveReferencesHandling.None });
			}

			SaveChanges(Principal, Usuario, Original.Id, Alterado, Entity);
		}
	}
}