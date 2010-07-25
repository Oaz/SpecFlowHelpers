using System;
using Moq;
using System.Linq.Expressions;
using System.Reflection;

namespace Oaz.SpecFlowHelpers.Moq
{
	public static class MockExtensions
	{
		internal class PropertyMocker<T,P> where T:class
		{		
			public static void SetupGet(Mock<T> mock, string propertyName, object propertyValue)
			{
				var parameter = Expression.Parameter(typeof(T),"m");
				var propertySelector = Expression.Property(parameter,propertyName);
				var propertyTypedValue = Tools.ConvertTo<P>(propertyValue);
				var propertyGetter = Expression.Lambda<Func<T,P>>( propertySelector, parameter );
				mock.SetupGet(propertyGetter).Returns(propertyTypedValue);
			}
		}
		
		public static void SetupGet<T>(this Mock<T> mock, string propertyName, object propertyValue) where T:class
		{
			var property = typeof(T).GetProperty(propertyName);
			Tools.Check( property != null, "Unknown property {0} on type {1}", propertyName, typeof(T) );
			var type = typeof(PropertyMocker<,>);
			var pmType = type.MakeGenericType(typeof(T), property.PropertyType);
			var setupGet = pmType.GetMethod("SetupGet");
			object[] args = {mock,propertyName,propertyValue};
			setupGet.Invoke(null, args);
		}
	}
}

