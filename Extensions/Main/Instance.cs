using System;
using TechTalk.SpecFlow;

namespace Oaz.SpecFlowHelpers
{
	public static class Instance
	{
		public static InstanceHandler<T> Of<T>() where T:class
		{
			return new InstanceHandler<T>();
		}
	}
	
	public class InstanceHandler<T> where T:class
	{
		private string Name { get; set; }
		private const string defaultName = "ù*⁼)";
		
		internal InstanceHandler()
		{
			Name = defaultName;
		}
		
		public void Is(T obj)
		{
			ScenarioContext.Current[UniqueName()] = obj;
		}
		
		public InstanceHandler<T> Named(string name)
		{
			return new InstanceHandler<T>() { Name = name };
		}
		
		public T Object
		{
			get
			{
				var name = UniqueName();
				if(!ScenarioContext.Current.ContainsKey(name))
					return null;
				return ScenarioContext.Current[name] as T;
			}
		}
		
		public static implicit operator T(InstanceHandler<T> ih)
		{
			return ih.Object;
		}
		
		private string UniqueName()
		{
			return string.Format("{0}/{1}", typeof(T), Name);
		}
	}
}

