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
		
		public InstanceHandler<T> Is(T obj)
		{
			Tools.Check( !Exists, "{0} is already defined", ToString() );
			ScenarioContext.Current[UniqueName()] = obj;
			return this;
		}
		
		public void Clear()
		{
			ScenarioContext.Current.Remove(UniqueName());
		}
		
		public InstanceHandler<T> Named(string name)
		{
			return new InstanceHandler<T>() { Name = name };
		}
		
		public bool Exists
		{
			get
			{
				return ScenarioContext.Current.ContainsKey(UniqueName());
			}
		}
		
		public T Object
		{
			get
			{
				var name = UniqueName();
				Tools.Check( ScenarioContext.Current.ContainsKey(name), "{0} is not defined", ToString() );
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
		
		public override string ToString ()
		{
			var naming = (Name == defaultName) ? "" : " named " + Name;
			if( !Exists )
				return string.Format ("empty instance{1} of {0}", typeof(T), naming);
			var obj = ScenarioContext.Current[UniqueName()];
			if( obj==null )
				return string.Format ("null instance{1} of {0}", typeof(T), naming);
			return string.Format ("instance{1} of {0} {2}", typeof(T), naming, obj);
		}

	}
}

