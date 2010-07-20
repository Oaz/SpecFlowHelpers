using System;
using TechTalk.SpecFlow;
using System.Collections.Generic;
using Moq;
using Moq.Language;
using Moq.Language.Flow;
using System.Reflection;
using System.Linq.Expressions;

namespace Oaz.SpecFlowHelpers.Moq
{
	public static class TableExtensions
	{
		public static T AsMock<T>(this Table table) where T:class
		{
			return Tools.PreventException(
			  () =>
			  {
				var mock = new Mock<T>(MockBehavior.Strict);
				foreach(var row in table.Rows)
				{
					var property = typeof(T).GetProperty(row[0]);
					if(property.PropertyType == typeof(string))
					{
						MockProperty<T,string>(mock, row[0], row[1]);
					}
					else if(property.PropertyType == typeof(int))
					{
						MockProperty<T,int>(mock, row[0], row[1]);
					}
					else if(property.PropertyType == typeof(double))
					{
						MockProperty<T,double>(mock, row[0], row[1]);
					}
					else if(property.PropertyType == typeof(DateTime))
					{
						MockProperty<T,DateTime>(mock, row[0], row[1]);
					}
				}
				return mock.Object;
			  }
			);
		}
		
		private static void MockProperty<T,P>(Mock<T> mock, string propertyName, string propertyValue) where T:class
		{
			var parameter = Expression.Parameter(typeof(T),"m");
			var propertySelector = Expression.Property(parameter,propertyName);
			var propertyTypedValue = Tools.ConvertTo<P>(propertyValue);
			var propertyGetter = Expression.Lambda<Func<T,P>>( propertySelector, parameter );
			mock.SetupGet(propertyGetter).Returns(propertyTypedValue);
		}
	}
}

