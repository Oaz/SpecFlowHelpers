using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
namespace Oaz.SpecFlowHelpers.Doubles
{
	public static class InstanceExtensions
	{
		public static InstanceHandler<T> IsVerifiable<T>(this InstanceHandler<T> ih) where T:class
		{
			var db = new OutputDouble<T>();
			ih.Is(db.Object);
			return ih;
		}

		public static IEnumerable<Command<T>> Commands<T>(this InstanceHandler<T> ih) where T:class
		{
			var outputDouble = Double<T>.Get(ih.Object) as OutputDouble<T>;
			return outputDouble.Commands;
		}
	}
}

