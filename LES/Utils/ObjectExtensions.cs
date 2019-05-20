using LES.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LES.Utils
{
	public static class ObjectExtensions
	{
		public static bool IsAllNull(this object Object, params string[] Ignore)
		{
			if (Object.GetType().IsValueType || Object.GetType() == typeof(string))
				return Object.GetType() != typeof(bool) ? IsDefaultValue(Object, Object.GetType()) : false;

			var Properties = Object.GetType().GetProperties().Where(p => !Ignore.ContainsNormalized(p.Name));

			foreach (var Property in Properties)
			{
				if (IsDefaultValue(Property.GetValue(Object), Property.PropertyType));
					return false;
			}

			return true;
		}

		public static bool IsDefaultValue(object Value, Type Type)
		{
			var Numerics = new Type[] { typeof(int), typeof(short), typeof(long), typeof(decimal), typeof(double), typeof(float) };

			if (Numerics.Contains(Type))
			{
				if (new Type[] { typeof(int), typeof(short), typeof(long) }.Contains(Type) && (long)Value != 0)
					return false;
				else if (new Type[] { typeof(decimal), typeof(double), typeof(float) }.Contains(Type) && (decimal)Value != 0m)
					return false;
			}
			else if (new Type[] { typeof(string) }.Contains(Type))
			{
				if (!string.IsNullOrWhiteSpace((string)Value))
					return false;
			}
			else if (Type == typeof(DateTime))
			{
				return (DateTime)Value == DateTime.MinValue;
			}

			return true;
		}

		public static void TrimAllStrings(this object Objeto, params string[] Ignorar)
		{
			var Tipo = Objeto.GetType();

			foreach (var Property in Tipo.GetProperties().Where(x => x.PropertyType == typeof(string) && !Ignorar.Contains(x.Name)))
			{
				var Value = Property.GetValue(Objeto)?.ToString();

				if (Value == null)
					continue;

				Value = Value.TrimEnd().TrimStart();
				Tipo.GetProperty(Property.Name).SetValue(Objeto, Value);
			}
		}

		public static void UpperCaseAll(this object Objeto, params string[] Ignorar)
		{
			var Tipo = Objeto.GetType();

			foreach (var Property in Tipo.GetProperties().Where(x => x.PropertyType == typeof(string) && !Ignorar.Contains(x.Name)))
			{
				var Value = Property.GetValue(Objeto)?.ToString();

				if (Value == null)
					continue;

				Value = Value.ToUpper();
				Tipo.GetProperty(Property.Name).SetValue(Objeto, Value);
			}
		}
	}
}