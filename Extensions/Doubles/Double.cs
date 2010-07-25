using System;
using System.Linq;
using Castle.DynamicProxy;
using Castle.Core.Interceptor;
using System.Collections.Generic;

namespace Oaz.SpecFlowHelpers.Doubles
{
	public class DoubleProxy<T> where T:class
	{
		internal Double<T> Double { get; set; }
	}
	
	public class Double<T> : IInterceptor where T:class
	{
		private static readonly ProxyGenerator _generator = new ProxyGenerator();

		public Double()
		{
			var options = new ProxyGenerationOptions();
			options.BaseTypeForInterfaceProxy = typeof(DoubleProxy<T>);

			Object = _generator.CreateInterfaceProxyWithoutTarget<T>(options, this);
			Proxy = (DoubleProxy<T>) (object) Object;
			Proxy.Double = this;
			
			Commands = new List<Command<T>>();
		}
		
		internal DoubleProxy<T> Proxy { get; private set; }
		public T Object { get; private set; }
		public IList<Command<T>> Commands { get; private set; }
	
		public static Double<T> Get(T obj)
		{
			var proxy = (DoubleProxy<T>) (object) obj;
			return proxy.Double;
		}
		
		public void Intercept (IInvocation invocation)
		{
			var cmd = new Command<T>(invocation.Method, invocation.Arguments.AsEnumerable());
			Commands.Add(cmd);
		}
	}
}

