using System;
using TechTalk.SpecFlow;
using System.Collections.Generic;
using Moq;

namespace Oaz.SpecFlowHelpers.Moq
{
	public static class TableExtensions
	{
		public static T AsMock<T>(this Table table) where T:class
		{
			return Tools.HandleExceptionInstance(
			  () =>
			  {
				var mock = new Mock<T>(MockBehavior.Strict);
				foreach(var row in table.Rows)
					mock.SetupGet(row[0], row[1]);
				return mock.Object;
			  }
			);
		}
		
		public static IEnumerable<T> AsMockEnumerable<T>(this Table table) where T:class
		{
			return Tools.HandleExceptionInstance(
			  () => AsMockEnumerableImpl<T>(table)
			);
		}
		
		private static IEnumerable<T> AsMockEnumerableImpl<T>(Table table) where T:class
		{
			foreach(var row in table.Rows)
			{
				var mock = new Mock<T>(MockBehavior.Strict);
				foreach(var field in table.Header)
					mock.SetupGet(field, row[field]);
				yield return mock.Object;
			}
		}
	}
	
}

