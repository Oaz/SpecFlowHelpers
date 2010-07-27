using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Oaz.SpecFlowHelpers
{
	public static class With
	{
		static With()
		{
			Syntax = new SyntaxFactory();
		}
		
		public static SyntaxFactory Syntax { get; private set; }
	}
	
	public class SyntaxFactory
	{
		public SyntaxFactory ()
		{
			Natural = new CreateCommandFromRegexes ()
			{
				MethodRegex = new Regex("^(.*?) [\"0-9]"),
				ParameterRegex = new Regex(" (\"(.*?)\"|[0-9]+)"),
				ParameterDelta = 1,
				ReturnsRegex = new Regex("=> (.*)"),
				MethodNameBuilder = s=>s.AsMethodName()
			};
			
			Functions = new CreateCommandFromRegexes ()
			{
				MethodRegex = new Regex("(.*)\\("),
				ParameterRegex = new Regex("[,\\(]([^,\\)]*)"),
				ParameterDelta = 0,
				ReturnsRegex = new Regex("=(.*)")
			};

		}

		public ICreateCommands Natural { get; private set; }
		public ICreateCommands Functions { get; private set; }
	}
}

