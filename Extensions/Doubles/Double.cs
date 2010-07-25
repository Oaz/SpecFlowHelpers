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
	
	public abstract class Double<T> : IInterceptor where T:class
	{
		private static readonly ProxyGenerator _generator = new ProxyGenerator();

		public Double()
		{
			var options = new ProxyGenerationOptions();
			options.BaseTypeForInterfaceProxy = typeof(DoubleProxy<T>);

			Object = _generator.CreateInterfaceProxyWithoutTarget<T>(options, this);
			Proxy = (DoubleProxy<T>) (object) Object;
			Proxy.Double = this;
		}
		
		internal DoubleProxy<T> Proxy { get; private set; }
		public T Object { get; private set; }
	
		public static Double<T> Get(T obj)
		{
			var proxy = (DoubleProxy<T>) (object) obj;
			return proxy.Double;
		}
		
		protected abstract void Execute (IInvocation invocation);

		public void Intercept (IInvocation invocation)
		{
			Execute(invocation);
		}
	}
}

