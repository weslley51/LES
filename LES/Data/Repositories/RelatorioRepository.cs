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
using System.Data.Entity.Core.Objects;

namespace LES.Data.Repositories
{
	public class RelatorioRepository : Repository<Relatorio>
	{
		public RelatorioRepository(DbContext Context, Usuario Usuario = null) : base(Context, Usuario)
		{
		}

		protected override Relatorio GetById(long id)
		{
			return EmptyList().FirstOrDefault();
		}

		protected override IEnumerable<Relatorio> GetAll()
		{
			return EmptyList();
		}
		
		public override IEnumerable<Relatorio> Update(params Relatorio[] Entities)
		{
			return EmptyList();
		}

		public override IEnumerable<Relatorio> Insert(params Relatorio[] Entities)
		{
			return EmptyList();
		}

		protected override IEnumerable<Relatorio> GetWithParameters(params Filter[] Filters)
		{
			return EmptyList();
		}

		public IEnumerable<Relatorio> GenerosPorMes(params Filter[] Filters)
		{
			var Parameters = Filters?.ToList()?.Cast<Filter>() ?? new List<Filter>();

			if (Parameters?.Count() <= 0 || !Parameters.HasParameters())
				return new List<Relatorio>();
			
			var Normalizados = Filters.ToList();
			Normalizados.ForEach(x => { x.TrimAllStrings(); x.UpperCaseAll(); });

			var Alugueis = Context.Set<Aluguel>().Include(x => x.UsuarioCadastro);

			if (Filters.Any(x => x.Property == "INICIO"))
			{
				var Data = Convert.ToDateTime(Filters.FirstOrDefault(f => f.Property == "INICIO").Value);
				Alugueis = Alugueis.Where(x => EntityFunctions.TruncateTime(x.DataCadastro.Value) >= Data.Date);
			}

			if (Filters.Any(x => x.Property == "FIM"))
			{
				var Data = Convert.ToDateTime(Filters.FirstOrDefault(f => f.Property == "FIM").Value);
				Data = new DateTime(Data.Year, Data.Month, DateTime.DaysInMonth(Data.Year, Data.Month));
				Alugueis = Alugueis.Where(x => EntityFunctions.TruncateTime(x.DataCadastro.Value) <= Data.Date);
			}

			var Retorno = Alugueis
							.Select(x => new
							{
								Ano = x.DataCadastro.Value.Year,
								Mes = x.DataCadastro.Value.Month,
								Genero = x.Livro.Titulo.Generos.FirstOrDefault()
							})
							.ToList();

			var Meses = Retorno
							.GroupBy(x => new { x.Mes, x.Ano })
							.OrderBy(x => x.Key.Ano)
							.ThenBy(x => x.Key.Mes)
							.ThenBy(x => x.Select(y => y.Genero));
						
			var Relatorio = new Relatorio
			{
				Titulos = Meses.Select(x => string.Concat(x.Key.Mes, "-", x.Key.Ano)).ToList(),
				Valores = new Dictionary<string, List<double>>()
			};
			
			Context.Set<Genero>().ToList().ForEach(x =>
			{
				Relatorio.Valores.Add(x.Nome, new List<double>());
				for (var i = 0; i < Meses.Count(); i++)
					Relatorio.Valores[x.Nome].Add(0);
			});

			var Contador = 0;
			foreach (var item in Meses)
			{
				foreach (var genero in item.GroupBy(x => x.Genero))
					Relatorio.Valores[genero.Key.Nome][Contador] = genero.Count();
				Contador++;
			}
			
			return new List<Relatorio> { Relatorio };
		}
	}
}