using System;
using TechTalk.SpecFlow;
using System.Collections.Generic;
using System.Collections;

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
		
		public static IDictionary<string,object> AsPropertiesDictionary<T>(this Table table)
		{
			return table.ValuePairs().PropertiesDictionary<T>();
		}
		
		internal static IEnumerable<KeyValuePair<string,string>> Values(this TableRow row)
		{
			foreach(var cell in row)
				yield return cell;
		}
		
		public static IDictionary<string,object> AsPropertiesDictionary<T>(this TableRow row)
		{
			return row.Values().PropertiesDictionary<T>();
		}
		
		internal static IDictionary<string,object> PropertiesDictionary<T>(this IEnumerable<KeyValuePair<string,string>> values)
		{
			var propertyValues = new Dictionary<string,object>();
			foreach(var pair in values)
			{
				var propertyName = pair.Key;
				var property = typeof(T).GetProperty(propertyName);
				Tools.Check( property != null, "Unknown property {0} on type {1}", propertyName, typeof(T) );
				var propertyValue = pair.Value;
				var typedValue = Convert.ChangeType(propertyValue, property.PropertyType);
				propertyValues[propertyName] = typedValue;
			}
			return propertyValues;
		}
		
		public static T As<T>(this Table table) where T:class, new()
		{
			return Tools.HandleExceptionInstance(
			  () =>
			  {
				T obj = new T();
				foreach(var row in table.Rows)
					obj.SetPropertyValue(row[0], row[1]);
				return obj;
			  }
			);
		}
		
		public static IEnumerable AsEnumerable(this Table table)
		{
			return Tools.HandleExceptionInstance(
			  () => AsEnumerableImpl(table)
			);
		}
		
		private static IEnumerable AsEnumerableImpl(Table table)
		{
			foreach(var row in table.Rows)
			{
				var obj = new List<KeyValuePair<string,string>>();
				foreach(var field in table.Header)
					obj.Add( new KeyValuePair<string,string>( field, row[field] ) );
				yield return obj;
			}
		}
		
		public static IEnumerable<T> AsEnumerable<T>(this Table table) where T:new()
		{
			return Tools.HandleExceptionInstance(
			  () => AsEnumerableImpl<T>(table)
			);
		}
		
		private static IEnumerable<T> AsEnumerableImpl<T>(Table table) where T:new()
		{
			foreach(var row in table.Rows)
			{
				T obj = new T();
				foreach(var field in table.Header)
					obj.SetPropertyValue(field, row[field]);
				yield return obj;
			}
		}
		
	}
}

