using System;
using System.Linq;
using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace Oaz.SpecFlowHelpers.Doubles
{
	public static class TableExtensions
	{
		public static T AsStub<T>(this Table table) where T:class
		{
			return Tools.HandleExceptionInstance(
			  () =>
			  {
				var stub = new TestStub<T>();
				foreach(var row in table.Rows)
					stub.SetupGet(row[0], row[1]);
				return stub.Object;
			  }
			);
		}
		
		public static IEnumerable<T> AsStubEnumerable<T>(this Table table) where T:class
		{
			return Tools.HandleExceptionInstance(
			  () => AsStubEnumerableImpl<T>(table)
			);
		}
		
		private static IEnumerable<T> AsStubEnumerableImpl<T>(Table table) where T:class
		{
			foreach(var row in table.Rows)
			{
				var stub = new TestStub<T>();
				foreach(var field in table.Header)
					stub.SetupGet(field, row[field]);
				yield return stub.Object;
			}
		}
		
		public static IEnumerable<Command<T>> AsCommands<T>(this Table table, ICreateCommands cmdBuilder) where T:class
		{
			return Tools.HandleExceptionInstance(
			  () =>
			  {
				return AsCommandsImpl<T>(table,cmdBuilder);
			  }
			);
		}
		
		private static IEnumerable<Command<T>> AsCommandsImpl<T>(Table table, ICreateCommands cmdBuilder) where T:class
		{
			foreach(var row in table.Rows)
				yield return cmdBuilder.Command<T>(row[0]);
		}
	}
}

