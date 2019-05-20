using LES.Data.Repositories;
using LES.Models;
using LES.Structure;
using LES.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LES.Business
{
	public class RelatorioBusiness : Business<Relatorio>
	{
		public RelatorioBusiness(IRepository<Relatorio> Repository, IEnumerable<IStrategy<Relatorio>> Strategies, IEnumerable<IStrategy<Dominio>> DomainStrategies) : base(Repository, Strategies, DomainStrategies)
		{
		}

		public IEnumerable<Relatorio> GenerosPorMes(params Filter[] Filters)
		{
			return (Repository as RelatorioRepository).GenerosPorMes(Filters);
		}
	}
}