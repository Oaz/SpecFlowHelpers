using System;
using TechTalk.SpecFlow;
using System.Collections.Generic;

namespace Oaz.SpecFlowHelpers
{
	public static class TableExtensions
	{
		internal static IEnumerable<IEnumerable<KeyValuePair<string,string>>> Values(this Table table)
		{
			foreach(var row in table.Rows)
				yield return row.Values();
		}
		
		internal static IEnumerable<KeyValuePair<string,string>> ValuePairs(this Table table)
		{
			foreach(var row in table.Rows)
				yield return new KeyValuePair<string, string>(row[0], row[1]);
		}
		
		internal static IEnumerable<KeyValuePair<string,string>> Values(this TableRow row)
		{
			foreach(var cell in row)
				yield return cell;
		}
		
		internal static void SetPropertyValue(object obj, string propertyName, string propertyValue, Type type)
		{
			var property = obj.GetType().GetProperty(propertyName);
			if( property == null )
				throw new ApplicationException(
				  string.Format("Unknown property {0} on type {1}", propertyName, type)
				);
			try
			{
				object newValue = Convert.ChangeType(propertyValue, property.PropertyType);
				property.SetValue(obj, newValue, null);
			}
			catch(Exception e)
			{
				throw new ApplicationException(
				  string.Format("Unsupported property type {0}", property.PropertyType),
				  e
				);
			}
		}
		
		public static T As<T>(this Table table) where T:class, new()
		{
			return PreventException(
			  () =>
			  {
				T obj = new T();
				foreach(var row in table.Rows)
				{
					SetPropertyValue(obj, row[0], row[1], typeof(T));
				}
				return obj;
			  }
			);
		}
		
		public static IEnumerable<T> AsEnumerable<T>(this Table table) where T:new()
		{
			return PreventException(
			  () => AsEnumerableImpl<T>(table)
			);
		}
		
		private static IEnumerable<T> AsEnumerableImpl<T>(Table table) where T:new()
		{
			foreach(var row in table.Rows)
			{
				T obj = new T();
				foreach(var field in table.Header)
				{
					SetPropertyValue(obj, field, row[field], typeof(T));
				}
				yield return obj;
			}
		}
		
		private static U PreventException<U>(Func<U> execute) where U:class
		{
			Instance.Is<Exception>(null);
			try
			{
				return execute();
			}
			catch(Exception e)
			{
				Instance.Is<Exception>(e);
				return null;
			}
		}
	}
}

