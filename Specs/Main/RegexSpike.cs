using System;
using System.Linq;
using System.Text.RegularExpressions;
using NUnit.Framework;

namespace Specs.Oaz.SpecFlowHelpers
{
	[TestFixture]
	public class RegexSpike
	{
		[Test]
		public void FunctionCallStyle ()
		{	
			Check
			.Match("aa(bb,cc,dd,,ee)")
			.Command("(.*)\\(", "aa")
			.Parameters("[,\\(]([^,\\)]*)", 0, "bb", "cc", "dd", "", "ee");
		}
		
		[Test]
		public void FunctionCallStyleWithResult()
		{	
			Check
			.Match("aa(bb,cc,dd,,ee)=zz")
			.Command("(.*)\\(", "aa")
			.Parameters("[,\\(]([^,\\)]*)", 0, "bb", "cc", "dd", "", "ee")
			.Result("=(.*)", "zz");
		}
		
		[Test]
		public void SpacesStyle ()
		{	
			Check
			.Match("aa bb cc dd ee")
			.Command("^(.*?) ", "aa")
			.Parameters(" ([^ ]+)", 0, "bb", "cc", "dd", "ee");
		}
		
		[Test]
		public void SpacesStyleWithResult ()
		{
			Check
			.Match("aa bb cc dd ee => zz")
			.Command("^(.*?) ", "aa")
			.Parameters(" ([^ ]+)", 2, "bb", "cc", "dd", "ee")
			.Result("=> *(.*)", "zz");
		}
		
		[Test]
		public void SentenceStyle ()
		{
			Check
			.Match("aa aa \"b b\" 3 \"d d\" 4")
			.Command("^(.*?) [\"0-9]", "aa aa")
			.Parameters(" (\"(.*?)\"|[0-9]+)", 0, "b b", "3", "d d", "4");
			
			Check
			.Match("aa aa 3 \"b b\" 4 \"d d\"")
			.Command("^(.*?) [\"0-9]", "aa aa")
			.Parameters(" (\"(.*?)\"|[0-9]+)", 0, "3", "b b", "4", "d d");
		}
	}
	
	static class Check
	{
		public static Checker Match(string text)
		{
			return new Checker() { Text=text };
		}
	}
	
	class Checker
	{
		public string Text  {get;set;}

		public Checker Command(string regex, string val)
		{
			return RegexMatch( regex, val, "Wrong Command" );
		}
		
		public Checker Parameters(string regex, int delta, params string[] values)
		{
			var matches = Regex.Matches( Text, regex );
			Assert.That( matches.Count, Is.EqualTo(values.Length+delta) );
			for(int i=0; i<values.Length; i++)
				Assert.That( MatchValue(matches[i]), Is.EqualTo(values[i]), string.Format("Wrong Parameter {0}",i) );
			return this;
		}
		
		public Checker Result(string regex, string val)
		{
			return RegexMatch( regex, val, "Wrong Result" );
		}

		private Checker RegexMatch(string regex, string val, string message)
		{
			var match = Regex.Match( Text, regex );
			Assert.That( MatchValue(match), Is.EqualTo(val), message );
			return this;
		}
		
		private static string MatchValue(Match m)
		{
			return m.Groups.Cast<Group>().Last(g=>g.Success).Value;
		}
	}
}

