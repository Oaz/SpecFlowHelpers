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
			Parameters = parameters.ToList();
			Returns = null;
		}
		
		public Command (MethodInfo method, IEnumerable<object> parameters, object returns)
		{
			Method = method;
			Parameters = parameters.ToList();
			Returns = returns;
		}

		public MethodInfo Method {get;private set;}
		public IList<object> Parameters {get;private set;}
		public object Returns {get;private set;}
		
		public override bool Equals (object obj)
		{
			var other = obj as Command<T>;
			return Method == other.Method && Parameters.SequenceEqual(other.Parameters.Cast<object>());
		}
		
		public override int GetHashCode ()
		{
			return base.GetHashCode ();
		}
		
		public override string ToString ()
		{
			return string.Format (
			  "[Command: Method={0}, Parameters={1}, Returns={2}]",
			  Method,
			  string.Join("|",Parameters.Select(p=>p.ToString()).ToArray()),
			  Returns
			);
		}
	}
}

