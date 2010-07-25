using System;
using System.Collections.Generic;
using Castle.Core.Interceptor;
using System.Linq;
namespace Oaz.SpecFlowHelpers.Doubles
{
	public class OutputDouble<T> : Double<T> where T:class
	{
		public OutputDouble ()
		{
			Commands = new List<Command<T>>();
		}
		
		public IList<Command<T>> Commands { get; private set; }

		protected override void Execute (IInvocation invocation)
		{
			var cmd = new Command<T>(invocation.Method, invocation.Arguments.AsEnumerable());
			Commands.Add(cmd);
		}
	}
}

