using System;
using System.Collections.Generic;
using System.Linq;
using LES.Models;
using LES.Utils;
using LES.Structure;
using System.Data.Entity;

namespace LES.Data.Repositories
{
	public class MultaRepository : Repository<Multa>
	{
		public MultaRepository(DbContext Context, Usuario Usuario = null) : base(Context, Usuario)
		{
		}

		protected override Multa GetById(long id)
		{
			return Set.Include(x => x.Cliente).Include(x => x.UsuarioCadastro).FirstOrDefault(x => x.Id == id);
		}

		protected override IEnumerable<Multa> GetAll()
		{
			return Set.Include(x => x.Cliente).Include(x => x.UsuarioCadastro);
		}

		protected override IEnumerable<Multa> GetWithParameters(params Filter[] Filters)
		{
			var Multas = Set
							.Include(x => x.Cliente)
							.Include(x => x.UsuarioCadastro)
							.Include(x => x.Aluguel)
							.Include(x => x.Aluguel.Livro)
							.Include(x => x.Aluguel.Livro.Titulo);

			var Normalizados = Filters.ToList();
			Normalizados.ForEach(x => { x.TrimAllStrings(); x.UpperCaseAll(); });

			if (Filters.Any(x => x.Property == "CLIENTE"))
			{
				var Cliente = Convert.ToInt32(Filters.FirstOrDefault(f => f.Property == "CLIENTE").Value);
				Multas = Multas.Where(x => x.Cliente.Id == Cliente);
			}

			return Multas;
		}
	}
}