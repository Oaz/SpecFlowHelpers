using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace Oaz.SpecFlowHelpers
{
	public static class ObjectExtensions
	{
		public static void SetPropertyValue(this object obj, string propertyName, object propertyValue)
		{
			var type = obj.GetType();
			var property = type.GetProperty(propertyName);
			Tools.Check( property != null, "Unknown property {0} on type {1}", propertyName, type );
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
		
		internal static IEnumerable<KeyValuePair<string,object>> Properties(this object obj)
		{
            foreach (PropertyInfo property in obj.GetType().GetProperties())
            {
                yield return new KeyValuePair<string, object>( property.Name, property.GetValue(obj, null) );
            }
		}
		
		internal static IDictionary<string,string> PropertiesAsStringDictionary(this object obj)
		{
			var d = new Dictionary<string,string>();
			foreach(var pair in obj.PropertiesAsValuePairs())
				d[pair.Key] = pair.Value;
			return d;
		}

		internal static IEnumerable<KeyValuePair<string,string>> PropertiesAsValuePairs(this object obj)
		{
			if( obj is IEnumerable<KeyValuePair<string,string>> )
				return obj as IEnumerable<KeyValuePair<string,string>>;
			else if( obj is TechTalk.SpecFlow.Table )
				return (obj as TechTalk.SpecFlow.Table).ValuePairs();
			else
				return obj.Properties().Select(
				  pair => new KeyValuePair<string,string>(
				     pair.Key,
				     pair.Value.ToString()
				  )
				);
		}
		
		public static string Capitalize(this string s)
		{
			return char.ToUpper(s[0]) + s.Substring(1, s.Length - 1).ToLower();
		}
		
		public static string AsMethodName(this string s)
		{
			return string.Join("", s.Split(' ').Select(u=>u.Capitalize()).ToArray());
		}
	}
}

