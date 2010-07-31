using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
namespace Oaz.SpecFlowHelpers.Doubles
{
	public static class InstanceExtensions
	{
		public static InstanceHandler<T> IsTestDouble<T>(this InstanceHandler<T> ih) where T:class
		{
			var tdb = new TestDouble<T>();
			ih.Is(tdb.Object);
			return ih;
		}
		
		internal static TestDouble<T> AsTestDouble<T>(this InstanceHandler<T> ih) where T:class
		{
			var tdb = TestDouble<T>.Get(ih.Object);
			Tools.Check( tdb != null, "{0} is not a test double", ih );
			return tdb;
		}
		
		public static InstanceHandler<T> Setup<T>(this InstanceHandler<T> ih, Func<Command<T>,object> behaviour) where T:class
		{
			ih.AsTestDouble().Behaviour = behaviour;
			return ih;
		}
		 
		public static IEnumerable<Command<T>> Commands<T>(this InstanceHandler<T> ih) where T:class
		{
			return ih.AsTestDouble().Commands;
		}
	}
}

