using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LES.Utils
{
	public static class LinqExtensions
	{
		public static bool HasParameters(this IEnumerable<Filter> Filters)
		{
			return Filters.Any(x => x.Id != 0 || !(x.Value?.IsAllNull() == null ? true : x.Value.IsAllNull()));
		}

		public static IEnumerable<Filter> RemoveNonValuableParameters(this IEnumerable<Filter> Filters)
		{
			var NewList = new List<Filter>();

			Filters.ToList().ForEach(x => 
			{
				if (x.Id != 0 || !(x.Value?.IsAllNull() == null ? true : x.Value.IsAllNull()))
					NewList.Add(x);
			});

			return NewList;
		}

		public static bool ContainsNormalized(this IEnumerable<string> Itens, params string[] Search)
		{
			return Itens.Any(x => Search.Any(y => y.EqualsNormalized(x)));
		}

		public static T? Get<T>(this NameValueCollection Collection, string Parameter) where T : struct
		{
			var Value = Collection[Parameter];

			if (string.IsNullOrWhiteSpace(Value))
				return default(T?);

			return (T)Convert.ChangeType(Value, typeof(T));
		}
	}
}
