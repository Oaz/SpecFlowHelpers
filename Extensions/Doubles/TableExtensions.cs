using System;
using System.Linq;
using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace Oaz.SpecFlowHelpers.Doubles
{
	public static class TableExtensions
	{
		public static Func<Command<T>,object> AsCommandBehaviour<T>(this Table table, ICreateCommands cmdBuilder) where T:class
		{
			return Tools.HandleExceptionInstance(
			  () =>
			  {
				return AsCommandBehaviourImpl<T>(table,cmdBuilder);
			  }
			);
		}

		private static Func<Command<T>,object> AsCommandBehaviourImpl<T>( Table table, ICreateCommands cmdBuilder) where T:class
		{
			var cmds = AsCommandsImpl<T>(table, cmdBuilder).ToList();
			return cmd =>
			{
				var foundCmd = cmds.Find( c => c.Equals(cmd) );
				if( foundCmd == null )
					return Behaviour.NotImplemented;
				else
					return foundCmd.Returns;
			};
		}
		
		public static Func<Command<T>,object> AsPropertiesBehaviour<T>(this Table table) where T:class
		{
			return Tools.HandleExceptionInstance(
			  () =>
			  {
				return AsPropertiesBehaviourImpl<T>(table.AsPropertiesDictionary<T>());
			  }
			);
		}

		private static Func<Command<T>,object> AsPropertiesBehaviourImpl<T>( this IDictionary<string,object> values ) where T:class
		{
			var propertyValues = new Dictionary<string,object>();
			foreach(var pair in values)
				propertyValues["get_"+pair.Key] = pair.Value;
			return cmd =>
			{
				if( !propertyValues.ContainsKey(cmd.Method.Name) )
					return null;
				else
					return propertyValues[cmd.Method.Name];
			};
		}
		
		//=============================================

		public static T AsTestDoubleWithProperties<T>(this Table table) where T:class
		{
			return Tools.HandleExceptionInstance(
			  () =>
			  {
				var tdb = new TestDouble<T>();
				tdb.Setup( table.AsPropertiesBehaviour<T>() );
				return tdb.Object;
			  }
			);
		}
		
		public static IEnumerable<T> AsTestDoubleWithPropertiesEnumerable<T>(this Table table) where T:class
		{
			return Tools.HandleExceptionInstance(
			  () => AsTestDoubleWithPropertiesEnumerableImpl<T>(table)
			);
		}
		
		private static IEnumerable<T> AsTestDoubleWithPropertiesEnumerableImpl<T>(Table table) where T:class
		{
			foreach(var row in table.Rows)
			{
				var tdb = new TestDouble<T>();
				tdb.Setup( row.AsPropertiesDictionary<T>().AsPropertiesBehaviourImpl<T>() );
				yield return tdb.Object;
			}
		}
		
		//=============================================

		public static IEnumerable<Command<T>> AsCommands<T>(this Table table, ICreateCommands cmdBuilder) where T:class
		{
			return Tools.HandleExceptionInstance(
			  () =>
			  {
				return AsCommandsImpl<T>(table,cmdBuilder).ToArray().AsEnumerable();
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

