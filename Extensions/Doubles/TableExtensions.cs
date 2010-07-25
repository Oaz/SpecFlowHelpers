using System;
using System.Linq;
using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace Oaz.SpecFlowHelpers.Doubles
{
	public static class TableExtensions
	{
		public static T AsDouble<T>(this Table table) where T:class
		{
			return Tools.HandleExceptionInstance(
			  () =>
			  {
				var db = new InputDouble<T>();
				foreach(var row in table.Rows)
					db.SetupGet(row[0], row[1]);
				return db.Object;
			  }
			);
		}
		
		public static IEnumerable<T> AsDoubleEnumerable<T>(this Table table) where T:class
		{
			return Tools.HandleExceptionInstance(
			  () => AsDoubleEnumerableImpl<T>(table)
			);
		}
		
		private static IEnumerable<T> AsDoubleEnumerableImpl<T>(Table table) where T:class
		{
			foreach(var row in table.Rows)
			{
				var db = new InputDouble<T>();
				foreach(var field in table.Header)
					db.SetupGet(field, row[field]);
				yield return db.Object;
			}
		}
		
		public static IEnumerable<Command<T>> AsCommands<T>(this Table table, CommandSyntax language) where T:class
		{
			return Tools.HandleExceptionInstance(
			  () =>
			  {
				return AsCommandsImpl<T>(table,language);
			  }
			);
		}
		
		private static IEnumerable<Command<T>> AsCommandsImpl<T>(Table table, CommandSyntax language) where T:class
		{
			foreach(var row in table.Rows)
				yield return language.Command<T>(row[0]);
		}
	}
}

