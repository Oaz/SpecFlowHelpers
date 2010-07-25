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
		//	Tools.HandleExceptionInstance(
		//	  () =>
		//	  {
			
			
			/*
				var mock = new Mock<T>(MockBehavior.Loose);
				var recorder = new CommandRecorder<T>();
				ih.SetData("recorder", recorder);
			    var allMethods = typeof(T).GetMethods();
				foreach( var method in allMethods.Where(m=>m.IsVirtual) )
				{
					mock.SetupRecord(method, recorder);
				}
				ih.Is(mock.Object);
			*/	
				var db = new Double<T>();
				ih.Is(db.Object);
				
				
		//	  }
		//	);
			return ih;
		}
		
		

		public static IEnumerable<Command<T>> Commands<T>(this InstanceHandler<T> ih) where T:class
		{
			return Double<T>.Get(ih.Object).Commands;
		}
	}
}

