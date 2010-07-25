using System;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Collections;
using System.Linq;

namespace Oaz.SpecFlowHelpers
{
	public class CommandLanguage
	{
		public CommandLanguage ()
		{
			MethodRegex = new Regex("^(.*?) [\"0-9]");
			ParameterRegex = new Regex(" (\"(.*?)\"|[0-9]+)");
		}
		
		public Regex MethodRegex {get;set;}
		public Regex ParameterRegex {get;set;}
		
		public Command<T> Command<T>(string text)
		{
			var match = MethodRegex.Match( text );
			var method = typeof(T).GetMethod(MatchValue(match).AsMethodName());
			return new Command<T>(method, Parameters(method,text));
		}
		
		private IEnumerable Parameters(MethodInfo method, string text)
		{
			var matches = ParameterRegex.Matches( text );
			foreach(Match match in matches)
				yield return MatchValue(match);
		}
		
		private static string MatchValue(Match m)
		{
			return m.Groups.Cast<Group>().Last(g=>g.Success).Value;
		}
	}
}

