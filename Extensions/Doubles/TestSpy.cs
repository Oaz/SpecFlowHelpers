using System;
using System.Collections.Generic;
using Castle.Core.Interceptor;
using System.Linq;
namespace Oaz.SpecFlowHelpers.Doubles
{
	public class TestSpy<T> : TestDouble<T> where T:class
	{
		public TestSpy ()
		{
			Commands = new List<Command<T>>();
			Behaviour = null;
		}
		
		public IList<Command<T>> Commands { get; private set; }
		public Func<Command<T>,object> Behaviour {get; internal set;}

		protected override void Execute (IInvocation invocation)
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

