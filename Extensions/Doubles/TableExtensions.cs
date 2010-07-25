using System;
using System.Linq;
using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace Oaz.SpecFlowHelpers.Doubles
{
	public static class TableExtensions
	{
		public static IEnumerable<Command<T>> AsCommands<T>(this Table table, CommandLanguage language) where T:class
		{
			return AsCommandsImpl<T>(table,language).ToArray().AsEnumerable();
		}
		
		private static IEnumerable<Command<T>> AsCommandsImpl<T>(Table table, CommandLanguage language) where T:class
		{
			foreach(var row in table.Rows)
				yield return language.Command<T>(row[0]);
		}
	}
}

