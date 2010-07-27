using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
namespace Oaz.SpecFlowHelpers.Doubles
{
	public static class InstanceExtensions
	{
		public static InstanceHandler<T> IsSpy<T>(this InstanceHandler<T> ih) where T:class
		{
			var spy = new TestSpy<T>();
			ih.Is(spy.Object);
			return ih;
		}
		
		public static InstanceHandler<T> IsSpy<T>(this InstanceHandler<T> ih, Func<Command<T>,object> behaviour) where T:class
		{
			var spy = new TestSpy<T>();
			spy.Behaviour = behaviour;
			ih.Is(spy.Object);
			return ih;
		}
		 
		public static IEnumerable<Command<T>> Commands<T>(this InstanceHandler<T> ih) where T:class
		{
			var spy = TestDouble<T>.Get(ih.Object) as TestSpy<T>;
			Tools.Check( spy != null, "Test Double is not a Spy" );
			return spy.Commands;
		}
	}
}

