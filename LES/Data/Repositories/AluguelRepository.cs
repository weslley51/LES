using System;
using System.Collections.Generic;
using System.Linq;
using LES.Models;
using LES.Utils;
using LES.Structure;
using System.Data.Entity;

namespace LES.Data.Repositories
{
	public class AluguelRepository : Repository<Aluguel>
	{
		public AluguelRepository(DbContext Context, Usuario Usuario = null) : base(Context, Usuario)
		{
		}

		protected override IEnumerable<Aluguel> GetActiveOnly()
		{
			var Retorno = base.GetActiveOnly();

			if (Usuario.Unidade != null && Usuario.Unidade.Id != 0)
				return Retorno.Where(x => x.Unidade.Id == Usuario.Unidade.Id);

			return Retorno;
		}

		protected override Aluguel GetById(long id)
		{
			return Set.Include(x => x.Livro).Include(x => x.Livro.Titulo).Include(x => x.Cliente).FirstOrDefault(x => x.Id == id);
		}

		protected override IEnumerable<Aluguel> GetAll()
		{
			return Set.Include(x => x.Livro).Include(x => x.Livro.Titulo).Include(x => x.Cliente);
		}

		protected override IEnumerable<Aluguel> GetWithParameters(params Filter[] Filters)
		{
			var Alugueis = Set.Include(x => x.Livro).Include(x => x.Livro.Titulo)
								.Include(x => x.Livro.Titulo.Editora).Include(x => x.Cliente)
								.Where(x => x.DataDevolucao.HasValue == false);

			var Normalizados = Filters.ToList();
			Normalizados.ForEach(x => { x.TrimAllStrings(); x.UpperCaseAll(); });

			if (Filters.Any(x => x.Property == "ATRASADO"))
			{
				var Atrasado = Convert.ToBoolean(Filters.FirstOrDefault(f => f.Property == "ATRASADO").Value);

				if (Atrasado)
					Alugueis = Alugueis.Where(x => x.DataPrevistaDevolucao < DateTime.Now && x.DataDevolucao.HasValue == false);
				else
					Alugueis = Alugueis.Where(x => x.DataPrevistaDevolucao >= DateTime.Now && x.DataDevolucao.HasValue == false);
			}

			if (Filters.Any(x => x.Property == "EDITORA"))
			{
				var Editora = Convert.ToInt32(Filters.FirstOrDefault(f => f.Property == "EDITORA").Value);
				Alugueis = Alugueis.Where(x => x.Livro.Titulo.Editora.Id == Editora);
			}

			if (Filters.Any(x => x.Property == "CLIENTE"))
			{
				var Nome = Filters.FirstOrDefault(f => f.Property == "CLIENTE").Value.ToString();
				Alugueis = Alugueis.Where(x => x.Cliente.NomeCompleto.Contains(Nome));
			}

			if (Filters.Any(x => x.Property == "NOME"))
			{
				var Nome = Filters.FirstOrDefault(f => f.Property == "NOME").Value.ToString().ToUpper();
				Alugueis = Alugueis.Where(x => x.Livro.Titulo.Nome.ToUpper().Contains(Nome));
			}

			if (Filters.Any(x => x.Property == "CPF"))
			{
				var CPF = Filters.FirstOrDefault(f => f.Property == "CPF").Value.ToString().ToUpper();
				Alugueis = Alugueis.Where(x => x.Cliente.CPF.ToUpper().Contains(CPF));
			}

			if (Filters.Any(x => x.Property == "CODIGO"))
			{
				var Nome = Filters.FirstOrDefault(f => f.Property == "CODIGO").Value.ToString();
				Alugueis = Alugueis.Where(x => x.Livro.CodigoBiblioteca.Contains(Nome));
			}		

			return Alugueis;
		}

		public override IEnumerable<Aluguel> Update(params Aluguel[] Entities)
		{
			return Process(() =>
			{
				foreach (var Entity in Entities)
				{
					var Original = GetById(Entity.Id);
					SaveUpdatedObject(Original, Entity);
					Context.Entry(Original).CurrentValues.SetValues(Entity);					
				}

				Save();
				return Entities;
			});
		}

		private void SaveUpdatedObject(Aluguel Original, Aluguel Updated)
		{
			var Entity = "Aluguel";
			var Principal = SerializeObject(Original);
			SaveChanges(Principal, Usuario, Original.Id, EntityName: Entity);
		}
	}
}