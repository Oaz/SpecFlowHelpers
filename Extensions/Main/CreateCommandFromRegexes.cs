using System;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

namespace Oaz.SpecFlowHelpers
{
	public class CreateCommandFromRegexes : ICreateCommands
	{
		public CreateCommandFromRegexes ()
		{
			MethodNameBuilder = s=>s;
		}
		
		public Regex MethodRegex {get;set;}
		public Regex ParameterRegex {get;set;}
		public int ParameterDelta {get;set;}
		public Regex ReturnsRegex {get;set;}
		public Func<string,string> MethodNameBuilder {get;set;}
		
		public Command<T> Command<T>(string text)
		{
			var methodMatch = MethodRegex.Match( text );
			var methodName = MethodNameBuilder(MatchValue(methodMatch,"method"));
			var method = typeof(T).GetMethod(methodName);
			Tools.Check( method != null, "Type <{0}> does not have a method named <{1}>", typeof(T), methodName );
			if( method.ReturnType == typeof(void) )
				return new Command<T>(method, Parameters(method,text));
			else
				return new Command<T>(method, Parameters(method,text), Returns(method,text));
		}
		
		private IEnumerable<object> Parameters(MethodInfo method, string text)
		{
			var matches = ParametersMatches(text);
			var paramsDefinition = method.GetParameters();
			Tools.Check(
				matches.Count() == paramsDefinition.Length,
			    "Wrong number of parameters ({2} instead of {3}) for method <{0}> in type <{1}>",
			    method.Name, method.DeclaringType, matches.Count(), paramsDefinition.Length
			);
			var i = 0;
			foreach(var match in matches)
			{
				var val = MatchValue(match);
				var typedValue = Convert.ChangeType(val, paramsDefinition[i++].ParameterType);
				yield return typedValue;
			}
		}
		
		private IEnumerable<Match> ParametersMatches(string text)
		{
			var allMatches = ParameterRegex.Matches( text ).Cast<Match>();
			if( ReturnsRegex.Match( text ).Success )
				return allMatches.Take( allMatches.Count() - ParameterDelta );
			else
				return allMatches;
		}
		
		private object Returns(MethodInfo method, string text)
		{
			var match = ReturnsRegex.Match( text );
			if( !match.Success )
				return null;
			var val = MatchValue(match);
			var typedValue = Convert.ChangeType(val, method.ReturnType);
			return typedValue;
		}
		
		private static string MatchValue(Match m)
		{
			return m.Groups.Cast<Group>().Last(g=>g.Success).Value;
		}
		
		private static string MatchValue(Match m,string name)
		{
			return m.Groups[name].Value;
		}
	}
}

