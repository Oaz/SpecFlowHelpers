using System;
using TechTalk.SpecFlow;

namespace Oaz.SpecFlowHelpers
{
	public static class Instance
	{
		private const string defaultName = "ù*⁼)";
		
		public static NamedInstance Named(string name)
		{
			return new NamedInstance() { Name=name };
		}
		
		public static void Is<T>(T obj) where T:class
		{
			Named(defaultName).Is(obj);
		}
		
		public static T Of<T>() where T:class
		{
			return Named(defaultName).Of<T>();
		}
	}
	
	public class NamedInstance
	{
		public string Name { get; set; }
		
		public T Of<T>() where T:class
		{
			var name = UniqueName<T>();
			if(!ScenarioContext.Current.ContainsKey(name))
				return null;
			return ScenarioContext.Current[name] as T;
		}
		
		public void Is<T>(T obj) where T:class
		{
			ScenarioContext.Current[UniqueName<T>()] = obj;
		}
		
		private string UniqueName<T>()
		{
			return string.Format("{0}/{1}", typeof(T), Name);
		}
	}
}

