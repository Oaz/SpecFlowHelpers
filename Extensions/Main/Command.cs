using System;
using System.Reflection;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace Oaz.SpecFlowHelpers
{
	public class Command<T> 
	{
		public Command (MethodInfo method, IEnumerable<object> parameters)
		{
			Method = method;
			Parameters = parameters;
		}

		public MethodInfo Method {get;private set;}
		public IEnumerable<object> Parameters {get;private set;}
		
		public override bool Equals (object obj)
		{
			var other = obj as Command<T>;
			return Method == other.Method && Parameters.SequenceEqual(other.Parameters.Cast<object>());
		}
		
		public override int GetHashCode ()
		{
			return base.GetHashCode ();
		}
	}
}

