using System;
using System.Linq;
using Castle.DynamicProxy;
using Castle.Core.Interceptor;
using System.Collections.Generic;

namespace Oaz.SpecFlowHelpers.Doubles
{
	internal class DoubleProxy<T> where T:class
	{
		internal TestDouble<T> Double { get; set; }
	}
	
	public class TestDouble<T> : IInterceptor where T:class
	{
		private static readonly ProxyGenerator _generator = new ProxyGenerator();

		public TestDouble()
		{
			var options = new ProxyGenerationOptions();
			options.BaseTypeForInterfaceProxy = typeof(DoubleProxy<T>);

			Object = _generator.CreateInterfaceProxyWithoutTarget<T>(options, this);
			Proxy = (DoubleProxy<T>) (object) Object;
			Proxy.Double = this;
			
			Commands = new List<Command<T>>();
			Behaviour = null;
		}
		
		internal DoubleProxy<T> Proxy { get; private set; }
		public T Object { get; private set; }
		public IList<Command<T>> Commands { get; private set; }
		public Func<Command<T>,object> Behaviour {get; internal set;}
	
		public static TestDouble<T> Get(T obj)
		{
			var proxy = ((object) obj) as DoubleProxy<T>;
			if( proxy == null )
				return null;
			return proxy.Double;
		}

		public void Intercept (IInvocation invocation)
		{
			var cmd = new Command<T>(invocation.Method, invocation.Arguments.AsEnumerable());
			Commands.Add(cmd);
			if(Behaviour != null)
			{
				var returnValue = Behaviour(cmd);
				if( cmd.Method.ReturnType != typeof(void) )
					invocation.ReturnValue = returnValue;
			}
		}
		

	}
}

