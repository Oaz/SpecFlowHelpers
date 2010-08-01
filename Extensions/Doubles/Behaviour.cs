using System;
namespace Oaz.SpecFlowHelpers.Doubles
{
	internal class BehaviourNotImplemented
	{
	}
	
	public class Behaviour
	{
		static Behaviour()
		{
			NotImplemented = new BehaviourNotImplemented();
		}
		
		public static object NotImplemented { get; private set; }
	}
	
}

