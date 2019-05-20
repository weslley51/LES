using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LES.Utils
{
	public enum Command
	{
		Delete,
		Insert,
		Select,
		Update
	};

	public enum StrategyType
	{
		Pre,
		Post
	}

	public enum Sexo
	{
		Feminino = 'F',
		Masculino = 'M'
	}

	public enum TipoMovimentacao
	{
		Saida = 'S',
		Entrada = 'E',
		Transferencia = 'T'
	}

	public enum TipoMulta
	{
		Pagamento = 'P',
		Atraso = 'A'			
	}
}