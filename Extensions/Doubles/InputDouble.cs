using System;
using Castle.Core.Interceptor;
using System.Collections.Generic;
namespace Oaz.SpecFlowHelpers.Doubles
{
	public class InputDouble<T> : Double<T> where T:class
	{
		public InputDouble ()
		{
			_returnValues = new Dictionary<string, object>();
		}
		
		private Dictionary<string,object> _returnValues;
		
		public void SetupGet(string propertyName, object propertyValue)
		{
			var property = typeof(T).GetProperty(propertyName);
			Tools.Check( property != null, "Unknown property {0} on type {1}", propertyName, typeof(T) );
			var typedValue = Convert.ChangeType(propertyValue, property.PropertyType);
			_returnValues["get_"+propertyName] = typedValue;
		}
		
		protected override void Execute (IInvocation invocation)
		{
			if(_returnValues.ContainsKey(invocation.Method.Name))
				invocation.ReturnValue = _returnValues[invocation.Method.Name];
		}
	}
}

