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
	
	internal class TestDouble<T> : IInterceptor where T:class
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
			Behaviours = new List<Func<Command<T>, object>>();
		}
		
		internal DoubleProxy<T> Proxy { get; private set; }
		public T Object { get; private set; }
		public IList<Command<T>> Commands { get; private set; }
		private List<Func<Command<T>,object>> Behaviours {get; set;}
	
		public static TestDouble<T> Get(T obj)
		{
			var proxy = ((object) obj) as DoubleProxy<T>;
			if( proxy == null )
				return null;
			return proxy.Double;
		}

		public void Setup (Func<Command<T>,object> behaviour)
		{
			Behaviours.Add(behaviour);
		}

		public void Intercept (IInvocation invocation)
		{
			var cmd = new Command<T>(invocation.Method, invocation.Arguments.AsEnumerable());
			Commands.Add(cmd);
			
			bool processed = (cmd.Method.ReturnType == typeof(void));
			foreach(var behaviour in Behaviours)
			{
				var returnValue = behaviour(cmd);
				if( returnValue.GetType() == typeof(BehaviourNotImplemented) )
					continue;
				if( cmd.Method.ReturnType != typeof(void) )
					invocation.ReturnValue = returnValue;
				processed = true;
			}
			Tools.Check(
			  processed,
			  "Unexpected call '{0}' on test double",
			  cmd.Method.Name
			);	
		}
		

	}
}

